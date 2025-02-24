/*
Bake AO - Easy Ambient Occlusion Baking - A plugin for baking ambient occlusion (AO) textures in the Unity Editor.
by Procedural Pixels - Jan Mróz

Documentation: https://proceduralpixels.com/BakeAO/Documentation
Asset Store: https://assetstore.unity.com/packages/slug/263743 

Help: If the plugin is not working correctly, if there’s a bug, or if you need assistance and the documentation does not help, please contact me via Discord (https://discord.gg/NT2pyQ28Jx) or email (dev@proceduralpixels.com).
*/

﻿using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static ProceduralPixels.BakeAO.Editor.BakeAOUtils;

namespace ProceduralPixels.BakeAO.Editor
{
    [CustomEditor(typeof(GenericBakingSetup)), CanEditMultipleObjects]
    internal class GenericBakingSetupEditor : UnityEditor.Editor
    {
        public SerializedProperty uvChannelProperty { get; private set; }
        public SerializedProperty targetSubmeshFlagsProperty { get; private set; }
        public SerializedProperty occluderSubmeshFlagsProperty { get; private set; }
        public SerializedProperty qualityProperty {get; private set; }
        public SerializedProperty contextBakingSettingsProperty { get; private set; }
        public SerializedProperty filePathProperty {get; private set; }
        public SerializedProperty meshFolderFallbackProperty {get; private set; }
        public SerializedProperty textureAssetPostprocessActionProperty {get; private set; }
        public SerializedProperty multiTargetBakingOptionProperty { get; private set; }
        public SerializedProperty bakeAOComponentOptionProperty { get; private set; }
        public bool isPathValid { get; private set; }

