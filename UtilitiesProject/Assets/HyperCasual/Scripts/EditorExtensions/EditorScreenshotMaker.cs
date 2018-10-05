using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual
{
    /// <summary>
    /// UnityEditor でスクリーンショットを撮影し、デスクトップに保存するクラス。
    /// 撮影したいシーンに手動でアタッチして使用する。
    /// Game Window にフォーカスがあり、再生中にのみ作動する。
    /// </summary>
    public class EditorScreenshotMaker : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField] KeyCode key = KeyCode.C;

        private void Update()
        {
            if (Input.GetKeyDown(key)) {
                TakeScreenshot();
            }
        }

        private static void TakeScreenshot()
        {
            var name = "unity_"
                       + System.DateTime.Now.ToString("HHmmssfff")
                       + ".png";
            var path = System.IO.Path.Combine(
                           System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
                           name
                       );

            TakeScreenshot(path);

            Debug.LogFormat("Took a screenshot: {0}", path);
        }

        private static void TakeScreenshot(string path)
        {
            ScreenCapture.CaptureScreenshot(path);
        }
        #endif
    }

    #if UNITY_EDITOR
    public class EditorScreenshotMakerEditor : UnityEditor.Editor
    {
        [UnityEditor.MenuItem ("GameObject/HyperCasual/Utils/Editor Screenshot Maker", false, 10)]
        public static void CreateEditorScreenshotMaker ()
        {
            // Create Button
            UnityEditor.EditorApplication.ExecuteMenuItem ("GameObject/Create Empty");

            // Get GameObject of Button
            GameObject go = UnityEditor.Selection.activeGameObject;

            if (go) {
                go.name = "Editor Screenshot Maker";
                go.AddComponent<EditorScreenshotMaker>();
            }
        }
    }
    #endif
}