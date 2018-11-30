using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class StageColorCoordinator : SingletonMonobehaviour<StageColorCoordinator>
{
    [SerializeField]
    MyEnvironmentSetting[] settings = null;

    [SerializeField]
    int index = 0;

    void OnValidate()
    {
        if (settings == null || index < 0 || index >= settings.Length) return;
        settings[index].Apply();
    }
    
    public void Init()
    {

    }

    //MyEnvironmentSettingから呼ばれる
    public void ApplyEnviromentSetting(MyEnvironmentSetting setting)
    {
        //ここでMaterialなどに色を適用させる
    }
}
