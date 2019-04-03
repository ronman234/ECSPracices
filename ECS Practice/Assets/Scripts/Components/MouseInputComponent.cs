using System; 
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct MouseInput : IComponentData
{
    public blittablebool leftClick;
    public blittablebool rightClick;
    public float3 MousePosition;
}

[UnityEngine.DisallowMultipleComponent]
public class MouseInputComponent : ComponentDataProxy<MouseInput> { }
