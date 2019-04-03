using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct GridData : IComponentData
{
    public int flowData;
}


[UnityEngine.DisallowMultipleComponent]
public class GridDataComponent : ComponentDataProxy<GridData> { }
