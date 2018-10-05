using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Kyusyukeigo.Helper;

namespace HyperCasual
{
    public static class GameWindowResolutionsEditor
    {
        /// <summary>
        /// 本番用の解像度の設定を Game Window に追加します。
        /// </summary>
        [MenuItem("HyperCasual/Add Resolutions to Game Window")]
        public static void AddProductionResolutions()
        {
            // Android 用


            // iOS 用
            // https://help.apple.com/itunes-connect/developer/#/devd274dd925
            AddResolution(GameViewSizeGroupType.iOS, "5.5 inch Tall", 1242, 2208);
            AddResolution(GameViewSizeGroupType.iOS, "5.5 inch Wide", 2208, 1242);
            AddResolution(GameViewSizeGroupType.iOS, "12.9 inch Tall", 2048, 2732);
            AddResolution(GameViewSizeGroupType.iOS, "12.9 inch Wide", 2732, 2048);
            AddResolution(GameViewSizeGroupType.iOS, "5.8 inch Tall [Option]", 1125, 2436);
            AddResolution(GameViewSizeGroupType.iOS, "5.8 inch Wide [Option]", 2436, 1125);
        }
        
        private static void AddResolution(GameViewSizeGroupType target, string baseText, int width, int height)
        {
            GameViewSizeHelper.AddCustomSize(
                target,
                new GameViewSizeHelper.GameViewSize {
                    baseText = baseText,
                    type = GameViewSizeHelper.GameViewSizeType.FixedResolution,
                    width = width,
                    height = height
                }
            );
        }
    }
}
