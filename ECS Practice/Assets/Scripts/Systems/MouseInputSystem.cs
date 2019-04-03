using System;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Jobs;
using Unity.Physics;

public class MouseInputSystem : JobComponentSystem
{

    [BurstCompile]
    struct MouseInputJob: IJobProcessComponentData<MouseInput>
    {
        public blittablebool leftClick;
        public blittablebool rightClick;
        public float3 mousePosition;

        public void Execute(ref MouseInput mi)
        {
            mi.leftClick = leftClick;
            mi.rightClick = rightClick;
            mi.MousePosition = mousePosition;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var physicsWorldSystem = Unity.Entities.World.Active.GetExistingManager<Unity.Physics.Systems.BuildPhysicsWorld>();
        var collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;

        var mousePostion = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(mousePostion);
        var direction = ray.direction - mousePostion;

        RaycastInput input = new RaycastInput()
        {
            Ray = new Unity.Physics.Ray()
            {
                Origin = mousePostion,
                Direction = direction
            },
            Filter = new CollisionFilter()
            {
                CategoryBits = ~0u,
                MaskBits = ~0u,
                GroupIndex = 0
            }
        };
        Unity.Physics.RaycastHit hit;
        bool havehit = collisionWorld.CastRay(input, out hit);
        if(havehit)
        {
            mousePostion = new float3(hit.Position.x, 0, hit.Position.z);
            //Debug.Log("Hit postion" + hit.Position.x + " , 0, " + hit.Position.z);
        }

        var job = new MouseInputJob
        {
            leftClick = Input.GetMouseButtonDown(0),
            rightClick = Input.GetMouseButtonDown(1),
            mousePosition = mousePostion
        };

        return job.Schedule(this, inputDeps);
    }
}
