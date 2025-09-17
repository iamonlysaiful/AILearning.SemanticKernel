using KernelInfrastructure.Vector;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.PgVector;
using Npgsql;

namespace HelloKernel.RoadmapSteps
{
    public static class MemoryNEmbedding_PersistentMemoryPGVector
    {
        public static async Task RunAsync(Kernel kernel)
        {
            // Simulate a chat prompt search
            string userQuery = "does Robi enjoy rain?";
            // Print query
            Console.WriteLine($"Query: {userQuery}");
            // Get embedding for the query

            var searchResults = await EmbeddingStoreToPGVectorNSearchAsync(kernel, userQuery);
            
            var pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", "SummarizerPlugin");
            var summarizerPlugin = kernel.ImportPluginFromPromptDirectory(pluginPath, "SummarizerPlugin");

            await foreach (var result in searchResults)
            {
                // Direct search result printing
               // Console.WriteLine($"Found: {result.Record.Data} (score={result.Score:F3})");

                // Summarize the found fact using the plugin
                var summary = await kernel.InvokeAsync("SummarizerPlugin", "Summarize", new()
                {
                    ["input"] = result.Record.Data
                });

                Console.WriteLine($"Summary: {summary}");
            }
        }

        public static async Task<IAsyncEnumerable<VectorSearchResult<SkMemoryRecord>>> EmbeddingStoreToPGVectorNSearchAsync(Kernel kernel, string userQuery)
        {
            // Connection string to your PostgreSQL database
            var connStr = "Host=localhost;Port=5434;Username=postgres;Password=password;Database=postgres";

            NpgsqlDataSourceBuilder dataSourceBuilder = new(connStr);
            dataSourceBuilder.UseVector();
            NpgsqlDataSource dataSource = dataSourceBuilder.Build();

            // create postgres vector store
            var collection = new PostgresCollection<string, SkMemoryRecord>(dataSource, "sk_memory", ownsDataSource: true);

            // Demo data to insert (with dynamic embeddings)
            var demoFacts = new[]
            {
                new
                {
                    Id = "fact1",
                    Data = "My name is Robi and I am a .NET developer.",
                    Metadata = new Dictionary<string, object?>
                    {
                        ["source"] = "system",
                        ["tags"] = new[] { "developer", "intro" }
                    }
                },
                new
                {
                    Id = "fact2",
                    Data = "I love working with Semantic Kernel.",
                    Metadata = new Dictionary<string, object?>
                    {
                        ["source"] = "system",
                        ["tags"] = new[] { "semantic-kernel", "work" }
                    }
                },
                new
                {
                    Id = "fact3",
                    Data = "I enjoy swimming and play zones with my child.",
                    Metadata = new Dictionary<string, object?>
                    {
                        ["source"] = "system",
                        ["tags"] = new[] { "hobby", "family" }
                    }
                }
            };

            // Get embedding service
            var embeddingService = kernel.Services.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();

            // Insert demo data with generated embeddings
            foreach (var fact in demoFacts)
            {
                var embedding = (await embeddingService.GenerateVectorAsync(
                        $"This data: {fact.Data} stats fact which have these metadata tags: {fact.Metadata["tags"]} "
                    )).ToArray();
                var record = new SkMemoryRecord
                {
                    Id = fact.Id,
                    Data = fact.Data,
                    Embedding = embedding,
                    Metadata = fact.Metadata
                };
                await collection.UpsertAsync(record);
            }


            var queryEmbedding = (await embeddingService.GenerateVectorAsync(userQuery)).ToArray();

            // Search the vector store for relevant facts (top 2)

            return collection.SearchAsync(
                queryEmbedding,
                top: 1
            );
        }
    }
}
