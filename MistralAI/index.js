import { Mistral } from "@mistralai/mistralai";
const main = async () => {
    const apiKey = process.env.MISTRAL_AI_API_KEY;
    const client = new Mistral({ apiKey });
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
    console.log(chatResponse.choices[0].message);
};
main();
