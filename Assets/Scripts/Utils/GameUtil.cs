using UnityEngine;

public static class GameUtil
{
    public static T GetOrAdd<T>(GameObject go)  where T : Component
    {
        var t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }

        return t;
    }
}