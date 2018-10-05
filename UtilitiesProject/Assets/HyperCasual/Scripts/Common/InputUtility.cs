using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HyperCasual
{

    public static class InputUtility
    {
        /// <summary>
        /// 長押しの共通秒数。
        /// </summary>
        public const float LongPressInterval = 1.0f;

        /// <summary>
        /// uGUI 以外をタッチしているかを取得します。
        /// </summary>
        /// <returns><c>true</c>ならば、uGUI 以外をタッチ。</returns>
        public static bool GetTouchDown ()
        {
            if (!Input.GetMouseButtonDown (0)) {
                return false;
            }

            // on mobile device
            if (Input.touchCount > 0) {
                var fingerId = Input.GetTouch (0).fingerId;
                return !EventSystem.current.IsPointerOverGameObject (fingerId);
            }

            // on editor
            return !EventSystem.current.IsPointerOverGameObject ();
        }

        /// <summary>
        /// Android の戻るボタンを押し始めたかを取得します。
        /// Android 以外は常に false を取得します。
        /// </summary>
        /// <returns><c>true</c>ならば戻るボタン押し始め。</returns>
        public static bool GetBackButtonDown ()
        {
            #if UNITY_ANDROID
            return Input.GetKeyDown (KeyCode.Escape);
            #else
            return false;
            #endif
        }
        /// <summary>
        /// Android の戻るボタンを押し終わったかを取得します。
        /// Android 以外は常に false を取得します。
        /// </summary>
        /// <returns><c>true</c>ならば戻るボタン押し終わり。</returns>
        public static bool GetBackButtonUp ()
        {
            #if UNITY_ANDROID
            return Input.GetKeyUp (KeyCode.Escape);
            #else
            return false;
            #endif
        }

        /// <summary>
        /// EventSystem が存在しない場合、作成します。
        /// </summary>
        /// <param name="pixelDragThreshold">Pixel drag threshold. 負の値の場合、設定を更新しません。</param>
        public static void CreateEventSystem (int pixelDragThreshold = -1)
        {
            if (EventSystem.current == null) {
                var obj = new GameObject ("EventSystem");
                EventSystem.current = obj.AddComponent<EventSystem> ();
                obj.AddComponent<StandaloneInputModule> ();
            }
            if (pixelDragThreshold >= 0) {
                EventSystem.current.pixelDragThreshold = pixelDragThreshold;
            }
        }
    }
}