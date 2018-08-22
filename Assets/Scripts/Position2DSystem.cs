using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Transforms2D;
using Unity.Jobs;
using Unity.Collections;
using Unity.Transforms;

public class Position2DSystem: JobComponentSystem
{
    [BurstCompile]
    struct Position2DPosition : IJobProcessComponentData<Position2D, Position>
    {
        public void Execute([ReadOnly]ref Position2D position2D, ref Position position)
        {
            position.Value.x = position2D.Value.x;
            position.Value.y = position2D.Value.y;
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        var job = new Position2DPosition(){};
        return job.Schedule(this, 64, inputDeps);
    }
}
