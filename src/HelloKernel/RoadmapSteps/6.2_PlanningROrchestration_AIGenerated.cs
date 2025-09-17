using KernelInfrastructure.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
namespace HelloKernel.RoadmapSteps
{
    public static class PlanningROrchestration_AIGenerated
    {
        public static async Task RunAsync(Kernel kernel)
        {
            // Import MathPlugin (from Step 3)
            kernel.ImportPluginFromObject(new MathPluginNative(), "math");

            // Create planner
            var history = new ChatHistory("You are an assistant that can call tools.");

            history.AddUserMessage("Add 5 and 7, then tell me today's date.");

            var settings = new OpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            var chat = kernel.Services.GetRequiredService<IChatCompletionService>();
            var response = await chat.GetChatMessageContentsAsync(history, settings, kernel);

            Console.WriteLine("Assistant > " + response[0].Content);
        }
    }
}
