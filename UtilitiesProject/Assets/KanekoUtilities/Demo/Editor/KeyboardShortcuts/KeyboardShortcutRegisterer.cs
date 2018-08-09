using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Reflection;

namespace HyperCasual
{
    [InitializeOnLoad]
    public class KeyboardShortcutRegisterer
    {
        static KeyboardShortcutRegisterer()
        {
            bool keyDown = false;
            EditorApplication.CallbackFunction function = () => {

                if (!keyDown && Event.current.type == EventType.KeyDown) {
                    keyDown = true;

                    // '_' が入力されたら、Hierarchy で選択しているオブジェクトのアクティブ状態を反転させる
                    if (Event.current.keyCode == KeyCode.LeftBracket
                        && ToggleGameObjectActiveKeyboardShortcut.IsAvailable()) {
                        ToggleGameObjectActiveKeyboardShortcut.Execute();
                    }

                    // ']' が入力されたら、PlayerSettings を開く
                    if (Event.current.keyCode == KeyCode.RightBracket
                        && OpenPlayerSettingsKeyboardShortcut.IsAvailable()) {
                        OpenPlayerSettingsKeyboardShortcut.Execute();
                    }
                }

                if (keyDown && Event.current.type == EventType.KeyUp) {
                    keyDown = false;
                }
            };

            FieldInfo info = typeof(EditorApplication).GetField("globalEventHandler", BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
            EditorApplication.CallbackFunction functions = (EditorApplication.CallbackFunction)info.GetValue(null);
            functions += function;
            info.SetValue(null, (object)functions);
        }
    }

}