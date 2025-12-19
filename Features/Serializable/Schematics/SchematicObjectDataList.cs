using UnityEngine;

namespace ProjectMER.Features.Serializable.Schematics;

public class SchematicObjectDataList
{
	public string? Author { get; set; }
	
	public string Path;

	public int RootObjectId { get; set; }
	
	public Vector3 CullingPosition { get; set; } = Vector3.zero;
	
	public Vector3 CullingBounds { get; set; } = Vector3.one * 5000f;

	public List<SchematicBlockData> Blocks { get; set; } = [];
}
