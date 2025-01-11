using Microsoft.SemanticKernel;

var apiKey = Environment.GetEnvironmentVariable("OpenApi");

var kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion("gpt-3.5-turbo", apiKey!).Build();

while (true)
{
    Console.WriteLine("Q: ");
    var question = Console.ReadLine();
    System.Console.WriteLine(await kernel.InvokePromptAsync(question!));
}
