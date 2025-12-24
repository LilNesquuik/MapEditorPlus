using UnityEngine;

namespace ProjectMER.Features.Objects;

public class LinkObject : MonoBehaviour
{
    public static readonly List<LinkObject> Instances = [];

    public void Awake() => Instances.Add(this);
    
    public void OnDestroy() => Instances.Remove(this);
    
    public static bool TryGetClosestLinkObject(Vector3 position, out LinkObject linkObject)
    {
        linkObject = null!;
        
        foreach (LinkObject instance in Instances)
        {
            if (Vector3.Distance(instance.transform.position, position) > 0.35f)
                continue;

            linkObject = instance;
        }
        
        return linkObject != null;
    }
}