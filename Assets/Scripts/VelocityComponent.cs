﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
struct Velocity : IComponentData{
	public float2 Value;
}

class VelocityComponent : ComponentDataWrapper<Velocity> {}
