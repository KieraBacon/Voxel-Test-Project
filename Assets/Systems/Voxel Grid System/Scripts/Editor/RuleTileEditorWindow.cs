// Credit to Game Dev Guide - https://www.youtube.com/watch?v=c_3DXBrH-Is.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace VoxelGridSystem
{
    public class RuleTileEditorWindow : ExtendedEditorWindow
    {
        private static RuleTileEditorWindow GetWindow()
        {
            return GetWindow<RuleTileEditorWindow>("Rule Tile Editor");
        }

        public static void ShowWindow(RuleTile ruleTile)
        {
            RuleTileEditorWindow window = GetWindow();
            window.serializedObject = ruleTile != null ? new SerializedObject(ruleTile) : null;
        }

        [MenuItem("Window/Voxel Tile System/Rule Tile Editor")]
        public static void ShowWindow()
        {
            ShowWindow(Selection.activeObject as RuleTile);
        }

        [OnOpenAsset()]
        public static bool ShowWindow(int instanceId, int line)
        {
            RuleTile obj = EditorUtility.InstanceIDToObject(instanceId) as RuleTile;
            if (obj != null)
            {
                RuleTileEditorWindow window = GetWindow();
                window.serializedObject = new SerializedObject(obj);
                return true;
            }
            return false;
        }

        private void OnSelectionChange()
        {
            RuleTile ruleTile = Selection.activeObject as RuleTile;
            serializedObject = ruleTile != null ? new SerializedObject(ruleTile) : null;

            Repaint();
        }

        private void OnGUI()
        {
            if (serializedObject == null) return;

            //SerializedProperty it = serializedObject.GetIterator();
            //it.Next(true);
            //
            //while (it.NextVisible(false))
            //{
            //    if (it.name != "m_Script")
            //        EditorGUILayout.PropertyField(it);
            //}

            Editor editor = Editor.CreateEditor(serializedObject.targetObject);
            Editor.DrawFoldoutInspector(serializedObject.targetObject, ref editor);
        }
    }
}
