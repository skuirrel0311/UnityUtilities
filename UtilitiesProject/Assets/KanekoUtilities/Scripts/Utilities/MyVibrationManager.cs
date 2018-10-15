using UnityEngine;
#if IMPORT_HYPERCOMMON
using HyperCasual.Vibration;
#endif

namespace KanekoUtilities
{
    public enum VibrationType
    {
        /// <summary>
        /// 選択の変化を表現
        /// </summary>
        SelectionChange,
        /// <summary>
        /// 衝突などの重さ（軽い）を表現
        /// 見た目の体験を補完する役割
        /// </summary>
        ImpactLight,
        /// <summary>
        /// 衝突などの重さ（中くらい）を表現
        /// 見た目の体験を補完する役割
        /// </summary>
        ImpactMedium,
        /// <summary>
        /// 衝突などの重さ（重い）を表現
        /// 見た目の体験を補完する役割
        /// </summary>
        ImpactHeavy,
        /// <summary>
        /// タスクやアクションの成功を表現
        /// </summary>
        NotificationSuccess,
        /// <summary>
        /// タスクやアクションの警告を表現
        /// </summary>
        NotificationWarning,
        /// <summary>
        /// タスクやアクションの失敗を表現
        /// </summary>
        NotificationFailure,
        /// <summary>
        /// UnityEngine.Handheld.Vibrate()
        /// </summary>
        UnityVibrate
    }

    public class MyVibrationManager : Singleton<MyVibrationManager>
    {
        public void Vibrate(VibrationType type)
        {
#if !IMPOERT_HYPERCOMMON
            Debug.Log("Vibrate " + type);
#else
            VibrationManager.Instance.Vibrate(ToHyperCommonVivrationType(type));
#endif
        }

        public bool IsSupported
        {
            get
            {
#if !IMPOERT_HYPERCOMMON
                return false;
#else
                return VibrationManager.IsSupported();
#endif
            }
        }


#if IMPORT_HYPERCOMMON
        VibrationManager.VibrationType ToHyperCommonVivrationType(VibrationType type)
        {
            switch (type)
            {
                case VibrationType.SelectionChange:
                    return VibrationManager.VibrationType.SelectionChange;
                case VibrationType.ImpactLight:
                    return VibrationManager.VibrationType.ImpactLight;
                case VibrationType.ImpactMedium:
                    return VibrationManager.VibrationType.ImpactMedium;
                case VibrationType.ImpactHeavy:
                    return VibrationManager.VibrationType.ImpactHeavy;
                case VibrationType.NotificationSuccess:
                    return VibrationManager.VibrationType.NotificationSuccess;
                case VibrationType.NotificationWarning:
                    return VibrationManager.VibrationType.NotificationWarning;
                case VibrationType.NotificationFailure:
                    return VibrationManager.VibrationType.NotificationFailure;
                case VibrationType.UnityVibrate:
                    return VibrationManager.VibrationType.UnityVibrate;
            }
            return 0;
        }
#endif
    }
}