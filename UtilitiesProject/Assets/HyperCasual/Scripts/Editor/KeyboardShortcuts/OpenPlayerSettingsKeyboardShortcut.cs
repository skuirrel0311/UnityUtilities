using UnityEditor;

namespace HyperCasual
{
    public static class OpenPlayerSettingsKeyboardShortcut
    {
        public static bool IsAvailable()
        {
            return true;
        }

        [MenuItem("HyperCasual/Open PlayerSettings")]
        public static void Execute()
        {
            EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player");
        }
    }
}