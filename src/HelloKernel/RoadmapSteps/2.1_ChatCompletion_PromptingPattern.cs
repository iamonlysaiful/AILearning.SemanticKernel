using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace HelloKernel.RoadmapSteps
{
    public static class ChatCompletion_PromptingPattern
    {
        public static async Task RunAsync(Kernel kernel)
        {
            var chatService = kernel.Services.GetRequiredService<IChatCompletionService>();
            
            //Few-shot prompting: include examples in the history so the model learns the pattern.
            var history = new ChatHistory("You are a helpful bot that converts C# LINQ queries to SQL.");

            history.AddUserMessage("LINQ: customers.Where(c => c.Age > 30).Select(c => c.Name);");
            history.AddAssistantMessage("SQL: SELECT Name FROM Customers WHERE Age > 30;");

            history.AddUserMessage("LINQ: orders.Where(o => o.Total > 1000).Select(o => o.Id);");

            var reply = await chatService.GetChatMessageContentsAsync(history, kernel: kernel);
            Console.WriteLine("Assistant > " + reply[0].Content);
        }
    }
}
