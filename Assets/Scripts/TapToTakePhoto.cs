using UnityEngine;
using System.Collections;

public class TapToTakePhoto : MonoBehaviour {
    public TakePhotoHololens takePhoto;
    // Called by GazeGestureManager when the user performs a Select gesture
    public UnityEngine.UI.Text testScript;
    void OnSelect()
    {
        testScript.text = "Tap Recognized Taking Photo";
        takePhoto.CapturePhoto();
    }
}
