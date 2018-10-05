// reference: https://github.com/anchan828/property-drawer-collection

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HyperCasual.Attributes
{
    /// <summary>
    /// Inspector での編集を禁止する Attributes.
    /// 当然だけれど、対象は Serialize されるので注意してください。
    /// </summary>
    public class Disable : PropertyAttribute
    {
    }

    #if UNITY_EDITOR

    [CustomPropertyDrawer (typeof(Disable))]
    public class DisableDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup (true);
            EditorGUI.PropertyField (position, property, label);
            EditorGUI.EndDisabledGroup ();
        }
    }

    #endif
}