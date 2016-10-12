using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour
{
    public GameObject ObjectHolder;
    public DotManager dotManager;
    bool selectMenuActive = false;

    void Awake()
    {
        ObjectHolder.SetActive(false);
        selectMenuActive = true;
    }

    void OnEnable()
    {
        selectMenuActive = true;
    }

    void OnDisable()
    {
        selectMenuActive = false;
    }

    public void ActivateDot(GameObject dot)
    {
        ObjectHolder.SetActive(true);
        Debug.Log("Object Holder Active");
        dotManager.ActivateDot(dot);
        Debug.Log("Activating Dot Object");
        gameObject.SetActive(false);
        Debug.Log("gameobject set active false");
    }

    GameObject FocusedObject;
    // Update is called once per frame
    void Update()
    {
        if (selectMenuActive)
        {
            // Figure out which hologram is focused this frame.
            GameObject oldFocusObject = FocusedObject;

            // Do a raycast into the world based on the user's
            // head position and orientation.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
            {
                //Debug.Log("Selection Manager hit something!!! " + hitInfo.collider.gameObject.name);
                // If the raycast hit a hologram, use that as the focused object.
                FocusedObject = hitInfo.collider.gameObject;
                //Change Focused Object to not Active
                TapToSelectItem focusedItemSelect = FocusedObject.GetComponent<TapToSelectItem>();
                if (!focusedItemSelect.isActive)
                    focusedItemSelect.OnHover();
            }
            else
            {
                // If the raycast did not hit a hologram, clear the focused object.
                FocusedObject = null;
                //Debug.Log("Selection Manager hit Nothing!!! " + gameObject.name);
            }

            // If the focused object changed this frame,
            // start detecting fresh gestures again.
            if (FocusedObject != oldFocusObject)
            {
                //Change Focused Object to not Active
                try
                {
                    TapToSelectItem focusedItemSelect = oldFocusObject.GetComponent<TapToSelectItem>();
                    if (focusedItemSelect.isActive)
                        focusedItemSelect.OnUnHover();
                }
                catch
                {
                    Debug.Log("Old Focused Object doesn't exist");
                }
            }
        }
    }
}