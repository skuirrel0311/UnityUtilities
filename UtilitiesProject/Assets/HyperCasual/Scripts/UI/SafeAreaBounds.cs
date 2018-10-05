using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HyperCasual.Attributes;

namespace HyperCasual.UI
{

    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaBounds : MonoBehaviour
    {
        private RectTransform _container;
        private RectTransform container {
            get {
                if (!_container) {
                    _container = this.transform as RectTransform;
                }
                return _container;
            }
        }
        Vector2? anchorMin = null;
        Vector2? anchorMax = null;


        void Start()
        {
            ApplySafeArea();

            if (ApplicationStatusDetector.IsOrientationRotatable()) {
                ApplicationStatusDetector.Instance.OnChangeOrientation.AddListener(OnChangeOrientation);
            }
        }

        void OnDestroy()
        {
            if (ApplicationStatusDetector.IsOrientationRotatable() && ApplicationStatusDetector.IsInstantiated()) {
                ApplicationStatusDetector.Instance.OnChangeOrientation.RemoveListener(OnChangeOrientation);
            }
        }

        void OnChangeOrientation(DeviceOrientation orientation)
        {
            ApplySafeArea();
        }


        void ApplySafeArea()
        {
            if (anchorMin.HasValue && anchorMax.HasValue) {
                container.anchorMin = anchorMin.Value;
                container.anchorMax = anchorMax.Value;
            } else {
                anchorMin = container.anchorMin;
                anchorMax = container.anchorMax;
            }

            var area = Screen.safeArea;

            #if UNITY_EDITOR
            area = SimulateSafeArea();
            #endif

            var min = area.position;
            var max = area.position + area.size;
            min.x /= Screen.width;
            min.y /= Screen.height;
            max.x /= Screen.width;
            max.y /= Screen.height;
            container.anchorMin = new Vector2(Mathf.Clamp(container.anchorMin.x, min.x, max.x), Mathf.Clamp(container.anchorMin.y, min.y, max.y));
            container.anchorMax = new Vector2(Mathf.Clamp(container.anchorMax.x, min.x, max.x), Mathf.Clamp(container.anchorMax.y, min.y, max.y));
        }

        #if UNITY_EDITOR
        public bool simulatesSafeArea = true;
        [Disable]
        public string SimulatingDevice = DeviceUnknown;
        const string DeviceUnknown = "Unknown";
        const string DeviceIPhoneX = "iPhone X";
        Rect SimulateSafeArea()
        {
            SimulatingDevice = DeviceUnknown;
            if (!simulatesSafeArea) {
                return Screen.safeArea;
            }

            if (ScreenUtility.IsIPhoneXResolution()) {
                SimulatingDevice = DeviceIPhoneX;
                return ScreenUtility.GetSafeArea(true);
            }

            return Screen.safeArea;
        }
        #endif
    }

    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(SafeAreaBounds))]
    public class SafeAreaBoundsInspector : UnityEditor.Editor
    {
        [UnityEditor.MenuItem("GameObject/HyperCasual/UI/SafeAreaBounds", false, 10)]
        public static void CreateSafeAreaBounds()
        {
            UnityEditor.EditorApplication.ExecuteMenuItem("GameObject/UI/Panel");

            // Get GameObject of Panel
            GameObject go = UnityEditor.Selection.activeGameObject;

            // Add SafeAreaBounds and remove others
            if (go) {
                go.name = "SafeAreaBounds";
                go.AddComponent<SafeAreaBounds>();
                Object.DestroyImmediate(go.GetComponent<Image>());
                Object.DestroyImmediate(go.GetComponent<CanvasRenderer>());
                var viewer = go.AddComponent<SafeAreaViewer>();
                viewer.enabled = false;
            }
        }

        public override void OnInspectorGUI()
        {
            UnityEditor.EditorGUILayout.HelpBox(
                "アタッチされた RectTransform の Anchor を SafeArea 内に設定します。\n" +
                "SimulatesSafeArea を ON にすると Editor 再生中のみ再現します (iPhoneX のみ)。",
                UnityEditor.MessageType.Info
            );

            base.OnInspectorGUI();
        }
    }
    #endif
}