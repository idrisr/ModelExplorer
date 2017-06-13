﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {
    Renderer rend;

    private Color originalColor;
    private Vector3 manipulationPreviousPosition;
    private Rigidbody rb;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        rend.enabled = true;
        originalColor = rend.material.color;
    }

    public void GazeEntered()
    {
        print(name + " :Gaze Entered");
        rend.material.color = Color.yellow;
    }

    public void GazeExited()
    {
        print(name + " :Gaze Exited");
        rend.material.color = originalColor;
    }

    public void OnTapped()
    {
        print(name + " :On Tapped");
        rend.material.color = Color.red;
    }
    
    void PerformManipulationStart(Vector3 position)
    {
        print(name + " :Manipulate Start");
        rb.isKinematic = true;
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        print(name + " :Manipulate Update");
        Vector3 moveVector = Vector3.zero;
        // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
        moveVector = (position * 2) - manipulationPreviousPosition;
        // 4.a: Update the manipulationPreviousPosition with the current position.
        manipulationPreviousPosition = position;

        // 4.a: Increment this transform's position by the moveVector.
        transform.position += moveVector;
    }

    void PerformManipulationEnd() {
        print(name + " :Manipulate End");
        rb.isKinematic = false;
        rend.material.color = originalColor;
    }
}