using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.Extensions.VectorData;

namespace KernelInfrastructure.Vector;

[Table("sk_memory")]
public class SkMemoryRecord
{
	[VectorStoreKey(StorageName = "id")]
	public string Id { get; set; }

	[VectorStoreData(StorageName = "data")]
	public string? Data { get; set; }

	//[VectorStoreVector(1024, StorageName = "embedding")]
	[VectorStoreVector(Dimensions: 1024, StorageName = "embedding", DistanceFunction = DistanceFunction.CosineDistance)]
	public float[]? Embedding { get; set; }

	[VectorStoreData(StorageName = "metadata")]
	public string? MetadataJson { get; set; }

	// Helper property for usage
	// Helper to access metadata as dictionary without exposing unsupported types to VectorData
	private Dictionary<string, object?>? _metadataCache;
	public Dictionary<string, object?>? Metadata
	{
		get
		{
			if (_metadataCache != null) return _metadataCache;
			if (string.IsNullOrEmpty(MetadataJson)) return null;

			_metadataCache = JsonSerializer.Deserialize<Dictionary<string, object?>>(MetadataJson);
			return _metadataCache;
		}
		set
		{
			_metadataCache = value;
			MetadataJson = value is null ? null : JsonSerializer.Serialize(value);
		}
	}
}
// ...existing code from HelloKernel/Vector/SkMemoryRecord.cs...
