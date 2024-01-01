using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityFunctions
{
    public static TKey FindKeyByValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TValue value)
    {
        foreach (var entry in dictionary)
        {
            if (EqualityComparer<TValue>.Default.Equals(entry.Value, value))
            {
                return entry.Key;
            }
        }
        return default; // or throw an exception, or indicate failure some other way
    }
}
