using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.Embeddings;

using System.Collections.Concurrent;
using System.Numerics;

namespace HelloKernel.Vector;
public class InMemoryVectorStore
{
    private readonly ConcurrentDictionary<string, (string text, ReadOnlyMemory<float> vector)> _store
        = new();

    private readonly IEmbeddingGenerator<string, Embedding<float>> _embeddings;

    public InMemoryVectorStore(IEmbeddingGenerator<string, Embedding<float>> embeddings)
    {
        _embeddings = embeddings;
    }

    public async Task SaveAsync(string key, string text)
    {
        var vector = await _embeddings.GenerateVectorAsync(text);
        _store[key] = (text, vector);
    }

    public async Task<List<(string text, double score)>> SearchAsync(string query, int topK = 3)
    {
        var queryVector = await _embeddings.GenerateVectorAsync(query);
        return _store.Values
            .Select(v => (v.text, CosineSimilarity(v.vector.Span, queryVector.Span)))
            .OrderByDescending(r => r.Item2)
            .Take(topK)
            .ToList();
    }

    private static double CosineSimilarity(ReadOnlySpan<float> v1, ReadOnlySpan<float> v2)
    {
        var dot = 0.0;
        var norm1 = 0.0;
        var norm2 = 0.0;
        for (int i = 0; i < v1.Length; i++)
        {
            dot += v1[i] * v2[i];
            norm1 += v1[i] * v1[i];
            norm2 += v2[i] * v2[i];
        }
        return dot / (Math.Sqrt(norm1) * Math.Sqrt(norm2));
    }
}
