using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public enum ConfigValueName { UseVibration = 0, AudioEnable = 1 }

    public class GameConfig : Singleton<GameConfig>
    {
        public GameConfig()
        {

        }

        public void OnChangeValue(ConfigValueName name, bool value)
        {
            switch (name)
            {
                case ConfigValueName.UseVibration:
                    SetVibration(value);
                    break;
                case ConfigValueName.AudioEnable:
                    SetAudioEnable(value);
                    break;
            }
        }

        public bool GetValue(ConfigValueName name)
        {
            switch (name)
            {
                case ConfigValueName.UseVibration:

                    return false;
                case ConfigValueName.AudioEnable:
                    return false;
            }

            return false;
        }

        void SetVibration(bool value)
        {
            //useVibration = value;
            //if (useVibration)
            //{
            //    VibrationManager.Instance.Vibrate(VibrationManager.VibrationType.ImpactMedium);
            //}
            //VibrationManager.Instance.SetEnable(useVibration);
        }

        void SetAudioEnable(bool value)
        {
            AudioManager.Instance.AudioEnable.SetValue(value);
        }
    }
}