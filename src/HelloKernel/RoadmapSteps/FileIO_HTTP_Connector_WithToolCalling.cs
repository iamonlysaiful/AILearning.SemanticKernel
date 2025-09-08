using HelloKernel.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text.Json;

namespace HelloKernel.RoadmapSteps
{
    public static class FileIO_HTTP_Connector_WithToolCalling
    {
        public static async Task RunAsync(Kernel kernel)
        {
            // Import plugins
            kernel.ImportPluginFromObject(new GitHubPlugin(), "github");
            kernel.ImportPluginFromObject(new FilePlugin(), "file");
            kernel.ImportPluginFromObject(new HttpPlugin(), "http");


            var chat = kernel.Services.GetRequiredService<IChatCompletionService>();

            var history = new ChatHistory(@"
                You are an assistant that can fetch GitHub issues and write them to files.
                You can use the provided tools if needed.
                ");

            history.AddUserMessage("Fetch the latest GitHub issues for dotnet/runtime. Save them to file naming issues.txt. And tell me how many issues are open.");

            // Auto tool calling
            //----------------------------
            var settings = new OpenAIPromptExecutionSettings
            {
                //ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
                Temperature = 0.2
            };

            var reply = await chat.GetChatMessageContentsAsync(chatHistory: history, executionSettings: settings, kernel: kernel);

            Console.WriteLine("Assistant > " + reply[0].Content);

            // Manual tool calling
            //----------------------------
            // Step 1: Fetch issues
            // var issueArgs = new KernelArguments { ["repo"] = "dotnet/runtime" };
            // var issuesJson = await kernel.InvokeAsync("github", "get_issues", issueArgs);
            // Console.WriteLine("Fetched issues: " + issuesJson.GetValue<string>());

            // // Step 2: Write to file
            // var issuesContent = issuesJson.GetValue<string>();
            // if (string.IsNullOrWhiteSpace(issuesContent))
            // {
            //     Console.WriteLine("No issues content returned.");
            //     return;
            // }
            // var writeArgs = new KernelArguments
            // {
            //     ["path"] = "issues.txt",
            //     ["content"] = issuesContent
            // };
            // var writeResult = await kernel.InvokeAsync("file", "write_file", writeArgs);
            // Console.WriteLine(writeResult.GetValue<string>());

            // // Step 3: Count open issues (optional)
            // var issues = JsonSerializer.Deserialize<List<JsonElement>>(issuesContent) ?? new List<JsonElement>();
            // Console.WriteLine($"Number of open issues: {issues.Count}");
            // Console.WriteLine($"Number of open issues: {issues.Count}");


        }
    }
}
