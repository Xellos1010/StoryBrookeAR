using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;
    public bool tapToPlaceActive = false;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        tapToPlaceActive = false;
        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();
    }

    bool ObjectHeld = false;
    GameObject heldObject;

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo1;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo1))
            {
                Debug.Log("Selection Manager hit something!!! " + hitInfo1.collider.gameObject.name);
                // If the raycast hit a hologram, use that as the focused object.
                FocusedObject = hitInfo1.collider.gameObject;
                FocusedObject.SendMessageUpwards("OnSelect");
                /*try
                {
                    TapToSelectItem tapToSelect = hitInfo1.collider.gameObject.GetComponent<TapToSelectItem>();
                    Debug.Log("Tap To Select Component Found");
                    tapToSelect.ActivateThisDot();
                }
                catch
                {
                    Debug.Log("Object is not Selectable" + FocusedObject.name);
                }*/

            }
            else
            {
                Debug.Log("Gaze Manager hit Nothing!!! " + gameObject.name);
            }
        }
        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
            
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}