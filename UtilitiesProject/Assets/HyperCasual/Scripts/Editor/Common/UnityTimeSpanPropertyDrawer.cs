using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HyperCasual
{
    [CustomPropertyDrawer (typeof(UnityTimeSpan))]
    public class UnityTimeSpanPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField (position, label);

            const float Diff = 14;
            var labelWidth = EditorGUIUtility.labelWidth + Diff;
            var fieldWidth = position.width - labelWidth + Diff;
            var fieldHeight = EditorGUIUtility.singleLineHeight;



            const string PropertyNameValue = "value";
            var valueProperty = property.FindPropertyRelative (PropertyNameValue);
            var valuePosition = new Rect (
                                    labelWidth,
                                    position.y,
                                    fieldWidth * 2 / 3,
                                    fieldHeight
                                );
            var value = EditorGUI.FloatField (
                            valuePosition, 
                            valueProperty.floatValue
                        );
            valueProperty.floatValue = value;



            const string PropertyNameUnit = "unit";
            var unitProperty = property.FindPropertyRelative (PropertyNameUnit);
            var unitPosition = new Rect (
                labelWidth + fieldWidth * 2 / 3,
                position.y,
                fieldWidth / 3,
                fieldHeight
            );
            var unit = (UnityTimeSpan.TimeUnit)EditorGUI.EnumPopup (
                unitPosition,
                (Enum)Enum.ToObject (typeof(UnityTimeSpan.TimeUnit), unitProperty.enumValueIndex)
            );
            unitProperty.enumValueIndex = Convert.ToInt32 (unit);
        }
    }
}