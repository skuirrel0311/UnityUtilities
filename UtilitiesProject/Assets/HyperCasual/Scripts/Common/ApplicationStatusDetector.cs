using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HyperCasual
{
    public class ApplicationStatusDetector : SingletonMonoBehaviour<ApplicationStatusDetector>
    {
        /// <summary>
        /// アプリがバックグラウンドに回った時に発行されるイベント。
        /// </summary>
        public UnityEvent OnSuspend = new UnityEvent();
        /// <summary>
        /// アプリがバックグラウンドから復帰した時に発行されるイベント。
        /// </summary>
        public ResumeUnityEvent OnResume = new ResumeUnityEvent();
        /// <summary>
        /// アプリの終了時に発行されるイベント。
        /// </summary>
        public UnityEvent OnQuit = new UnityEvent();
        /// <summary>
        /// 端末の向きが変更されたことで、画面の向きが変わった時に発行されるイベント。
        /// </summary>
        public ChangeOrientaionUnityEvent OnChangeOrientation = new ChangeOrientaionUnityEvent();
        private DateTime suspendedDateTime;

        [System.NonSerialized] bool isSuspended = false;

        protected override void AwakeValidly()
        {
            DontDestroyOnLoad();
            if (IsOrientationRotatable()) {
                DetectOrientationChange();
            }
        }

        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus) {
                if (!isSuspended) {
                    isSuspended = true;
                    suspendedDateTime = DateTime.Now;
                    if (OnSuspend != null) {
                        OnSuspend.Invoke();
                    }
                }
            } else {
                if (isSuspended) {
                    isSuspended = false;
                    if (OnResume != null) {
                        var span = DateTime.Now - suspendedDateTime;
                        OnResume.Invoke(span);
                    }
                }
            }
        }

        #if UNITY_EDITOR
        void OnDestroy()
        {
            if (Application.isEditor && OnQuit != null) {
                OnQuit.Invoke();
            }
        }
        #endif

        void OnApplicationQuit()
        {
            if (OnQuit != null) {
                OnQuit.Invoke();
            }
        }

        Coroutine DetectOrientationChange()
        {
            return StartCoroutine(DetectOrientationChangeCoroutine());
        }

        IEnumerator DetectOrientationChangeCoroutine()
        {
            const float Delay = 0.1f;
            var wait = new WaitForSeconds(Delay);
            #if UNITY_EDITOR
            var prev = new Vector2(Screen.width, Screen.height);
            while (true) {
                if (Screen.width == prev.x && Screen.height == prev.y) {
                    yield return wait;
                    continue;
                }
                if (Screen.width < Screen.height) {
                    // 縦持ち
                    OnChangeOrientation.Invoke(DeviceOrientation.Portrait);
                    prev = new Vector2(Screen.width, Screen.height);
                    yield return wait;
                    continue;
                } else {
                    // 横持ち
                    OnChangeOrientation.Invoke(DeviceOrientation.LandscapeLeft);
                    prev = new Vector2(Screen.width, Screen.height);
                    yield return wait;
                    continue;
                }
            }
            #else
            DeviceOrientation prev = Input.deviceOrientation;
            while (true) {
                switch (prev) {
                case DeviceOrientation.Unknown:
                case DeviceOrientation.FaceUp:
                case DeviceOrientation.FaceDown:
                    {
                        yield return wait;
                        continue;
                    }
                default:
                    {
                        switch (Input.deviceOrientation) {
                        case DeviceOrientation.Unknown:
                        case DeviceOrientation.FaceUp:
                        case DeviceOrientation.FaceDown:
                            {
                                yield return wait;
                                break;
                            }
                        default:
                            {
                                if (prev != Input.deviceOrientation) {
                                    OnChangeOrientation.Invoke(Input.deviceOrientation);
                                    prev = Input.deviceOrientation;
                                }
                                yield return wait;
                                break;
                            }
                        }
                        continue;
                    }
                }
            }
            #endif
        }

        [Serializable]
        public class ResumeUnityEvent : UnityEvent<TimeSpan>
        {
        }

        [Serializable]
        public class ChangeOrientaionUnityEvent : UnityEvent<DeviceOrientation>
        {
        }


        /// <summary>
        /// アプリの設定で、画面の回転が許可されているかどうかを取得します。
        /// 端末側の設定ではありません。
        /// </summary>
        /// <returns><c>true</c>ならば画面回転可能。</returns>
        public static bool IsOrientationRotatable()
        {
            #if UNITY_EDITOR

            if (PlayerSettings.defaultInterfaceOrientation != UIOrientation.AutoRotation) {
                return false;
            }
            // FIXME: Check "Allowed Orientation for AutoRotation" of PlayerSettings
            return true;

            #else

            int count = 0;
            if (Screen.autorotateToPortrait)
                count++;
            if (Screen.autorotateToPortraitUpsideDown)
                count++;
            if (Screen.autorotateToLandscapeLeft)
                count++;
            if (Screen.autorotateToLandscapeRight)
                count++;

            return (count > 1);

            #endif
        }

        /// <summary>
        /// 通信接続があるかチェックします。
        /// </summary>
        /// <returns><c>true</c>の場合は通信接続があり。</returns>
        public static bool HasNetworkConnection()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }
}