        private bool isInitialized = false;
        private Renderer rendererContext = null;

        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                uvChannelProperty = serializedObject.FindProperty("uvChannel");
                targetSubmeshFlagsProperty = serializedObject.FindProperty("targetSubmeshFlags");
                occluderSubmeshFlagsProperty = serializedObject.FindProperty("occluderSubmeshFlags");
                qualityProperty = serializedObject.FindProperty("quality");
                contextBakingSettingsProperty = serializedObject.FindProperty("contextBakingSettings");
                filePathProperty = serializedObject.FindProperty("filePath");
                meshFolderFallbackProperty = serializedObject.FindProperty("meshFolderFallback");
                textureAssetPostprocessActionProperty = serializedObject.FindProperty("textureAssetPostprocessAction");
                multiTargetBakingOptionProperty = serializedObject.FindProperty("multiTargetBakingOption");
                bakeAOComponentOptionProperty = serializedObject.FindProperty("bakeAOComponentOption");
                isInitialized = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception _)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // TODO: Investigate this - this sometimes produces an internal unity exception, which can be catched by a type
                // So unity thinks that the given serialized object is for some reason not creatable.
                // This happens on assembly reload. When ignoring this error, everything works fine...
                isInitialized = false;
            }
        }

        public void DrawPostprocessGUI(bool drawTextureAssetPostprocessAction, bool drawMultiTargetBakingOptionProperty, bool drawBakeAOComponentOptionProperty)
        {
            if (drawTextureAssetPostprocessAction)
                EditorGUILayout.PropertyField(textureAssetPostprocessActionProperty);
            if (drawMultiTargetBakingOptionProperty)
                EditorGUILayout.PropertyField(multiTargetBakingOptionProperty);
            if (drawBakeAOComponentOptionProperty)
                EditorGUILayout.PropertyField(bakeAOComponentOptionProperty);
        }

        public override void OnInspectorGUI()
        {
            Initialize();
            if (!isInitialized)
                return;

            isPathValid = true;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(uvChannelProperty, false);

            if (BakeAOPreferences.instance.showSubmeshSelectionInBakingSettings)
            {
                DrawSubmeshFlagsProperty(targetSubmeshFlagsProperty, rendererContext);
                DrawSubmeshFlagsProperty(occluderSubmeshFlagsProperty, rendererContext);
            }
            else
            {
                targetSubmeshFlagsProperty.longValue = -1;
                filePathProperty.serializedObject.ApplyModifiedProperties();
                filePathProperty.serializedObject.Update();
            }    

            EditorGUILayout.PropertyField(qualityProperty, true);
            EditorGUILayout.PropertyField(contextBakingSettingsProperty, true);

            Rect controlRect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);
            PropertyFieldWithButton(controlRect, filePathProperty, "Browse", 60, () =>
            {
                var folderPath = EditorUtility.OpenFolderPanel("Select folder to bake to", "Assets", "AO Textures");
                if (string.IsNullOrEmpty(folderPath))
                    return;

                folderPath += $"{Path.DirectorySeparatorChar}{PathResolver.MeshName}_AO.png";

                string projectFolderPath;

                if (PathUtils.TryGetProjectPathFromAbsolutePath(folderPath, out projectFolderPath))
                {
                    filePathProperty.stringValue = projectFolderPath;
                    filePathProperty.serializedObject.ApplyModifiedProperties();
                    filePathProperty.serializedObject.Update();
                }
                else
                {
                    Debug.LogError("Not a project folder, you can only save textures into the current project");
                }

                this.Repaint();
            });

            int invalidCharIndex = -1;
            bool notInAssets = false;
            if (!filePathProperty.hasMultipleDifferentValues)
            {
                if (!PathResolver.ValidateTemplate(filePathProperty.stringValue, out invalidCharIndex, out notInAssets))
                {
                    isPathValid = false;
                    if (invalidCharIndex >= 0)
                        EditorGUILayout.HelpBox($"File path contains invalid character at position {invalidCharIndex}: {filePathProperty.stringValue[invalidCharIndex]}", MessageType.Error, true);
                    if (notInAssets)
                        EditorGUILayout.HelpBox($"File path needs to be in the Asset folder.", MessageType.Error, true);

                    EditorGUILayout.HelpBox($"Available parameters:\n" +
                        $"{PathResolver.AssetName} - Name of the baked asset, if it's serialized in the project folder.\n" +
                        $"{PathResolver.GameObjectName} - Name of the GameObject of the BakeAO component.\n" +
                        $"{PathResolver.MeshName} - Name of the baked mesh.\n" +
                        $"{PathResolver.MeshFolder} - Folder where the baked mesh is saved. If the mesh in not saved in the project folder, fallback path will be used.", MessageType.Info, true);
                }
            }
            else
            {
                bool anyObjectHasInvalidPath = targets.Any(t => !PathResolver.ValidateTemplate((t as GenericBakingSetup).filePath, out int _, out bool _));

                if (anyObjectHasInvalidPath)
                {
                    isPathValid = false;
                    EditorGUILayout.HelpBox($"One of the selected objects contains invalid path.", MessageType.Error, true);
                }
            }

            controlRect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);
            PropertyFieldWithButton(controlRect, meshFolderFallbackProperty, "Browse", 60, () =>
            {
                var folderPath = EditorUtility.OpenFolderPanel("Select folder to bake to", "Assets", "AO Textures");
                if (string.IsNullOrEmpty(folderPath))
                    return;

                string projectFolderPath;

                if (PathUtils.TryGetProjectPathFromAbsolutePath(folderPath, out projectFolderPath))
                {
                    meshFolderFallbackProperty.stringValue = projectFolderPath;
                    meshFolderFallbackProperty.serializedObject.ApplyModifiedProperties();
                    meshFolderFallbackProperty.serializedObject.Update();
                }
                else
                    Debug.LogError("Not a project folder, you can only save textures into the current project");

                this.Repaint();
            });

            if (!meshFolderFallbackProperty.hasMultipleDifferentValues)
            {
                if (!PathResolver.ValidatePath(meshFolderFallbackProperty.stringValue, out invalidCharIndex, out notInAssets))
                {
                    isPathValid = false;
                    if (invalidCharIndex >= 0)
                        EditorGUILayout.HelpBox($"Mesh fallback path contains invalid character at position {invalidCharIndex}: {meshFolderFallbackProperty.stringValue[invalidCharIndex]}", MessageType.Error, true);
                    if (notInAssets)
                        EditorGUILayout.HelpBox($"File path needs to be in the Asset folder.", MessageType.Error, true);
                }
            }
            else
            {
                bool anyObjectHasInvalidPath = targets.Any(t => !PathResolver.ValidateTemplate((t as GenericBakingSetup).meshFolderFallback, out int _, out bool _));

                if (anyObjectHasInvalidPath)
                {
                    isPathValid = false;
                    EditorGUILayout.HelpBox($"One of the selected objects contains invalid fallback path.", MessageType.Error, true);
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
        }

        string[] flagDisplayValues;

        private void DrawSubmeshFlagsProperty(SerializedProperty property, Renderer context)
        {
            Mesh mesh = BakeAOUtils.FirstOrDefaultMesh(context);

            if (mesh != null && context != null)
            {
                Material[] materials = context.sharedMaterials; 
                if (flagDisplayValues == null || flagDisplayValues.Length != mesh.subMeshCount)
                    flagDisplayValues = new string[mesh.subMeshCount];

                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    flagDisplayValues[i] = i.ToString();
                    if (i < materials.Length && materials[i] != null)
                        flagDisplayValues[i] = $"{i}: {materials[i].name}";
                }
            }
            else
            {
                flagDisplayValues = new string[32];
                for (int i = 0; i < 32; i++)
                    flagDisplayValues[i] = i.ToString();
            }

            property.longValue = EditorGUILayout.MaskField(property.displayName, (int)property.longValue, flagDisplayValues);
        }

        public void SetContext(Renderer rendererContext)
        {
            this.rendererContext = rendererContext;
        }
    }
}