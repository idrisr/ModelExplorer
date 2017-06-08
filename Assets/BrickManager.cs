using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {
    Renderer rend;
    private Color originalColor;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        originalColor = rend.material.color;
    }

    public void GazeEntered()
    {
        rend.material.color = Color.red;
    }

    public void GazeExited()
    {
        rend.material.color = originalColor;
    }
}
