using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeTakePictureManager : MonoBehaviour {

    public static GazeTakePictureManager Instance { get; private set; }
    
    GestureRecognizer recognizer;

    // Use this for initialization
    void Awake()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            
        };
        recognizer.StartCapturingGestures();
    }

    bool ObjectHeld = false;
    GameObject heldObject;

}
