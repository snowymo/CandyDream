using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class femailAnimateController : MonoBehaviour {

    //public float test;

    Vector3 lastPos, curPos;

    public float dis;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        curPos = GetComponent<ApplyTheme>().tracked_transform.position;
        dis = Vector3.Distance(curPos, lastPos);
        GetComponent<ApplyTheme>().curObj.GetComponent<Animator>().SetFloat("distance",dis);
        lastPos = curPos;

	}
}
