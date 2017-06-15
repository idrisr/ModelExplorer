using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {
    Renderer rend;

    private Color originalColor;
    private Vector3 manipulationPreviousPosition;
    private Rigidbody rb;
    private Collider collider;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
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
        rend.material.color = Color.red;
        print(name + " :Manipulate Start");
        rb.isKinematic = true;
        collider.enabled = false;
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        print(name + " :Manipulate Update");
        Vector3 moveVector = Vector3.zero;

        Vector3 denoisedPosition = position;

        // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
        moveVector = (denoisedPosition * 2) - manipulationPreviousPosition;
        // 4.a: Update the manipulationPreviousPosition with the current position.
        manipulationPreviousPosition = denoisedPosition;

        // 4.a: Increment this transform's position by the moveVector.
        transform.position += moveVector;
    }

    void PerformManipulationEnd() {
        print(name + " :Manipulate End");
        rb.isKinematic = false;
        collider.enabled = true;
        rend.material.color = originalColor;
    }

    private Vector3 DeNoiseVector(Vector3 original)
    {
        float x = Mathf.Abs(original.x);
        float y = Mathf.Abs(original.y);
        float z = Mathf.Abs(original.z);

        Vector3 v;
        // x max
        if (x > y && x > z)
        {
            v.x = original.x;
            v.y = 0;
            v.z = 0;
        }
        // y max
        else if (y > x && y > z)
        {
            v.x = 0;
            v.y = original.y;
            v.z = 0;
        }
        // z max
        else 
        {
            v.x = 0;
            v.y = 0;
            v.z = original.z;
        }

        return v;
    }
}