using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual
{
    public static class ScreenUtility
    {
        public const int IPhoneXWidth = 1125;
        public const int IPhoneXHeight = 2436;

        /// <summary>
        /// iPhone X の解像度なのかを取得します。
        /// </summary>
        /// <returns><c>true</c>ならば、iPhone X の解像度。</returns>
        public static bool IsIPhoneXResolution()
        {
            return (
                (Screen.width == IPhoneXWidth && Screen.height == IPhoneXHeight)
                || (Screen.width == IPhoneXHeight && Screen.height == IPhoneXWidth)
            );
        }

        /// <summary>
        /// Screen の Rect を取得します。
        /// </summary>
        /// <returns>The screen area.</returns>
        public static Rect GetScreenArea()
        {
            return new Rect(0, 0, Screen.width, Screen.height);
        }

        /// <summary>
        /// SafeArea を取得します。
        /// Simulates を ON にすると、Editor 再生中のみ再現を行います（iPhoneX のみ）。
        /// </summary>
        /// <returns>The safe area.</returns>
        /// <param name="simulates">If set to <c>true</c> simulates.</param>
        public static Rect GetSafeArea(bool simulates = false)
        {
            if (!Application.isEditor || !simulates) {
                return Screen.safeArea;
            }

            if (IsIPhoneXResolution()) {
                var area = new Rect();
                if (Screen.width < Screen.height) {
                    //縦持ち
                    area.position = new Vector2(0f * Screen.width / IPhoneXWidth, 102f * Screen.height / IPhoneXHeight);
                    area.size = new Vector2(IPhoneXWidth * Screen.width / IPhoneXWidth, 2202f * Screen.height / IPhoneXHeight);
                } else {
                    //横持ち
                    area.position = new Vector2(132f * Screen.width / IPhoneXHeight, 63f * Screen.height / IPhoneXWidth);
                    area.size = new Vector2(2172f * Screen.width / IPhoneXHeight, 1062f * Screen.height / IPhoneXWidth);
                }
                return area;
            }

            return Screen.safeArea;
        }

        /// <summary>
        /// SafestArea （Screen の中心を中心に、SafeArea が最も広くなる矩形）を取得します。
        /// Simulates を ON にすると、Editor 再生中のみ再現を行います（iPhoneX のみ）。
        /// </summary>
        /// <returns>The safest area.</returns>
        /// <param name="simuraltes">If set to <c>true</c> simuraltes.</param>
        public static Rect GetSafestArea(bool simuraltes)
        {
            var safe = GetSafeArea(simuraltes);
            var screen = GetScreenArea();
            var width = Mathf.Min(Mathf.Abs(screen.center.x - safe.xMin), Mathf.Abs(screen.center.x - safe.xMax)) * 2;
            var height = Mathf.Min(Mathf.Abs(screen.center.y - safe.yMin), Mathf.Abs(screen.center.y - safe.yMax)) * 2;
            return new Rect(
                screen.center.x - width / 2,
                screen.center.y - height / 2,
                width,
                height
            );
        }
    }
}