using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FoldoutTest))]
public class FoldoutTestEditor : Editor
{
    SerializedProperty script;
    SerializedProperty sampleClass;

    void OnEnable()
    {
        script = serializedObject.FindProperty("m_Script");
        sampleClass = serializedObject.FindProperty("sampleClass");
    }

    public override void OnInspectorGUI()
    {
        // スクリプト名を表示する. 編集はできないようにする.
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(script);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.PropertyField(sampleClass, true);

        serializedObject.ApplyModifiedProperties();
    }
}