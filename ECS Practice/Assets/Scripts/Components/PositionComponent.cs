using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Position : IComponentData
{
    public float3 position;
}

[UnityEngine.DisallowMultipleComponent]
public class PositionComponent : ComponentDataProxy<Position> { }
