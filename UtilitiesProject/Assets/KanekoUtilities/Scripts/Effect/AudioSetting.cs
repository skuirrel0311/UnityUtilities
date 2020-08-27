using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class AudioVolumeSetting
    {
        RegisterFloatParameter volume;
        RegisterBoolParameter enable;

        public float Volume
        {
            get
            {
                return volume.GetValue() * (Enable ? 0.0f : 1.0f) * (IsMute ? 0.0f : 1.0f);
            }
            set
            {
                volume.SetValue(value);
            }
        }
        public bool Enable
        {
            get
            {
                return enable.GetValue();
            }
            set
            {
                enable.SetValue(value);
            }
        }
        public bool IsMute;

        public AudioVolumeSetting(string id, float defaultValue = 1.0f)
        {
            volume = new RegisterFloatParameter(id + "Volume", defaultValue);
            enable = new RegisterBoolParameter(id + "Enable", false);
        }
    }

    public class MasterVolumeSettings
    {
        public AudioVolumeSetting BGM;
        public AudioVolumeSetting SE;
        
        public MasterVolumeSettings()
        {
            BGM = new AudioVolumeSetting("MasterBGM");
            SE = new AudioVolumeSetting("MasterSE");
        }
    }
}
