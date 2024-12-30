using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class DynamicSquadUtils
{
    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static void Shuffle<T>(this List<T> list)
    {
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
    }

    public static bool TryToEnum<T>(this string value, out T result)
    {
        if (Enum.TryParse(typeof(T), value, true, out object objResult))
        {
            result = (T)objResult;
            return true;
        }

        result = default;
        return false;
    }
}