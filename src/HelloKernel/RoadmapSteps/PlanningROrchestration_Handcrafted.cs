using HelloKernel.Plugins;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace HelloKernel.RoadmapSteps
{
    public static class PlanningROrchestration_Handcrafted
    {
        public static async Task RunAsync(Kernel kernel)
        {
            // Simulate a chat prompt search
            string userQuery = "Tell me about Robi";
            // Print query
            Console.WriteLine($"Query: {userQuery}");
            // Get embedding for the query


            // Search memory for context
            var memoryResults = await MemoryNEmbedding_PersistentMemoryPGVector.EmbeddingStoreToPGVectorNSearchAsync(kernel, userQuery);

            string memoryContext = "";
            await foreach (var r in memoryResults)
            {
                memoryContext += r.Record.Data + " " + r.Record.MetadataJson + "\n";
            }

            // Build chat history with context
            var chat = kernel.Services.GetRequiredService<IChatCompletionService>();
            var history = new ChatHistory("You are a helpful assistant. Use the provided context if relevant.");

            history.AddSystemMessage($"Memory context:\n{memoryContext}");
            history.AddUserMessage("Write a short introduction for Robi.");

            // Call LLM
            var reply = await chat.GetChatMessageContentsAsync(history, kernel: kernel);
            Console.WriteLine("Assistant > " + reply[0].Content);

        }
    }
}
