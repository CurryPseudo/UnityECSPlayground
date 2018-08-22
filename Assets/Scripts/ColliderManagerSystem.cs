using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Transforms2D;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System;

public class ColliderManagerSystem: JobComponentSystem
{
    [BurstCompile]
    struct ColliderManagerCircleCollider : IJobParallelFor
    {
        public Group CollidersGroup;
        public ComponentDataArray<ColliderManager> Managers;
        public int CurrentUpdateManager;

        public void Execute(int index)
        {
            var halfSize = CollidersGroup.CircleColliders[index].Radius * new float2(1, 1);
            var origin = CollidersGroup.Position2Ds[index].Value - halfSize; 
            var end = CollidersGroup.Position2Ds[index].Value + halfSize;
            var min = f2Toi2(origin);
            var max = f2Toi2(end);
            for(int x = min.x; x < max.x; x++) {
                for(int y = min.y; y < max.y; y++) {
                    Managers[CurrentUpdateManager][new int2(x, y)].Add(CollidersGroup.Entities[index]);
                }
            }
        }
        private int2 f2Toi2(float2 f2) {
            var v = new Vector2(f2.x, f2.y);
            v -= Managers[CurrentUpdateManager].edge.min;
            v.x *= Managers[CurrentUpdateManager].size.x / Managers[CurrentUpdateManager].edge.size.x;
            v.y *= Managers[CurrentUpdateManager].size.y / Managers[CurrentUpdateManager].edge.size.y;
            return new int2(v);
        }
    }
    struct Group {
        [ReadOnly]
        public ComponentDataArray<CircleCollider> CircleColliders;
        [ReadOnly]
        public ComponentDataArray<Position2D> Position2Ds;
        [ReadOnly]
        public EntityArray Entities;
        [ReadOnly]
        public int Length;

    }
    [Inject]private Group collidersGroup;
    [Inject]private ComponentDataArray<ColliderManager> managers;
    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        for(int i = 0; i < managers.Length; i++) {
            var size = managers[i].size.x * managers[i].size.y;
            var list = managers[i].entities;
            while(list.Count > size) {
                list.RemoveAt(list.Count - 1);
            }
            list.Capacity = size;
            for(int j = 0; j < list.Count; j++) {
                list[i].Clear();
            }
            while(list.Count < size) {
                list.Add(new List<Entity>());
            }
        }
        for(int i = 0; i < managers.Length; i++) {
            var job = new ColliderManagerCircleCollider(){
                CollidersGroup = collidersGroup, 
                Managers = managers, 
                CurrentUpdateManager = i
            };
            inputDeps = job.Schedule(collidersGroup.Length, 64, inputDeps);
        }
        return inputDeps;
    }
}
