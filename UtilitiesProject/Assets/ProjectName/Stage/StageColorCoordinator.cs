using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageColorCoordinator : MonoBehaviour
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
}
