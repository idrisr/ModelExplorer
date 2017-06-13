using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {
    Renderer rend;

    private Color originalColor;
    private Vector3 manipulationPreviousPosition;
    private Rigidbody rb;
    private bool isManipulating;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        rend.enabled = true;
        originalColor = rend.material.color;
        isManipulating = false;
    }

    public void GazeEntered()
    {
        // do nothing with the gaze if manipulating
        if (isManipulating) {
            return;
        }

        print(name + " :Gaze Entered");
        rend.material.color = Color.yellow;
    }

    public void GazeExited()
    {
        // do nothing with the gaze if manipulating
        if (isManipulating) {
            return;
        }

        print(name + " :Gaze Exited");
        rend.material.color = originalColor;
    }

    public void OnTapped()
    {
        print(name + " :On Tapped");
        rend.material.color = Color.red;
        isManipulating = true;
    }
    
    void PerformManipulationStart(Vector3 position)
    {
        // do nothing if not manipulating
        if (!isManipulating) {
            return;
        }

        print(name + " :Manipulate Start");
        rb.isKinematic = true;
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        // do nothing if not manipulating
        if (!isManipulating) {
            return;
        }

        print(name + " :Manipulate Update");
        Vector3 moveVector = Vector3.zero;
        // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
        moveVector = (position * 5) - manipulationPreviousPosition;
        // 4.a: Update the manipulationPreviousPosition with the current position.
        manipulationPreviousPosition = position;

        // 4.a: Increment this transform's position by the moveVector.
        transform.position += moveVector;
    }

    void PerformManipulationEnd() {
        // do nothing if not manipulating
        if (!isManipulating) {
            return;
        }

        print(name + " :Manipulate End");
        rb.isKinematic = false;
        isManipulating = false;
        rend.material.color = originalColor;
    }
}