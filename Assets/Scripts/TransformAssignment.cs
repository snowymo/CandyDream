using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAssignment : MonoBehaviour {

    public Transform tracked_transform;

    //public Vector3 startRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = tracked_transform.position;
        transform.rotation = tracked_transform.rotation;
	}
}
