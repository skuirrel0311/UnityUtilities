using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class GameConfig : Singleton<GameConfig>
    {
        /*ここにパラメータを追加していく*/
        //bool useVibration;
        //public bool UseVibration
        //{
        //    get
        //    {
        //        return useVibration;
        //    }
        //    set
        //    {
        //        if (useVibration == value) return;
        //        useVibration = value;
        //        if (useVibration)
        //        {
        //            VibrationManager.Instance.Vibrate(VibrationManager.VibrationType.ImpactMedium);
        //        }
        //        VibrationManager.Instance.SetEnable(useVibration);
        //    }
        //}

        public GameConfig()
        {
        }
    }
}