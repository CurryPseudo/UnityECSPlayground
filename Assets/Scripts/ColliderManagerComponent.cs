using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
struct ColliderManager: IComponentData{
    public Rect edge;
    public int2 size;
    public List<List<Entity>> entities;
    private int posIndex(int2 pos) {
        return pos.x + pos.y * size.x;
    }
    public List<Entity> this[int2 pos] {
        get {
            return entities[posIndex(pos)];
        }
        set {
            entities[posIndex(pos)] = value;
        }
    }
}

class ColliderManagerComponent : ComponentDataWrapper<ColliderManager> {}
