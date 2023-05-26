using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// ‚æ‚­‚í‚©‚ç‚ñ‚¯‚Çwhere T : class’Ç‰Á
public static class Locator<T> where T : class
{
    private static Dictionary<int, T> instancesDic = new Dictionary<int, T>();

    public static void Bind(T instance, int id = 0)
    {
        instancesDic.Add(id, instance);
    }

    public static T GetT(int id = 0)
    {
        if (!instancesDic.ContainsKey(id)) return default;
        return instancesDic[id];
    }
}

