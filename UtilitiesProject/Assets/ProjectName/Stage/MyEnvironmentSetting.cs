using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MyEnvironmentSetting : EnvironmentSetting 
{
    //ここに追加したい色の項目を増やす

    public override void Apply()
    {
        base.Apply();

        StageColorCoordinator.Instance.ApplyEnviromentSetting(this);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MyEnvironmentSetting))]
public class MyEnvironmentSettingDrawer : EnvironmentSettingDrawer
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif
