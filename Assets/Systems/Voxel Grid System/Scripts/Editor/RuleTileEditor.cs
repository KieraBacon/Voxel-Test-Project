// Credit to Game Dev Guide - https://www.youtube.com/watch?v=c_3DXBrH-Is.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace VoxelGridSystem
{
    //public class RuleTileAssetHandler
    //{
    //    [OnOpenAsset()]
    //    public static bool OpenEditor(int instanceId, int line)
    //    {
    //        RuleTile obj = EditorUtility.InstanceIDToObject(instanceId) as RuleTile;
    //        if (obj != null)
    //        {
    //            RuleTileEditorWindow.ShowWindow();
    //            return true;
    //        }
    //        return false;
    //    }
    //}

    [CustomEditor(typeof(RuleTile))]
    public class RuleTileEditor : Editor
    {
        //public override VisualElement CreateInspectorGUI()
        //{
        //    VisualElement container = new VisualElement();
        //    
        //    SerializedProperty it = serializedObject.GetIterator();
        //    it.Next(true);
        //    
        //    while (it.NextVisible(false))
        //    {
        //        PropertyField property = new PropertyField(it);
        //        property.SetEnabled(it.name != "m_Script");
        //        container.Add(property);
        //    }
        //
        //    return container;
        //}

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
        
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultTile"));
        
            if (GUILayout.Button("Open Editor"))
            {
                RuleTileEditorWindow.ShowWindow((RuleTile)target);
            }
        }
    }
}
