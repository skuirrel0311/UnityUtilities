using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonsterContainer))]
public class MonsterEditor : MyCustomEditor
{
    SerializedProperty typeProperty;

    MonsterType type;
    MonsterType Type
    {
        get
        {
            return type;
        }
        set
        {
            if (type == value) return;

            type = value;
            viewClass = GetClassSerializeProperty();
            typeProperty = serializedObject.FindProperty("monsterType");
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        typeProperty = serializedObject.FindProperty("monsterType");
        Type = (target as MonsterContainer).monsterType;
    }

    protected override SerializedProperty GetClassSerializeProperty()
    {
        switch (type)
        {
            case MonsterType.Warrior:
                return serializedObject.FindProperty("warrior");
            case MonsterType.Witch:
                return serializedObject.FindProperty("witch");
            case MonsterType.Dragon:
                return serializedObject.FindProperty("dragon");
        }

        return null;
    }

    protected override void ShowClassProperty()
    {
        Type = (target as MonsterContainer).monsterType;

        EditorGUILayout.PropertyField(typeProperty);

        base.ShowClassProperty();
    }
}
