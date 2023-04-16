using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    public float cellWidth = 4.625f;
    public float cellHeight = 4.625f;
    void Start()
    {
        // Set position of all the children of the grid by their position as a grid.
        int i = 0;
        foreach (Transform child in transform)
        {
            int x = i % 3;
            int y = i / 3;
            Debug.Log("x: " + x*cellWidth + " y: " + y*cellHeight);
            // set relative position to parent
            child.transform.localPosition = new Vector3(x*cellWidth, y*cellHeight, 0);
            i++;
        }
        
    }
}
