using UnityEngine;

public class TapToSelectItem : MonoBehaviour
{

    public int iDotToActivate;
    public GameObject dotToActivate;
    public bool isActive;
    public SelectionManager selection;

    Vector3 originalScale;
    void Awake()
    {
        originalScale = transform.localScale;
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    public  void OnSelect()
    {
        //Load This Texture as the object to place
        ActivateThisDot();
    }

    public void ActivateThisDot()
    {
        //Load This Texture as the object to place
        Debug.Log("Activating Dot");
        selection.ActivateDot(dotToActivate);
    }

    public void OnHover()
    {
        isActive = true;
        iTween.ScaleTo(gameObject, new Vector3(gameObject.transform.localScale.x * 1.25f, gameObject.transform.localScale.y * 1.25f, gameObject.transform.localScale.z * 1.25f), .3f);
    }

    public void OnUnHover()
    {
        isActive = false;
        iTween.ScaleTo(gameObject, originalScale, .3f);
    }
}