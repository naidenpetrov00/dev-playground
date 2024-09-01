import { readFile } from "fs/promises";
import { join } from "path";
import { Mistral } from "@mistralai/mistralai";
import { RecursiveCharacterTextSplitter } from "langchain/text_splitter";
import { createClient } from "@supabase/supabase-js";
const supabaseUrl = process.env.SUPABASE_API_URL;
const supabaseKey = process.env.SUPABASE_API_KEY;
console.log(supabaseKey);
console.log(supabaseUrl);
const supabaseClient = createClient(supabaseUrl, supabaseKey);
const apiKey = process.env.MISTRAL_AI_API_KEY;
const mistralClient = new Mistral({ apiKey });
const main = async () => {
    const chatResponse = await mistralClient.chat.complete({
        model: "mistral-tiny",
        messages: [
            {
                role: "system",
                content: "You are interviewer for .net position, Reply with JSON",
            },
            { role: "user", content: "I am Nayden " },
        ],
        //from 0 to 1 more focus > more creative
        temperature: 0.5,
        responseFormat: { type: "json_object" },
    });
    //   for await (const chunk of chatResponse) {
    //     chunk.data.choices[0].delta.content;
    //   }
    if (chatResponse && chatResponse.choices) {
        console.log(chatResponse.choices[0].message);
    }
};
// main();
async function splitDocument(path) {
    try {
        const filePath = join(path);
        const text = await readFile(filePath, "utf8");
        const splitter = new RecursiveCharacterTextSplitter({
            chunkSize: 250,
            chunkOverlap: 40,
        });
        const output = await splitter.createDocuments([text]);
        return output.map((chunk) => chunk.pageContent);
    }
    catch (error) {
        console.error("Error reading file:", error);
    }
}
async function createEmbeddings(chunks) {
    if (chunks == null) {
        return;
    }
    const response = await mistralClient.embeddings.create({
        model: "mistral-embed",
        inputs: chunks,
    });
    const data = chunks.map((c, index) => ({
        content: c,
        embedding: response.data[index].embedding,
    }));
    return data;
}
const handbookChunks = await splitDocument("handbook.txt");
const embeddings = await createEmbeddings(handbookChunks);
const response = await supabaseClient.from("handbook_docks").insert(embeddings);
console.log("response");
console.log(response);
