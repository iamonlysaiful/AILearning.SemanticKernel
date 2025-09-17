using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace HelloKernel.RoadmapSteps
{
    public static class ChatCompletion_PromptingPatternWithInstruction
    {
        public static async Task RunAsync(Kernel kernel)
        {
            var chatService = kernel.Services.GetRequiredService<IChatCompletionService>();

            //Sometimes you want machine-readable output. With local models in LM Studio you can enforce this with instruction + system prompt.
            var history = new ChatHistory();
            history.AddSystemMessage(@"
                You are an API that always returns JSON with this schema:
                { ""summary"": string, ""keywords"": [string] }
                ");

            history.AddUserMessage("Explain Semantic Kernel in 2 sentences and extract 3 keywords.");

            var reply = await chatService.GetChatMessageContentsAsync(history, kernel: kernel);
            Console.WriteLine("Assistant JSON > " + reply[0].Content);
        }
    }
}
