using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Rendering;


public class GridSystem : JobComponentSystem
{
    public class SpawningBarrier : EntityCommandBufferSystem { };

    public SpawningBarrier m_Barrier;
    public EntityCommandBuffer m_EntityCommandBuffer;

    public Entity tile;

    protected override void OnCreateManager()
    {
        m_Barrier = World.GetExistingManager<SpawningBarrier>();
    }

    protected override void OnStartRunning()
    {
        m_EntityCommandBuffer = m_Barrier.CreateCommandBuffer();

        EntityArchetype tileArch = EntityManager.CreateArchetype(typeof(GridData), typeof(Translation), typeof(LocalToWorld));

        tile = m_EntityCommandBuffer.CreateEntity(tileArch);

        var spawnJob = new SpawnTiles
        {
            commandBuffer = m_EntityCommandBuffer,
            tile = tile
        };
        JobHandle spawnJobHandle = spawnJob.Schedule(this);

        m_Barrier.AddJobHandleForProducer(spawnJobHandle);

        spawnJobHandle.Complete();

        m_EntityCommandBuffer.DestroyEntity(tile);
    }

    struct SpawnTiles :IJobProcessComponentDataWithEntity<GridData, Translation, LocalToWorld>
    {
        public EntityCommandBuffer commandBuffer;
        public Entity tile;

        public void Execute(Entity e, int index, [ReadOnly] ref GridData g, ref Translation t, [ReadOnly] ref LocalToWorld ltw)
        {
            for(float x = 0; x < 100; x++ )
            {
                for(float z = 0; z < 100; z++)
                {
                    var instance = commandBuffer.Instantiate(tile);

                    commandBuffer.SetComponent(instance, new Translation { Value = new float3(x, 0, z) });
                }
            }

            commandBuffer.DestroyEntity(e);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    { 

        return inputDeps;
    }


}
