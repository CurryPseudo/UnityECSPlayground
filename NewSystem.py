import sys
path = "Assets/Scripts/"
content = """using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Transforms2D;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

public class {0}: JobComponentSystem
{{
    [BurstCompile]
    struct {1} : IJobProcessComponentData<{2}>
    {{
        public void Execute({3})
        {{
        }}
    }}
    protected override JobHandle OnUpdate(JobHandle inputDeps) {{
        var job = new {1}(){{}};
        return job.Schedule(this, 64, inputDeps);
    }}
}}
"""
system_name = "{}System".format(sys.argv[1])
components = sys.argv[2:]

def head_lower(s):
    l = list(s)
    l[0] = l[0].lower()
    return "".join(l)

def to_struct_name(components):
    return "".join(components)

struct_name = to_struct_name(components)


def to_template_args(components):
    return ", ".join(components)

template_args = to_template_args(components)

def to_execute_args(components):
    for i in range(len(components)):
        c = components[i]
        components[i] = "ref {0} {1}".format(c, head_lower(c))
    return ", ".join(components)
execute_args = to_execute_args(components);

fullContent = content.format(system_name, struct_name, template_args, execute_args)

pathFormat = "{0}{1}.cs".format(path, system_name)
f = open(pathFormat, "w+")
f.write(fullContent)
