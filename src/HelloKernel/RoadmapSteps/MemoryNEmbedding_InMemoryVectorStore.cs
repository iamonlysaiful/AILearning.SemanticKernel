using HelloKernel.Vector;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace HelloKernel.RoadmapSteps
{
    public static class MemoryNEmbedding_InMemoryVectorStore
    {
        public static async Task RunAsync(Kernel kernel)
        {
            // Concepts: Embeddings- Convert text into numeric vectors. Similar meaning → nearby vectors.,  Memory- Kernel can store embeddings and retrieve the most relevant context for a query.
            // ## In-memory vector store- Fast access to recent context, but limited capacity.

            var embeddings = kernel.Services.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();
            var memory = new InMemoryVectorStore(embeddings);

            // Save some facts
            await memory.SaveAsync("fact1", "My name is Robi and I am a .NET developer.");
            await memory.SaveAsync("fact2", "I love working with Semantic Kernel.");
            await memory.SaveAsync("fact3", "I enjoy swimming and play zones with my child.");

            // Search semantically
            var results = await memory.SearchAsync("Who is Robi?", 2);
            foreach (var (text, score) in results)
            {
                Console.WriteLine($"Found: {text} (score={score:F3})");
            }

            // Feeding into chat
            var chatService = kernel.Services.GetRequiredService<IChatCompletionService>();

            // Query memory before chat
            var memoryResults = await memory.SearchAsync("What hobbies does Robi have?", 2);
            string memoryContext = string.Join("\n", memoryResults.Select(r => r.text));

            var history = new ChatHistory("You are a helpful assistant. Use the provided memory if relevant.");
            history.AddSystemMessage("Memory:\n" + memoryContext);
            history.AddUserMessage("What hobbies does Robi have?");

            var reply = await chatService.GetChatMessageContentsAsync(history, kernel: kernel);
            Console.WriteLine("Assistant > " + reply[0].Content);
        }
    }
}
