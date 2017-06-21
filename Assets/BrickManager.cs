using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;


public class BrickManager : MonoBehaviour, IManipulationHandler {
    private Rigidbody rb;
    private Collider collider;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
	}

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationUpdated(ManipulationEventData eventData) {
    }

    public void OnManipulationCompleted(ManipulationEventData eventData) {
    }

    public void OnManipulationCanceled(ManipulationEventData eventData) {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
