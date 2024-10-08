import { Mistral } from "@mistralai/mistralai";
const mistralApiKey = process.env.MISTRAL_AI_API_KEY;
const mistralClient = new Mistral({ apiKey: mistralApiKey });
const data = [
    {
        transaction_id: "T1001",
        customer_id: "C001",
        payment_amount: 125.5,
        payment_date: "2021-10-05",
        payment_status: "Paid",
    },
    {
        transaction_id: "T1002",
        customer_id: "C002",
        payment_amount: 89.99,
        payment_date: "2021-10-06",
        payment_status: "Unpaid",
    },
    {
        transaction_id: "T1003",
        customer_id: "C003",
        payment_amount: 120.0,
        payment_date: "2021-10-07",
        payment_status: "Paid",
    },
    {
        transaction_id: "T1004",
        customer_id: "C002",
        payment_amount: 54.3,
        payment_date: "2021-10-05",
        payment_status: "Paid",
    },
    {
        transaction_id: "T1005",
        customer_id: "C001",
        payment_amount: 210.2,
        payment_date: "2021-10-08",
        payment_status: "Pending",
    },
];
function getPaymentStatus({ transactionId }) {
    const transaction = data.find((row) => row.transaction_id === transactionId);
    if (transaction) {
        return JSON.stringify({ status: transaction.payment_status });
    }
    return JSON.stringify({ error: "transaction id not found." });
}
function getPaymentDate({ transactionId }) {
    const transaction = data.find((row) => row.transaction_id === transactionId);
    if (transaction) {
        return JSON.stringify({ date: transaction.payment_date });
    }
    return JSON.stringify({ error: "transaction id not found." });
}
const availableFunctions = {
    getPaymentDate,
    getPaymentStatus,
};
async function agent(query) {
    const messages = [{ role: "user", content: query }];
    for (let index = 0; index < 5; index++) {
        const response = await mistralClient.chat.complete({
            model: "mistral-large-latest",
            // @ts-ignore
            messages: messages,
            tools: [
                {
                    type: "function",
                    function: {
                        name: "getPaymentStatus",
                        description: "Get payment status of a transaction",
                        parameters: {
                            type: "object",
                            properties: {
                                transactionId: {
                                    type: "string",
                                    description: "The transaction id.",
                                },
                            },
                            required: ["transactionId"],
                        },
                    },
                },
                {
                    type: "function",
                    function: {
                        name: "getPaymentDate",
                        description: "Get payment date of transaction",
                        parameters: {
                            type: "object",
                            properties: {
                                transactionId: {
                                    type: "string",
                                    description: "The transaction id.",
                                },
                            },
                            required: ["transactionId"],
                        },
                    },
                },
            ],
        });
        const message = response.choices[0].message;
        // @ts-ignore
        messages.push(message);
        if (response.choices[0].finishReason === "stop") {
            return message.content;
        }
        else if (response.choices[0].finishReason === "tool_calls") {
            const $function = message.toolCalls[0].function;
            const functionName = $function.name;
            //@ts-ignore
            const functionArgs = JSON.parse($function.arguments);
            //@ts-ignore
            const funcResponse = availableFunctions[functionName](functionArgs);
            messages.push({
                role: "tool",
                //@ts-ignore
                name: functionName,
                content: funcResponse,
            });
        }
    }
}
const response = await agent("Is the transaction T1001 paid? And if yes when");
console.log(response);
// const response = await agent("When is transaction T1001 paid ");
// console.log(response.choices![0].message.toolCalls![0].function);
