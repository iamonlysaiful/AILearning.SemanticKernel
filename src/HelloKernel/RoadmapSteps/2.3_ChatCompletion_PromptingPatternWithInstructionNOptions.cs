using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace HelloKernel.RoadmapSteps
{
    public static class ChatCompletion_PromptingPatternWithInstructionNOptions
    {
        public static async Task RunAsync(Kernel kernel)
        {
            var chatService = kernel.Services.GetRequiredService<IChatCompletionService>();

            // Parameters (temperature, max tokens, etc.) When calling GetChatMessageContentsAsync, you can also pass options:
            var history = new ChatHistory("You are a helpful assistant that writes short poems.");
            var settings = new OpenAIPromptExecutionSettings
            {
                Temperature = 0.2,   // lower = more deterministic
                MaxTokens = 200
            };

            history.AddUserMessage("Write a short 3-line poem for children birthday named Yahyaa.");

            var response = await chatService.GetChatMessageContentsAsync(history, settings, kernel);
            Console.WriteLine("Assistant > " + response[0].Content);
        }
    }
}
