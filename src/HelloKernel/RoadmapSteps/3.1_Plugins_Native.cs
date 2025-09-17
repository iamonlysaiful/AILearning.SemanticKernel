using KernelInfrastructure.Plugins;
using Microsoft.SemanticKernel;

namespace HelloKernel.RoadmapSteps
{
    public static class Plugins_Native
    {
        public static async Task RunAsync(Kernel kernel)
        {
            // Import plugin
            kernel.ImportPluginFromObject(new MathPluginNative(), "math");

            // Now you can manually call it like any kernel function:
            var result = await kernel.InvokeAsync("math", "add_numbers", new()
            {
                ["a"] = 5,
                ["b"] = 7
            });

            Console.WriteLine("Manual call: 5+7 = " + result);
        }
    }
}
