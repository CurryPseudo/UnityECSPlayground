using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Transforms2D;
using Unity.Jobs;
using Unity.Collections;

public class VelocitySystem : JobComponentSystem
{
    [BurstCompile]
    struct VelocityPosition : IJobProcessComponentData<Velocity, Position2D>
    {
        public float dt;
        public void Execute([ReadOnly]ref Velocity velocity, ref Position2D position)
        {
            position.Value += velocity.value * dt;
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        var job = new VelocityPosition() {dt = Time.deltaTime};
        return job.Schedule(this, 64, inputDeps);
    }
}