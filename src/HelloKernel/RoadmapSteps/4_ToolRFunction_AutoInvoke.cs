using KernelInfrastructure.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace HelloKernel.RoadmapSteps
{
    public static class ToolRFunction_AutoInvoke
    {
        public static async Task RunAsync(Kernel kernel)
        {

            // In Step 3, you invoked plugins manually. Now, we’ll enable function calling so the AI chooses when to use them.

            // Import MathPlugin
            kernel.ImportPluginFromObject(new MathPluginNative(), "math");

            // Get chat service
            var chat = kernel.Services.GetRequiredService<IChatCompletionService>();

            // Create chat history
            var history = new ChatHistory("You are a helpful assistant. Use tools if needed.");

            // Example: user asks a question
            //history.AddUserMessage("What is 5 plus 7?");
            //history.AddUserMessage("What is todays date?");

            // Enable Tool calling
            var settings = new OpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            var response = await chat.GetChatMessageContentsAsync(history, settings, kernel);

            Console.WriteLine("Assistant > " + response[0].Content);
        }
    }
}
