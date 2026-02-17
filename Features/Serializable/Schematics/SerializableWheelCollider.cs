using UnityEngine;

namespace ProjectMER.Features.Serializable.Schematics;

public class SerializableWheelCollider
{
	public float Mass { get; set; }
	public float Radius { get; set; }
	public float DampingRate { get; set; }
	public float ForceApplicationPoint { get; set; }
	public Vector3 Center { get; set; }
	public JointSpring SuspensionSpring { get; set; }
	public WheelFrictionCurve ForwardFrictionSpring { get; set; }
	public WheelFrictionCurve SideFrictionSpring { get; set; }
}
