using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VoxelGridSystem
{
    public class ExtendedEditorWindow : EditorWindow
    {
        protected SerializedObject serializedObject;

        protected void DrawProperties(SerializedProperty property, bool drawChildren)
        {
            string lastPropertyPath = string.Empty;
            foreach (SerializedProperty childProperty in property)
            {
                if (childProperty.isArray && childProperty.propertyType == SerializedPropertyType.Generic)
                {
                    EditorGUILayout.BeginHorizontal();
                    childProperty.isExpanded = EditorGUILayout.Foldout(childProperty.isExpanded, childProperty.displayName);
                    EditorGUILayout.EndHorizontal();
                    
                    if (childProperty.isExpanded)
                    {
                        EditorGUI.indentLevel++;
                        DrawProperties(childProperty, drawChildren);
                        EditorGUI.indentLevel--;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(lastPropertyPath) && childProperty.propertyPath.Contains(lastPropertyPath)) { continue; }
                    lastPropertyPath = childProperty.propertyPath;
                    EditorGUILayout.PropertyField(childProperty, drawChildren);
                }
            }
        }
    }
}
