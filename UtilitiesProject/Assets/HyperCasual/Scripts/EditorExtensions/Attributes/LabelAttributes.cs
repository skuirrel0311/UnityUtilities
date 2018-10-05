using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HyperCasual.Attributes
{
    /// <summary>
    /// Inspector のラベルを裏書きする Attributes.
    /// 構造化されているものには適用できないので注意してください。
    /// </summary>
    public class Label : PropertyAttribute
    {
        public string label;
        public Label (string label)
        {
            this.label = label;
        }
    }

    #if UNITY_EDITOR

    [CustomPropertyDrawer (typeof(Label))]
    public class LabelDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = this.attribute as Label;
            label.text = (string.IsNullOrEmpty (attribute.label)) ? label.text : attribute.label;
            EditorGUI.PropertyField (position, property, label);
        }
    }

    #endif
}