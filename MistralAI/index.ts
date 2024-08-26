import { readFile } from "fs/promises";
import { join } from "path";
import { Mistral } from "@mistralai/mistralai";
import { RecursiveCharacterTextSplitter } from "langchain/text_splitter";

//Windows
// const apiKey = process.env.MISTRAL_AI_API_KEY;
//Linux
const apiKey = process.env.MISTRAL_AI_API_KEY;
const client = new Mistral({ apiKey });

const main = async () => {
  const chatResponse = await client.chat.complete({
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

async function splitDocument(path: string) {
  try {
    const filePath = join(path);
    const text = await readFile(filePath, "utf8");
    const splitter = new RecursiveCharacterTextSplitter({
      chunkSize: 250,
      chunkOverlap: 40,
    });
    const output = await splitter.createDocuments([text]);
    return output.map((chunk) => chunk.pageContent);
  } catch (error) {
    console.error("Error reading file:", error);
  }
}
// await splitDocument("handbook.txt");
// main();

const exampleChunk =
  "professional ethics and behavior are expected of all Ambrics employees. Further, Ambrics expects each employee to display good judgment,";
const response = await client.embeddings.create({
  model: "mistral-embed",
  inputs: [exampleChunk],
});
console.log(response);

