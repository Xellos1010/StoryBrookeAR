using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Advertisements;

public class ServerConnection : MonoBehaviour {

    public static ServerConnection instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<ServerConnection>();
            return _instance;
        }
    }
    private static ServerConnection _instance;
    public Texture2D previewImage;
    public string ImageTitle;
    string uploadURL = "http://ec2-52-33-217-21.us-west-2.compute.amazonaws.com/contour/";
    public UnityEngine.UI.Text displayText;
    public string arrayString;
    /*
    MenuBarSceneLoader menuBarInstance
    {
        get
        {
            if (_menuBarInstance == null)
            {
                GameObject[] SceneObjects = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Main").GetRootGameObjects();
                foreach (GameObject rootObject in SceneObjects)
                {
                    if (rootObject.name.Contains("SceneManager"))
                    {
                        _menuBarInstance = rootObject.GetComponent<MenuBarSceneLoader>();
                    }
                }
            }
            return _menuBarInstance;
        }
    }*/



    public IEnumerator UploadFileCo()
    {
        
        byte[] bytes = previewImage.EncodeToJPG();
        WWWForm postForm = new WWWForm();
        postForm.AddField("file", bytes.ToString());//Convert.ToBase64String(bytes));
        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
        {
            Debug.Log(upload.text);
            arrayString = upload.text;
        }
        else
        {
            Debug.Log(upload.error);
        }
    }

   

    IEnumerator UploadTargetToServer(byte[] bytes)
    {
        WWWForm postForm = new WWWForm();
        postForm.AddBinaryData("file", bytes, previewImage.name + ".jpg", "text/plain");
        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
        {
            Debug.Log("upload done :" + upload.text);
            displayText.text = "upload done :" + upload.text;
            arrayString = upload.text;
        }
        else
        {
            Debug.Log("Error during upload: " + upload.error);
            displayText.text = "Error during upload: " + upload.error;
        }
    }

   
}