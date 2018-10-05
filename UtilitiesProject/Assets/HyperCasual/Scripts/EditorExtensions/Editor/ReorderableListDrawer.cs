using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace HyperCasual
{
    public class ReorderableListDrawer
    {
        string label;
        public UnityEditorInternal.ReorderableList List {
            get;
            private set;
        }

        public ReorderableListDrawer (SerializedObject obj, string propertyName, string label)
        {
            CreateListWithObject (obj, propertyName, label);
        }
        public ReorderableListDrawer (SerializedObject obj, string propertyName)
        {
            CreateListWithObject (obj, propertyName, string.Empty);
        }
        void CreateListWithObject (SerializedObject obj, string propertyName, string label)
        {
            var property = obj.FindProperty (propertyName);
            this.label = string.IsNullOrEmpty (label) ? property.displayName : label;

            List = new ReorderableList (obj, property);
            List.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField (rect, List.count.ToString (), EditorExtensions.GetLabelGUIStyle (TextAnchor.MiddleRight));
                rect.xMin += 10;
                property.isExpanded = EditorGUI.Foldout (rect, property.isExpanded, this.label, true);
            };
            List.drawElementCallback += (Rect rect, int index, bool selected, bool focused) => {
                if (!property.isExpanded) {
                    GUI.enabled = index == List.count;
                    return;
                }
                rect.height -= 4;
                rect.y += 2;
                SerializedProperty element = List.serializedProperty.GetArrayElementAtIndex (index);
                EditorGUI.PropertyField (rect, element, GUIContent.none);
            };
            List.elementHeightCallback = (int indexer) => {
                if (!property.isExpanded)
                    return 0;
                else
                    return List.elementHeight;
            };
        }

        public ReorderableListDrawer (IList elements, System.Type type, string label)
        {
            this.label = label;
            List = new ReorderableList (elements, type);
            List.drawElementCallback += (Rect rect, int index, bool selected, bool focused) => {
                rect.height -= 4;
                rect.y += 2;
                SerializedProperty property = List.serializedProperty.GetArrayElementAtIndex (index);
                EditorGUI.PropertyField (rect, property, GUIContent.none);
            };
            List.drawHeaderCallback += (rect) => {
                rect.height = 0;
            };
        }

        public void DoLayout ()
        {
            List.DoLayoutList ();
        }
    }
}