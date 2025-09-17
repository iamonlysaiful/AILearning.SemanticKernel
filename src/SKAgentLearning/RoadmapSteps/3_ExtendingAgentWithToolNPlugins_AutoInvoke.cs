using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using KernelInfrastructure.Plugins;

namespace SKAgentLearning.RoadmapSteps
{
    public static class Plugins_AutoInvoke
    {
        public static async Task RunAsync(Kernel kernel)
        {
            // Load Plugins
            kernel.ImportPluginFromObject(new MathPluginNative(), "math");
            var summarizerPlugin = kernel.ImportPluginFromPromptDirectory(
                "src/KernelInfrastructure/Plugins/SummarizerPlugin"
            );

            // Load a specific function from the plugin
            var summarizer = kernel.Plugins.GetFunction("SummarizerPlugin", "Summarize");


            // Execution settings to allow tool selection
            var execSettings = new PromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            var agent = new ChatCompletionAgent
            {
                Name = "helperAgent",
                Instructions = "You are a helpful assistant that will use tools when useful for the user's request.",
                Kernel = kernel,
                Arguments = new KernelArguments(executionSettings: execSettings)
            };

            // Test
            while (true)
            {
                Console.Write("User: ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) break;

                await foreach (var msgRes in agent.InvokeAsync(new[] { new ChatMessageContent(AuthorRole.User, input) }))
                {
                    var msg = msgRes.Message;
                    Console.WriteLine($"{msg.Role}: {msg.Content}");
                }
            }
        }
    }
}
