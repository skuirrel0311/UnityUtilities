using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FoldoutTest))]
public class FoldoutTestEditor : Editor
{
    SerializedProperty script;
    SerializedProperty sampleClass;
    SerializedProperty hoge, fuga;

    void OnEnable()
    {
        script = serializedObject.FindProperty("m_Script");
        sampleClass = serializedObject.FindProperty("sampleClass");
        hoge = sampleClass.FindPropertyRelative("hoge");
        fuga = sampleClass.FindPropertyRelative("fuga");
    }

    public override void OnInspectorGUI()
    {
        // スクリプト名を表示する. 編集はできないようにする.
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(script);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.PropertyField(sampleClass);

        if (sampleClass.isExpanded)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(hoge);
            EditorGUILayout.PropertyField(fuga);
            EditorGUI.indentLevel--;
        }
    }
}