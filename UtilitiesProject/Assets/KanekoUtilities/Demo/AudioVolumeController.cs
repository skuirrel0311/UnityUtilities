using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanekoUtilities;

public class AudioVolumeController : MonoBehaviour
{
    enum VolumeType { Master, BGM, SE }

    [SerializeField]
    VolumeType type = 0;

    [SerializeField]
    Slider slider = null;

    [SerializeField]
    UGUITextUnity label = null;

    void Start()
    {
        label.Text = type.ToString();

        switch (type)
        {
            case VolumeType.Master:
                slider.value = AudioManager.Instance.MasterVolume;
                break;
            case VolumeType.BGM:
                slider.value = AudioManager.Instance.MasterBGMVolume;
                break;
            case VolumeType.SE:
                slider.value = AudioManager.Instance.MasterSEVolume;
                break;
        }

        slider.onValueChanged.AddListener((currentValue) =>
        {
            switch (type)
            {
                case VolumeType.Master:
                    AudioManager.Instance.SetMasterVolume(currentValue);
                    break;
                case VolumeType.BGM:
                    AudioManager.Instance.SetMasterBGMVolume(currentValue);
                    break;
                case VolumeType.SE:
                    AudioManager.Instance.SetMasterSEVolume(currentValue);
                    break;
            }
        });
    }
}
