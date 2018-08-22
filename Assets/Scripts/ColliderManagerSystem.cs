using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Transforms2D;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class ColliderManagerSystem: JobComponentSystem
{
    [BurstCompile]
    struct ColliderManagerCircleCollider : IJobParallelFor
    {
        
        public void Execute(int index)
        {

        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps) {
    }
}
