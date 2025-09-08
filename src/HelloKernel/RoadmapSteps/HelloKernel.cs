
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace HelloKernel.RoadmapSteps
{
    public static class HelloKernelSection
    {
        public static async Task RunAsync(Kernel kernel)
        {
            var chatService = kernel.Services.GetRequiredService<IChatCompletionService>();
            // # Create a ChatHistory and ask something
            // System prompts set behavior, user prompts give tasks.
            var history = new ChatHistory("You are a helpful assistant that writes short poems.");
            history.AddUserMessage("Write a short 3-line haiku about Chemistry.");

            var responses = await chatService.GetChatMessageContentsAsync(history, kernel: kernel);
            
            var reply = responses.FirstOrDefault()?.Content ?? "<no response>";
            Console.WriteLine("Assistant > " + reply);
            // ...add more blocks as needed from the section...
        }
    }
}
