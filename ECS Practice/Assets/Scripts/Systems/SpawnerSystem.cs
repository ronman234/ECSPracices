/*using System;
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

    SpawningBarrier m_Barrier;
    EntityCommandBuffer.Concurrent m_EntityCommandBuffer;
    public static EntityManager entityManger;
    public float size = 1.0f;
    //public NativeArray<Entity> tiles;
    EntityArchetype tileArchetype;
    Entity tile;
    BlobAssetReference<Unity.Physics.Collider> boxCollider;
    ComponentGroup m_tiles;

    protected override void OnStartRunning()
    {
        m_Barrier = World.GetExistingManager<SpawningBarrier>();
        m_EntityCommandBuffer = m_Barrier.CreateCommandBuffer().ToConcurrent();
        entityManger = World.Active.GetOrCreateManager<EntityManager>();
        Debug.Log("Tile Srchetypeing");
        tileArchetype = EntityManager.CreateArchetype(typeof(GridData),
                                                      typeof(Translation),
                                                      typeof(PhysicsCollider));
        //commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer();
        tile = entityManger.CreateEntity(tileArchetype);
        boxCollider = Unity.Physics.BoxCollider.Create(float3.zero,
                                                        new quaternion(0, 0, 0, 1),
                                                        new float3(1, 1, 1), 1);


        var spawnJob = new SpawningJob
        {
            e = tile,
            cb = m_EntityCommandBuffer,
            collider = boxCollider

        };

        m_tiles = GetComponentGroup(typeof(GridData), typeof(Translation), typeof(PhysicsCollider));
        NativeArray<Translation> m_tilePos = new NativeArray<Translation>(m_tiles.CalculateLength(), Allocator.Temp);

        var xPosJob = new PositionXJob { };
        var zPosJob = new PositionZJob { };

        JobHandle job1 = spawnJob.Schedule(100, 64);

        JobHandle job2 = xPosJob.Schedule(this, job1);

        JobHandle job3 = zPosJob.Schedule(this, job2);

        m_Barrier.AddJobHandleForProducer(job1);

        job1.Complete();

        World.Active.DestroyManager(m_Barrier);
    }

    [BurstCompile]
    public struct SpawningJob : IJobParallelFor
    {
        [ReadOnly] public Entity e;
        [ReadOnly] public EntityCommandBuffer.Concurrent cb;
        [ReadOnly] public BlobAssetReference<Unity.Physics.Collider> collider;

        public void Execute(int index)
        {
            //Debug.Log("Instantiating Tiles");

            cb.Instantiate(index, e);

            cb.SetComponent(index, e, new PhysicsCollider { Value = collider });

            //Debug.Log("Finished Tile number: " + index);
        }
    }

    public struct PositionXJob : IJobProcessComponentDataWithEntity<GridData, Translation>
    {
        [WriteOnly] NativeArray<Translation> tilePositions;

        public void Execute([ReadOnly]Entity e, int i, [ReadOnly]ref GridData g, ref Translation t)
        {
            if (i < 10)
            {
                t.Value.x = (float)i;
            }
            if (i > 10 && i < 20)
            {
                t.Value.x = (float)i - 10;
            }
            if (i > 20 && i < 30)
            {
                t.Value.x = (float)i - 20;
            }
            if (i > 30 && i < 40)
            {
                t.Value.x = (float)i - 30;
            }
            if (i > 40 && i < 50)
            {
                t.Value.x = (float)i - 40;
            }
            if (i > 50 && i < 60)
            {
                t.Value.x = (float)i - 50;
            }
            if (i > 60 && i < 70)
            {
                t.Value.x = (float)i - 60;
            }
            if (i > 70 && i < 80)
            {
                t.Value.x = (float)i - 70;
            }
            if (i > 80 && i < 90)
            {
                t.Value.x = (float)i - 80;
            }
            if (i > 90 && i < 100)
            {
                t.Value.x = (float)i - 90;
            }
        }
    }

    public struct PositionZJob : IJobProcessComponentDataWithEntity<GridData, Translation>
    {

        public void Execute([ReadOnly]Entity e, int i, [ReadOnly]ref GridData g, ref Translation t)
        {
            if (i < 10)
            {
                t.Value.z = (float)i;
            }
            if (i > 10 && i < 20)
            {
                t.Value.z = (float)i - 10;
            }
            if (i > 20 && i < 30)
            {
                t.Value.z = (float)i - 20;
            }
            if (i > 30 && i < 40)
            {
                t.Value.z = (float)i - 30;
            }
            if (i > 40 && i < 50)
            {
                t.Value.z = (float)i - 40;
            }
            if (i > 50 && i < 60)
            {
                t.Value.z = (float)i - 50;
            }
            if (i > 60 && i < 70)
            {
                t.Value.z = (float)i - 60;
            }
            if (i > 70 && i < 80)
            {
                t.Value.z = (float)i - 70;
            }
            if (i > 80 && i < 90)
            {
                t.Value.z = (float)i - 80;
            }
            if (i > 90 && i < 100)
            {
                t.Value.z = (float)i - 90;
            }
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }
}*/