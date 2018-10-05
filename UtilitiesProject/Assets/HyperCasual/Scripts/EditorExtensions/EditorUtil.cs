using System.Collections;
using System.Collections.Generic;
using HyperCasual;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HyperCasual
{
    public static class EditorUtil
    {
        public const string InspectorWindowName = "Inspector";
        public const string ProcjetWindowName = "Project";

        /// <summary>
        /// Inspector window にフォーカスを合わせます。
        /// </summary>
        public static bool FocusOnInspectorWindow()
        {
            return FoucsOnWindow(InspectorWindowName);
        }
        /// <summary>
        /// Project window にフォーカスを合わせます。
        /// </summary>
        public static bool FocusOnProjectWindow()
        {
            return FoucsOnWindow(ProcjetWindowName);
        }
        /// <summary>
        /// 指定した名前のウィンドウにフォーカスを合わせます。
        /// </summary>
        /// <returns><c>true</c>ならば、フォーカスが合っている。</returns>
        /// <param name="name">Window の名前。</param>
        public static bool FoucsOnWindow(string name)
        {
            #if UNITY_EDITOR
            EditorWindow[] editorWindows = Resources.FindObjectsOfTypeAll(typeof(EditorWindow)) as EditorWindow[];
            foreach (var window in editorWindows) {
                if (window.titleContent.text == name) {
                    window.Focus();
                    return true;
                }
            }
            #endif

            return false;
        }

        /// <summary>
        /// アセットを選択状態にします。 Inspector window にもフォーカスを合わせます。
        /// </summary>
        /// <param name="asset">Asset.</param>
        public static bool SelectAsset(Object asset)
        {
            if (asset == null) {
                return false;
            }

            EditorUtil.FocusOnInspectorWindow();

            #if UNITY_EDITOR
            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
            #endif

            return true;
        }

        /// <summary>
        /// ScriptableObject を作成し、 Inspector Window で表示します。
        /// </summary>
        /// <param name="path">Path.</param>
        public static T CreateScriptableObject<T>(string path) where T : ScriptableObject
        {
            var asset = ScriptableObjectCreator.CreateAsset<T>(path);

            SelectAsset(asset);

            return asset;
        }

    }
}