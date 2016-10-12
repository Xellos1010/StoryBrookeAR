using UnityEngine;

public class TapToReturnToMenu : MonoBehaviour
{
    
    public GameObject MenuToActivate;
    public GameObject ObjectHolderDisable;
    public bool loadPhotoScene;
    public bool loadMenu;
    // Called by GazeGestureManager when the user performs a Select gesture
    public  void OnSelect()
    {
        //Load This Texture as the object to place
        if (loadPhotoScene)
            LoadScene();
        else if (loadMenu)
            LoadSceneMenu();
        else
            ActivateMenu();
    }

    public void ActivateMenu()
    {
        //Load This Texture as the object to place
        Debug.Log("Activating Dot");
        MenuToActivate.SetActive(true);
        ObjectHolderDisable.SetActive(false);
    }

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TakePicture");
    }
    public void LoadSceneMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}