using UnityEngine;
using System.Collections;

public class SelectEnd : MonoBehaviour {
    public GazeGestureManager gazeManager;
    void OnSelect()
    {
        // On each Select gesture, toggle whether the user is in placing mode.
        gazeManager.tapToPlaceActive = !gazeManager.tapToPlaceActive;

        // If the user is in placing mode, display the spatial mapping mesh.
        if (gazeManager.tapToPlaceActive)
        {
            SpatialMapping.Instance.DrawVisualMeshes = true;
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            SpatialMapping.Instance.DrawVisualMeshes = false;

        }
    }
}
