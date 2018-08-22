using Unity.Entities;
using UnityEngine;
using Unity.Collections;
using System;

[Serializable]
struct CircleCollider : IComponentData {
    public float radius;
}

class CircleColliderComponent : ComponentDataWrapper<CircleCollider>{}
