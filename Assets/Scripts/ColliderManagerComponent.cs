using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
struct ColliderManager: IComponentData{
    public Rect edge;
    public int2 size;
    public List<Entity> entities;
}

class ColliderManagerComponent : ComponentDataWrapper<ColliderManager> {}
