using UnityEngine;

namespace KanekoUtilities
{
    public class ScreenshotManager : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                var path = System.IO.Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
                    "unity_" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second + ".png");
                ScreenCapture.CaptureScreenshot(path);
            }
        }
#endif
    }
}