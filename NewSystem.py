import sys
path = "Assets/Scripts/"
content = """using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Transforms2D;
using Unity.Jobs;
using Unity.Collections;

public class {0}System : JobComponentSystem
{
    [BurstCompile]
    struct {1} : IJobProcessComponentData<{2}>
    {
        public void Execute({3})
        {
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        var job = new {1}();
        return job.Schedule(this, 64, inputDeps);
    }
}
"""
system_name = sys.argv[1].lower()
components = sys.argv[1:]
for i in len(components):
    components[i] = components[i].lower()

def headUpper(s):
    l = list(s)
    l[0] = l[0].upper()
    return "".join(l)


