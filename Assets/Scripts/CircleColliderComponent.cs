using Unity.Entities;
using UnityEngine;
using Unity.Collections;
using System;

[Serializable]
struct CircleCollider : IComponentData {
    public float Radius;
}

class CircleColliderComponent : ComponentDataWrapper<CircleCollider>{}
