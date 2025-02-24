/*
Bake AO - Easy Ambient Occlusion Baking - A plugin for baking ambient occlusion (AO) textures in the Unity Editor.
by Procedural Pixels - Jan Mróz

Documentation: https://proceduralpixels.com/BakeAO/Documentation
Asset Store: https://assetstore.unity.com/packages/slug/263743 

Help: If the plugin is not working correctly, if there’s a bug, or if you need assistance and the documentation does not help, please contact me via Discord (https://discord.gg/NT2pyQ28Jx) or email (dev@proceduralpixels.com).
*/

﻿using System.Collections.Generic;
using UnityEngine;

namespace ProceduralPixels.BakeAO.Editor
{
    internal abstract class ScriptableMeshToBakeProvider : ScriptableObject, IProvideMeshesToBake
    {
        public abstract bool IsSetupCorrectly { get; }
        public abstract IReadOnlyList<MeshContext> MeshesToBake { get; }
    }
}