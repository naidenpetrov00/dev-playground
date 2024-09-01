import  { Mistral } from "@mistralai/mistralai";
import { createClient } from "@supabase/supabase-js";

const mistralApiKey = process.env.MISTRAL_AI_API_KEY;
const mistralClient = new Mistral({ apiKey: mistralApiKey });
const supabaseUrl = process.env.SUPABASE_API_URL;
const supabaseKey = process.env.SUPABASE_API_KEY;
const supabaseClient = createClient(supabaseUrl!, supabaseKey!);

// 1. Getting the user input
const input =
  "December 25th is on a Sunday, do I get any extra time off to account for that?";

// 2. Creating an embedding of the input
const embedding = await createEmbedding(input);

// 3. Retrieving similar embeddings / text chunks (aka "context")
const context = await retrieveMatches(embedding);
// console.log(context);

// 4. Combining the input and the context in a prompt
// and using the chat API to generate a response
const response = await generateChatResponse(context, input);
console.log(response);

async function createEmbedding(input: string | undefined) {
  if (input == null) {
    return;
  }

  const response = await mistralClient.embeddings.create({
    model: "mistral-embed",
    inputs: input!,
  });
  return response.data[0].embedding;
}

async function retrieveMatches(embedding: number[] | undefined) {
  if (embedding == null) {
    return;
  }
  const response = await supabaseClient.rpc("match_handbook_docks", {
    query_embedding: embedding,
    match_threshold: 0.78,
    match_count: 5,
  });
  const data = response.data;

  return data.map((chunk: any) => chunk.content).join(" ");
}

async function generateChatResponse(context: any, query: string) {
  const response = await mistralClient.chat.complete({
    model: "mistral-large-latest",
    messages: [
      { role: "system", content: context },
      {
        role: "user",
        content: query,
      },
    ],
  });

  return response.choices![0].message.content;
}
