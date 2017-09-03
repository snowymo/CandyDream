using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAnimatorCtrl : MonoBehaviour {

    Vector3 lastPos, curPos;

    float dis;

    public float speed;

    public float speedThres;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        curPos = GetComponent<ApplyTheme>().tracked_transform.position;
        dis = Vector3.Distance(curPos, lastPos);
        speed = dis / Time.deltaTime;
        
            GetComponent<ApplyTheme>().curObj.GetComponent<Animator>().SetBool("Walk", (speed > speedThres));
        
        
        lastPos = curPos;

    }
}
