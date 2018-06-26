using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class MyCustomEditor : Editor
{
    SerializedProperty script;
    protected SerializedProperty viewClass;

    protected virtual void OnEnable()
    {
        script = serializedObject.FindProperty("m_Script");
        viewClass = GetClassSerializeProperty();
    }

    protected abstract SerializedProperty GetClassSerializeProperty();

    public override void OnInspectorGUI()
    {
        ShowScriptReference();

        ShowClassProperty();

        serializedObject.ApplyModifiedProperties();
    }

    protected void ShowScriptReference()
    {
        // スクリプト名を表示する. 編集はできないようにする.
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(script);
        EditorGUI.EndDisabledGroup();
    }

    protected virtual void ShowClassProperty()
    {
        EditorGUILayout.PropertyField(viewClass, true);
    }
}
