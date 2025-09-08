using Microsoft.SemanticKernel;

namespace HelloKernel.RoadmapSteps
{
    public static class Plugins_Semantic
    {
        public static async Task RunAsync(Kernel kernel)
        {

            // Load plugin
            var pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", "SummarizerPlugin");
            var plugin = kernel.ImportPluginFromPromptDirectory(pluginPath, "SummarizerPlugin");

            //Use It
            var result = await kernel.InvokeAsync("SummarizerPlugin", "Summarize", new()
            {
                ["input"] = "Semantic Kernel is a .NET library that lets you compose AI models, prompts, and native code together."
            });

            Console.WriteLine("Summary > " + result);
        }
    }
}
