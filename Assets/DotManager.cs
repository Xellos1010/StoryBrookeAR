using UnityEngine;
using System.Collections;

public class DotManager : MonoBehaviour {

    public GameObject[] dots;

    public void ActivateDot(int dot)
    {
        for (int i = 0; i < dots.Length; i++)
            if (i != dot)
                dots[i].SetActive(false);
            else
                dots[i].SetActive(true);
    }

    public void ActivateDot(GameObject dot)
    {
        foreach(GameObject child in dots)
            if (child != dot)
                child.SetActive(false);
            else
                child.SetActive(true);
    }
}
