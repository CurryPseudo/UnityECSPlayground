import sys
path = "Assets/Scripts/"
content = """using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
struct {0}: IComponentData{{
}}

class {0}Component : ComponentDataWrapper<{0}> {{}}
"""
component_name = sys.argv[1]
fullContent = content.format(component_name)
filePath = "{0}{1}Component.cs".format(path, component_name)
f = open(filePath, "w")
f.write(fullContent)