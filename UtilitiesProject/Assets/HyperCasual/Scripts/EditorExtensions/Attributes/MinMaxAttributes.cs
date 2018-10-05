using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HyperCasual.Attributes
{
    /// <summary>
    /// 最小値を与える Attributes. 引数なしの場合は最小値 0.
    /// </summary>
    public class Min : PropertyAttribute
    {
        public float min;
        public Min ()
        {
            this.min = 0;
        }
        public Min (float min)
        {
            this.min = min;
        }
    }

    /// <summary>
    /// 最大値を与える Attributes. 引数なしの場合は最大値 0.
    /// </summary>
    public class Max : PropertyAttribute
    {
        public float max;
        public Max ()
        {
            this.max = 0;
        }
        public Max (float max)
        {
            this.max = max;
        }
    }


    #if UNITY_EDITOR
    [CustomPropertyDrawer (typeof(Min))]
    public class MinDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = this.attribute as Min;
            switch (property.propertyType) {
            case SerializedPropertyType.Float:
                {
                    var value = EditorGUI.FloatField (position, label, property.floatValue);
                    value = Mathf.Max (attribute.min, value);
                    property.floatValue = value;
                    break;
                }
            case SerializedPropertyType.Integer:
                {
                    var value = EditorGUI.IntField (position, label, property.intValue);
                    var min = Mathf.FloorToInt (attribute.min);
                    value = Mathf.Max (min, value);
                    property.intValue = value;
                    break;
                }
            }
        }
    }

    [CustomPropertyDrawer (typeof(Max))]
    public class MaxDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = this.attribute as Max;
            switch (property.propertyType) {
            case SerializedPropertyType.Float:
                {
                    var value = EditorGUI.FloatField (position, label, property.floatValue);
                    value = Mathf.Min (attribute.max, value);
                    property.floatValue = value;
                    break;
                }
            case SerializedPropertyType.Integer:
                {
                    var value = EditorGUI.IntField (position, label, property.intValue);
                    var min = Mathf.FloorToInt (attribute.max);
                    value = Mathf.Min (min, value);
                    property.intValue = value;
                    break;
                }
            }
        }
    }
    #endif
}