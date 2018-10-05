#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HyperCasual
{
    public static class EditorExtensions
    {
        public const int DefaultLineHeight = 16;
        public const int DefaultMargin = 2;

        /// <summary>
        /// EDITOR ONLY.
        /// GameScene の Screen のサイズを取得します。
        /// </summary>
        /// <returns>The game screen size.</returns>
        public static Vector2Int GetGameScreenSize()
        {
            const char Splitter = 'x';
            var res = UnityStats.screenRes.Split(Splitter);
            int width = int.Parse(res[0]);
            int height = int.Parse(res[1]);
            return new Vector2Int(width, height);
        }


        #region Field

        // MonoScript

        public static void AddScriptField(this Editor editor, MonoBehaviour behaviour, bool editable = false)
        {
            AddScriptField(behaviour, editable);
        }
        public static void AddScriptField(this Editor editor, ScriptableObject obj, bool editable = false)
        {
            AddScriptField(obj, editable);
        }
        public static void AddScriptField(this Editor editor, MonoScript script, bool editable = false)
        {
            AddScriptField(script, editable);
        }
        public static void AddScriptField(this EditorWindow window, ScriptableObject obj, bool editable = false)
        {
            AddScriptField(obj, editable);
        }
        public static void AddScriptField(this EditorWindow window, MonoScript script, bool editable = false)
        {
            AddScriptField(script, editable);
        }
        static void AddScriptField(MonoBehaviour behaviour, bool editable)
        {
            var script = MonoScript.FromMonoBehaviour(behaviour);
            AddScriptField(script, editable);
        }
        static void AddScriptField(ScriptableObject obj, bool editable)
        {
            var script = MonoScript.FromScriptableObject(obj);
            AddScriptField(script, editable);
        }
        static void AddScriptField(MonoScript script, bool editable)
        {
            EditorGUI.BeginDisabledGroup(!editable);
            EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();
        }


        // 隠れた Property の強制表示

        public static void AddHiddenObjectField(this Editor editor, string name, bool editable = true)
        {
            var obj = new SerializedObject(editor);
            AddHiddenObjectField(obj, name, editable);
        }
        public static void AddHiddenObjectField(this EditorWindow window, string name, bool editable = true)
        {
            var obj = new SerializedObject(window);
            AddHiddenObjectField(obj, name, editable);
        }
        static void AddHiddenObjectField(SerializedObject obj, string name, bool editable)
        {
            var property = obj.FindProperty(name);
            if (property == null) {
                Debug.LogErrorFormat("Not found a property named \"{0}\".", name);
            } else {
                if (editable) {
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(property, true);
                    if (EditorGUI.EndChangeCheck()) {
                        obj.ApplyModifiedProperties();
                    }
                } else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(property, true);
                    EditorGUI.EndDisabledGroup();
                }
            }
        }

        #endregion



        #region Editor Layout

        public static Vector2 GetCurrentPosition(this EditorWindow window)
        {
            var last = GUILayoutUtility.GetLastRect();
            return new Vector2(last.xMin, last.yMax);
        }


        public static void AddSeparator(this Editor editor, int margin = 0)
        {
            AddSeparator(margin);
        }
        public static void AddSeparator(this EditorWindow window, int margin = 0)
        {
            AddSeparator(margin);
        }
        public static void AddSeparator(int margin = 0)
        {
            AddVerticalSpace(margin);

            const int Margin = 0;
            const int Height = 1;
            var style = new GUIStyle("box");
            style.margin.top = style.margin.bottom = Margin;
            style.padding.top = style.padding.bottom = Margin;
            style.border.top = style.border.bottom = Height;
            GUILayout.Box(GUIContent.none, style, GUILayout.ExpandWidth(true), GUILayout.Height(Height));

            AddVerticalSpace(margin);
        }


        public static void AddVerticalSpace(this Editor editor, int height)
        {
            AddVerticalSpace(height);
        }
        public static void AddVerticalSpace(this EditorWindow window, int height)
        {
            AddVerticalSpace(height);
        }
        public static void AddHorizontalSpace(this Editor editor, int width)
        {
            AddHorizontalSpace(width);
        }
        public static void AddHorizontalSpace(this EditorWindow window, int width)
        {
            AddHorizontalSpace(width);
        }
        static void AddVerticalSpace(int height)
        {
            height = Mathf.Max(height, 0);
            if (height > 0) {
                GUILayout.Box(GUIContent.none, NoneStyle, GUILayout.ExpandWidth(true), GUILayout.Height(height));
            }
        }
        static void AddHorizontalSpace(int width)
        {
            width = Mathf.Max(width, 0);
            if (width > 0) {
                GUILayout.Box(GUIContent.none, NoneStyle, GUILayout.Width(width), GUILayout.ExpandHeight(true));
            }
        }


        static GUIStyle NoneStyle {
            get {
                var style = GUIStyle.none;
                const int Weight = 0;
                style.margin.top = style.margin.bottom = style.margin.right = style.margin.left = Weight;
                style.padding.top = style.padding.bottom = style.padding.right = style.padding.left = Weight;
                style.border.top = style.border.bottom = style.border.right = style.border.left = Weight;
                return style;
            }
        }

        public static GUIStyle GetLabelGUIStyle(TextAnchor anchor)
        {
            return new GUIStyle(GUI.skin.label){ alignment = anchor };
        }

        #endregion



        #region SerializedObject

        public class EditorButton
        {
            public string Label { get; private set; }
            private System.Action action;

            public EditorButton(string label, System.Action action)
            {
                this.Label = label;
                this.action = action;
            }

            public bool IsValid()
            {
                return !string.IsNullOrEmpty(this.Label) && action != null;
            }

            public void InvokeAction()
            {
                if (action != null) {
                    action();
                }
            }
        }

        /// <summary>
        /// 小さなボタンを追加します。
        /// </summary>
        /// <param name="actions">EditorButtonAction.</param>
        public static void AddButtons(this Editor editor, params EditorButton[] actions)
        {
            actions = actions.Where(a => (a != null && a.IsValid())).ToArray();

            var count = actions.Length;
            if (count == 0) {
                return;
            }

            GUILayout.BeginHorizontal();
            {
                const int ButtonMaxWidth = 64;
                const int MarginWidth = 32;
                var width = Mathf.Min((EditorGUIUtility.currentViewWidth - MarginWidth) / count, ButtonMaxWidth);

                GUILayout.FlexibleSpace();

                if (count == 1) {
                    if (GUILayout.Button(actions[0].Label, EditorStyles.miniButton, GUILayout.Width(width))) {
                        actions[0].InvokeAction();
                    }

                } else {
                    for (var i = 0; i < count; i++) {
                        GUIStyle style = EditorStyles.miniButtonMid;
                        if (i == 0) {
                            style = EditorStyles.miniButtonLeft;
                        } else if (i == count - 1) {
                            style = EditorStyles.miniButtonRight;
                        }

                        if (GUILayout.Button(actions[i].Label, style, GUILayout.Width(width))) {
                            actions[i].InvokeAction();
                        }
                    }
                }

            }
            GUILayout.EndHorizontal();

            editor.AddVerticalSpace(EditorExtensions.DefaultMargin);
        }

        /// <summary>
        /// AssetDatabase 上の自身を選択します。
        /// </summary>
        public static void AddButtonToSelectAsset(this Editor editor)
        {
            var asset = editor.target;

            if (!(asset is ScriptableObject)) {
                return;
            }

            const string ButtonLabel = "Select";
            var action = new EditorButton(ButtonLabel, () => {
                EditorUtil.FocusOnProjectWindow();
                EditorUtil.SelectAsset(editor.target);
            });
            editor.AddButtons(action);
        }


        /// <summary>
        /// 指定した PropertyName の OjbectReferenceValue が null の場合、 AssetPath で指定した prefab の参照を代入します。
        /// </summary>
        /// <returns><c>true</c>ならば、代入成功。</returns>
        /// <param name="obj">SerizalizedObject.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="assetPath">Asset path.</param>
        public static bool SetDefaultObjectReferenceValue(this SerializedObject obj, string propertyName, string assetPath)
        {
            var property = obj.FindProperty(propertyName);
            if (property.objectReferenceValue == null) {
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                property.objectReferenceValue = prefab;
                return prefab != null;
            }
            return false;
        }

        /// <summary>
        /// 全ての PropertyPath を取得します。
        /// </summary>
        /// <returns>The property paths.</returns>
        /// <param name="obj">Object.</param>
        public static string GetPropertyPaths(this SerializedObject obj)
        {
            var sp = obj.GetIterator();
            var builder = new System.Text.StringBuilder();
            while (sp.Next(true)) {
                builder.AppendLine(sp.propertyPath);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 配列想定の SerializedProperty から Object の配列を抽出します。
        /// </summary>
        /// <returns>The elements.</returns>
        /// <param name="property">Property.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static List<T> GetElements<T>(SerializedProperty property) where T : Object
        {
            var elements = new List<T>();

            if (!property.isArray) {
                return elements;
            }

            for (var i = 0; i < property.arraySize; i++) {
                var element = property.GetArrayElementAtIndex(i);
                var obj = element.objectReferenceValue as T;
                elements.Add(obj);
            }

            return elements;
        }

        #endregion



        #region Platforms

        public static RuntimePlatform ToRuntimePlatform(this BuildTarget buildTarget)
        {
            switch (buildTarget) {
            case BuildTarget.Android:
                return RuntimePlatform.Android;
            case BuildTarget.iOS:
                return RuntimePlatform.IPhonePlayer;
            default:
                throw new System.NotSupportedException();
            }
        }

        public static BuildTarget ToBuildTarget(this RuntimePlatform runtimePlatform)
        {
            switch (runtimePlatform) {
            case RuntimePlatform.Android:
                return BuildTarget.Android;
            case RuntimePlatform.IPhonePlayer:
                return BuildTarget.iOS;
            default:
                throw new System.NotSupportedException();
            }
        }

        public static BuildTargetGroup ToBuildTargetGroup(this BuildTarget buildTarget)
        {
            switch (buildTarget) {
            case BuildTarget.Android:
                return BuildTargetGroup.Android;
            case BuildTarget.iOS:
                return BuildTargetGroup.iOS;
            default:
                throw new System.NotSupportedException();
            }
        }

        public static BuildTarget ToBuildTarget(this BuildTargetGroup buildTargetGroup)
        {
            switch (buildTargetGroup) {
            case BuildTargetGroup.Android:
                return BuildTarget.Android;
            case BuildTargetGroup.iOS:
                return BuildTarget.iOS;
            default:
                throw new System.NotSupportedException();
            }
        }

        public static RuntimePlatform GetApplicationPlatformInEditor()
        {
            return EditorUserBuildSettings.activeBuildTarget.ToRuntimePlatform();
        }

        #endregion

    }
}
#endif
    