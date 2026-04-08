namespace ProjectMER.Features.Utils;

internal static class CacheUtils
{
    internal static bool IsCacheValid<T>(List<T> cache) where T : UnityEngine.Object
    {
        if (cache.Count == 0) 
            return false;
        
        foreach (T item in cache)
            if (item == null) 
                return false;
        
        return true;
    }

    internal static IReadOnlyList<T> GetOrRefresh<T>(List<T> cache, Action<List<T>> populate) where T : UnityEngine.Object
    {
        if (IsCacheValid(cache)) 
            return cache;
        
        cache.Clear();
        populate(cache);
        return cache;
    }
}