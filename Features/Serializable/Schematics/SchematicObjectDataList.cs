namespace ProjectMER.Features.Serializable.Schematics;

public class SchematicObjectDataList
{
	public string? Author { get; set; }
	
	public string Path;

	public int RootObjectId { get; set; }

	public List<SchematicBlockData> Blocks { get; set; } = [];
}
