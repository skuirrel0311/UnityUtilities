using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutoLightingApplyer))]
public class AutoLightingApplyerInspector : Editor{
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var obj = target as AutoLightingApplyer;

        using (new EditorGUILayout.HorizontalScope())
        {
            obj.enableDirLight = EditorGUILayout.BeginToggleGroup("平行光源色", obj.enableDirLight);
            obj.DirLightColor = EditorGUILayout.ColorField(obj.DirLightColor);
            EditorGUILayout.EndToggleGroup();
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            obj.enableClearColor = EditorGUILayout.BeginToggleGroup("環境光", obj.enableClearColor);
            obj.selection = GUILayout.SelectionGrid(obj.selection, new[]{"SkyBox","単色"}, 2);
            if(obj.selection==0)
            {
                obj.SkyBoxMaterial = EditorGUILayout.ObjectField(obj.SkyBoxMaterial, typeof(Material), false) as Material;
            }
            else
            {
                obj.SolidClearColor = EditorGUILayout.ColorField(obj.SolidClearColor);
            }
            EditorGUILayout.EndToggleGroup();
        }

        {
            obj.enableFog = EditorGUILayout.BeginToggleGroup("Fog", obj.enableFog);
            obj.FogColor = EditorGUILayout.ColorField(obj.FogColor);
            obj.FogMode = (FogMode)EditorGUILayout.EnumPopup(obj.FogMode);
            if(obj.FogMode == FogMode.Linear)
            {
                obj.FogStart = EditorGUILayout.FloatField("Start", obj.FogStart);
                obj.FogEnd = EditorGUILayout.FloatField("End", obj.FogEnd);
            }
            else
            {
                obj.FogDensity = EditorGUILayout.FloatField("Denstiy", obj.FogDensity);
            }
            EditorGUILayout.EndToggleGroup();
        }


        //GUILayout.Space(20);
        //obj.AutoDestroy = EditorGUILayout.ToggleLeft("AutoDestroy", obj.AutoDestroy);

        GUILayout.Space(10);
        if(GUILayout.Button("現在のシーンに適用"))
        {
            obj.Apply();
        }
        if (GUILayout.Button("今の環境設定値をここに入れる"))
        {
            obj.Store();
        }


        GUILayout.Space(20);


        serializedObject.ApplyModifiedProperties();
    }
}
