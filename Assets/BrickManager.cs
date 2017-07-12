using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;


public class BrickManager : MonoBehaviour, IManipulationHandler {
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        rb.isKinematic = true;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {

    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        rb.isKinematic = false;
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        rb.isKinematic = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
