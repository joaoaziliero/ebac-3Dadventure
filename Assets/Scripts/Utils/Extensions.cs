using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class Extensions
{
    public static IEnumerable<Type> GetTypesWithAttribute<T>(this Assembly assembly)
        where T : Attribute
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsDefined(typeof(T), false))
            {
                yield return type;
            }
        }
    }
}
