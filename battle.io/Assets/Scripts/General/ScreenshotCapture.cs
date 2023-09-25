using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
    private const string _screenshotName = "Screenshot_0_";
    private int _screenshotAmount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ScreenCapture.CaptureScreenshot(_screenshotName + _screenshotAmount + ".png");
            _screenshotAmount++;
        }
    }
}
