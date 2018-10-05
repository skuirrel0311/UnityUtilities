using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperCasual;

namespace HyperCasual.UI
{
    [ExecuteInEditMode]
    public class SafeAreaViewer : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField]
        Color color = new Color(1, 0, 1, 0.5f);

        [Header("Unsafe Area")]
        [SerializeField]
        bool viewsUnsafeArea = true;

        [Header("Frame")]
        [SerializeField]
        bool viewsFrame = true;
        [SerializeField]
        DeviceOrientation orientation = DeviceOrientation.Portrait;

        void OnGUI()
        {
            ValidateOrientation();

            if (!ScreenUtility.IsIPhoneXResolution()) {
                return;
            }

            var tex = new Texture2D(1, 1);
            var safe = GetEmulatedSafeAreaOnIPhoneX();

            if (viewsUnsafeArea) {
                var unsafes = new Rect[] { 
                    new Rect(0, 0, Screen.width, safe.y),
                    new Rect(0, safe.y, safe.x, safe.height),
                    new Rect(safe.xMax, safe.y, Screen.width - safe.xMax, safe.height),
                    new Rect(0, safe.yMax, Screen.width, Screen.height - safe.yMax)
                };
                foreach (var area in unsafes) {
                    DrawRect(tex, area, color);
                }

            }

            if (viewsFrame) {
                var min = Mathf.Min(Screen.width, Screen.height);
                float homebarWidth = min;
                float homebarHeight = 16;

                switch (orientation) {
                case DeviceOrientation.Portrait:
                    {
                        var notchWidth = Screen.width * 0.55f;
                        var notchHeight = safe.yMin * 2 / 3;
                        var notch = new Rect((Screen.width - notchWidth) / 2, 0, notchWidth, notchHeight);
                        DrawRect(tex, notch, color);

                        homebarWidth = min * 0.45f;
                        break;
                    }
                case DeviceOrientation.LandscapeRight:
                    {
                        var notchWidth = Screen.height * 0.55f;
                        var notchHeight = safe.xMin * 2 / 3;
                        var notch = new Rect(0, (Screen.height - notchWidth) / 2, notchHeight, notchWidth);
                        DrawRect(tex, notch, color);

                        homebarWidth = min * 0.6f;
                        break;
                    }
                case DeviceOrientation.LandscapeLeft:
                    {
                        var notchWidth = Screen.height * 0.55f;
                        var notchHeight = (Screen.width - safe.xMax) * 2 / 3;
                        var notch = new Rect(Screen.width - notchHeight, (Screen.height - notchWidth) / 2, notchHeight, notchWidth);
                        DrawRect(tex, notch, color);

                        homebarWidth = min * 0.6f;
                        break;
                    }
                }
                {
                    DrawRect(tex, new Rect((Screen.width - homebarWidth) / 2, Screen.height - homebarHeight * 2, homebarWidth, homebarHeight), color);

                    var corner = min / 12f;
                    var diff = new Vector2(-1, -1) / 2 * corner;
                    DrawLine(
                        tex,
                        Vector2.right * corner + diff,
                        Vector2.up * corner + diff,
                        color,
                        corner
                    );
                    diff = new Vector2(1, -1) / 2 * corner;
                    DrawLine(
                        tex,
                        Vector2.right * Screen.width + Vector2.left * corner + diff,
                        Vector2.right * Screen.width + Vector2.up * corner + diff, 
                        color,
                        corner
                    );
                    diff = new Vector2(-1, 1) / 2 * corner;
                    DrawLine(
                        tex,
                        Vector2.up * Screen.height + Vector2.right * corner + diff,
                        Vector2.up * Screen.height + Vector2.down * corner + diff,
                        color,
                        corner
                    );
                    diff = new Vector2(1, 1) / 2 * corner;
                    DrawLine(
                        tex,
                        new Vector2(Screen.width, Screen.height) + Vector2.left * corner + diff,
                        new Vector2(Screen.width, Screen.height) + Vector2.down * corner + diff,
                        color,
                        corner
                    );
                }
            }
        }


        void ValidateOrientation()
        {
            if (Screen.width < Screen.height) {
                // 縦持ち
                orientation = DeviceOrientation.Portrait;
            } else {
                // 横持ち
                switch (orientation) {
                case DeviceOrientation.LandscapeLeft:
                case DeviceOrientation.LandscapeRight:
                    break;
                default:
                    orientation = DeviceOrientation.LandscapeLeft;
                    break;
                }
            }
        }

        Rect GetEmulatedSafeAreaOnIPhoneX()
        {
            var safe = ScreenUtility.GetSafeArea(true);
            return new Rect(safe.x, Screen.height - safe.yMax, safe.width, safe.height);
        }


        void DrawRect(Texture tex, Rect rect, Color color)
        {
            using (new Cache()) {
                GUI.color = color;
                GUI.DrawTexture(rect, tex);
            }
        }

        void DrawLine(Texture tex, Vector2 p1, Vector2 p2, Color color, float weight = 1)
        {
            var angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180f / Mathf.PI;
            var length = (p1 - p2).magnitude;

            using (new Cache()) {
                GUIUtility.RotateAroundPivot(angle, p1);
                GUI.color = color;
                GUI.DrawTexture(new Rect(p1.x, p1.y - weight / 2, length, weight), tex);
            }
        }

        class Cache : System.IDisposable
        {
            public Color color;
            public Matrix4x4 matrix;
            public Cache()
            {
                color = GUI.color;
                matrix = GUI.matrix;
            }

            public void Dispose()
            {
                GUI.color = color;
                GUI.matrix = matrix;
            }
        }
        #endif
    }

    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(SafeAreaViewer))]
    class SafeAreaViewerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            UnityEditor.EditorGUILayout.HelpBox(
                "SafeArea やフレームを Game window に表示します (iPhoneX のみ)。実機では表示しません。\n" +
                "表示するには、この Component を enable にしてください。",
                UnityEditor.MessageType.Info
            );

            base.OnInspectorGUI();
        }
    }
    #endif
}