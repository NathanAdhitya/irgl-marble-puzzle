using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayGridCreator : MonoBehaviour
{
    public GameObject overlayGridSquarePrefab;
    void Start()
    {
        // Create 3x2 the overlay grid squares from the prefab
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject overlayGridSquare = Instantiate(overlayGridSquarePrefab);
                overlayGridSquare.transform.SetParent(transform, false);
            }
        }
    }

}
