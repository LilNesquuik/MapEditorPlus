using UnityEngine;

namespace ProjectMER.Features.Serializable.Schematics;

public class SchematicObjectDataList
{
	public string Path;

	public int RootObjectId { get; set; }
	
	public Vector3 CullingPosition { get; set; } = Vector3.zero;
	
	public Vector3 CullingSize { get; set; } = Vector3.one * 5000f;

	public List<SchematicBlockData> Blocks { get; set; } = [];
}
