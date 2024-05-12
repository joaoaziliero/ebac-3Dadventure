#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class EnumGenerator : Metaprogramming
{
    private void Awake()
    {
        var mainLines = Assembly
            .GetExecutingAssembly()
            .GetTypesWithAttribute<StateFilterAttribute>()
            .Select(type => type.Name + ",");

        GenerateProgramWithNewCodeBlock(mainLines);
    }
}
#endif