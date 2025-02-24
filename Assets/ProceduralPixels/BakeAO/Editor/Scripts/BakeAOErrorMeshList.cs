/*
Bake AO - Easy Ambient Occlusion Baking - A plugin for baking ambient occlusion (AO) textures in the Unity Editor.
by Procedural Pixels - Jan Mróz

Documentation: https://proceduralpixels.com/BakeAO/Documentation
Asset Store: https://assetstore.unity.com/packages/slug/263743 

Help: If the plugin is not working correctly, if there’s a bug, or if you need assistance and the documentation does not help, please contact me via Discord (https://discord.gg/NT2pyQ28Jx) or email (dev@proceduralpixels.com).
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ProceduralPixels.BakeAO.Editor
{
    internal class BakeAOErrorMeshList : UnityEditor.EditorWindow
    {
        public static BakeAOErrorMeshList instance;

        [System.Serializable]
        public struct ErrorData
        {
            public string reason;
            public UnityEngine.Object obj;

            public ErrorData(string reason, UnityEngine.Object obj)
            {
                this.reason = reason;
                this.obj = obj;
            }
        }

        public List<ErrorData> errors;
        private GUIRecycledList guiList;

        public static void ShowWindow(IEnumerable<ErrorData> errors)
        {
            if (instance == null)
            {
                BakeAOErrorMeshList window = GetWindow<BakeAOErrorMeshList>(true, "Bake AO Errors", true);
                instance = window;
            }
            else
                instance.Focus();

            instance.errors = errors.ToList();
            instance.minSize = new Vector2(500, 500);
            instance.maxSize = new Vector2(1000, 500);
        }

        private void OnEnable()
        {
            instance = this;
        }

        private void OnDisable()
        {
            if (instance == this)
                instance = null;
            else
                throw new Exception("More than one instance of error window!");
        }

        private void OnGUI()
        {
            if (guiList == null)
                guiList = new GUIRecycledList(EditorGUIUtility.singleLineHeight, DrawErrorElement, GetErrorsCount);

            EditorGUILayout.HelpBox("The list below contains all the models that failed the baking.", MessageType.Error, true);
            Rect rect = EditorGUILayout.GetControlRect(false, 456);
            guiList.Draw(rect);

            Repaint();
        }

        private int GetErrorsCount()
        {
            return errors.Count();
        }

        private void DrawErrorElement(int index, Rect rect)
        {
            var error = errors[index];
            Rect fieldRect = new Rect(rect.x + 300, rect.y, rect.width - 300, rect.height);
            Rect messageRect = new Rect(rect.x, rect.y, 300, rect.height);
            EditorGUI.ObjectField(fieldRect, error.obj, typeof(UnityEngine.Object), true);
            EditorGUI.LabelField(messageRect, error.reason);
        }

        public static void AddError(ErrorData errorData)
        {
            if (instance == null)
                ShowWindow(new ErrorData[] { errorData });

            if (!instance.errors.Contains(errorData))
                instance.errors.Add(errorData);
        }
    }
}