﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GazeRecord : MonoBehaviour {

    public Transform[] comparedObjs;

    private Vector3[] compareVecs;

    public string gazeFilePath;

    void updatePos()
    {
        for (int i = 0; i < comparedObjs.Length; i++)
            compareVecs[i] = comparedObjs[i].position - Camera.main.transform.position;
        compareVecs[compareVecs.Length - 1] = Vector3.forward;
    }

	// Use this for initialization
	void Start () {
        compareVecs = new Vector3[comparedObjs.Length + 1];
        updatePos();
	}
	
	// Update is called once per frame
	void Update () {

        updatePos();

        RaycastHit hitInfo;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 20, Color.green);
        if (Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hitInfo,
                20.0f,
                Physics.DefaultRaycastLayers))
        {
            // If the Raycast has succeeded and hit a hologram
            // hitInfo's point represents the position being gazed at
            // hitInfo's collider GameObject represents the hologram being gazed at
            // here I record the angle between [forward,table,chair,other user]
            print("gaze at " + hitInfo.collider.name);
            string[] contents = new string[compareVecs.Length];
            for(int i = 0; i < comparedObjs.Length; i++)
            {
                float angle = Vector3.Angle(compareVecs[i], hitInfo.collider.gameObject.transform.position);
                contents[i] = angle.ToString();
                
            }
            WriteToFile.write2csv(Application.dataPath + "/record/" + gazeFilePath, contents);
        }
    }
}