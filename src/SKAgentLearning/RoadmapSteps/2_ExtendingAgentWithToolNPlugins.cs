using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using KernelInfrastructure.Plugins;
using System.Text.Json.Nodes;

namespace SKAgentLearning.RoadmapSteps
{
    public static class Plugins
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


            var agent = new ChatCompletionAgent
            {
                Name = "toolAgent",
                Instructions = "You can answer questions, do math, and summarize text.",
                Kernel = kernel
            };

            var session = new ChatHistory();

            while (true)
            {
                Console.Write("User: ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) break;

                session.AddUserMessage(input);

                // Agent responds (can auto-call plugins)
                await foreach (var msgRes in agent.InvokeAsync(session))
                {
                    var msg = msgRes.Message;
                    Console.WriteLine($"{msg.Role}: {msg.Content}");
                    session.Add(msg);
                }
            }
        }
    }
}
