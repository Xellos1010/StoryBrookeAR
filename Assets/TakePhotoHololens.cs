using UnityEngine;
using UnityEngine.VR.WSA.WebCam;
using System.Collections;
public class TakePhotoHololens : MonoBehaviour {
    PhotoCapture photoCaptureObject = null;
    Texture2D previewTexture;
    public Material previewMaterial;
    public UnityEngine.UI.Text testScript;
    public void CapturePhoto()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
        testScript.text += "CreateAsync Initialized";
    }
    Resolution highestRes;
    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;
        highestRes = new Resolution();
        foreach (Resolution r in PhotoCapture.SupportedResolutions)
        {
            if (highestRes.width == 0 || highestRes.height == 0)
                highestRes = r;
            else
                if (highestRes.width < r.width && highestRes.height < r.height)
                highestRes = r;
        }

        Resolution cameraResolution = highestRes;

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        testScript.text += " Camera Captured";

        captureObject.StartPhotoModeAsync(c, false, OnPhotoModeStarted);

    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            testScript.text += "Photo mode started";
            photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if (result.success)
        {
            testScript.text += "Camera storing to memory";
            // Create our Texture2D for use and set the correct resolution
            Resolution cameraResolution = highestRes;
            Texture2D targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);
            // Copy the raw image data into our target texture
            photoCaptureFrame.UploadImageDataToTexture(targetTexture);
            previewTexture = targetTexture;
            testScript.text += "Stored to memory";
            TakeScreenshot();
            // Do as we wish with the texture such as apply it to a material, etc.
        }
        // Clean up
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Saved Photo to disk!");
            testScript.text += " Stopping Photo";
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
        }
        else
        {
            Debug.Log("Failed to save Photo to disk");
        }
    }

    public void TakeScreenshot()
    {
            ServerConnection.instance.previewImage = previewTexture;
            ServerConnection.instance.ImageTitle = "TestUpload";
            previewMaterial.mainTexture = previewTexture;
            StartCoroutine(SendToServer());
    }

    public PolygonTester GeneratePolygon;
    IEnumerator SendToServer()
    {
        yield return ServerConnection.instance.UploadFileCo();
        GeneratePolygon.GenerateMesh(ServerConnection.instance.arrayString);
    }
}
