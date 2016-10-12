using UnityEngine;
using System.Collections;

public class TapToPlaceParent : MonoBehaviour
{
    public GazeGestureManager gazeManager;
    void Start()
    {
        SpatialMapping.Instance.DrawVisualMeshes = false;
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        Debug.Log("On Select Active for " + gameObject.name);
        // On each Select gesture, toggle whether the user is in placing mode.
        gazeManager.tapToPlaceActive = !gazeManager.tapToPlaceActive;

        // If the user is in placing mode, display the spatial mapping mesh.
        if (gazeManager.tapToPlaceActive)
        {
            Debug.Log("gazeManager.tapToPlaceActive now running deselect and having object follow gaze");
            //SpatialMapping.Instance.DrawVisualMeshes = true;
            //SpatialMapping.Instance.DrawVisualMeshes = false;
            StartCoroutine(Deselect());
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            //SpatialMapping.Instance.DrawVisualMeshes = false;
            //SpatialMapping.Instance.DrawVisualMeshes = false;
            StopAllCoroutines();
        }
    }

    IEnumerator Deselect()
    {
        yield return new WaitForSeconds(5);
        //SpatialMapping.Instance.DrawVisualMeshes = false;
        gazeManager.tapToPlaceActive = false;
        lastRotation = new Quaternion();
    }
    Quaternion lastRotation;
    // Update is called once per frame
    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (gazeManager.tapToPlaceActive)
        {
            Debug.Log("parent of " + gameObject.name + " is being moved");
            if (!SpatialMapping.Instance.DrawVisualMeshes)
                SpatialMapping.Instance.DrawVisualMeshes = true;
            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                Debug.Log("Physics Raycast Mask was hit!!!");
                // Move this object's parent object to
                // where the raycast hit the Spatial Mapping mesh.
                transform.parent.position = hitInfo.point;

                // Rotate this object's parent object to face the user.
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                transform.parent.rotation = toQuat;
            }
            else
            {
                if(lastRotation != null)
                    if(lastRotation.eulerAngles != Camera.main.transform.rotation.eulerAngles)
                    {
                        //Apply
                    }
                Debug.Log("SpatialMapping Mask not Active");
                                
            }

            lastRotation = Camera.main.transform.rotation;
        }
        else
        {
            if (SpatialMapping.Instance.DrawVisualMeshes)
                SpatialMapping.Instance.DrawVisualMeshes = false;
        }
    }
}