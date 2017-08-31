using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// rotation.x rotation.z = 0 all the time
// translation.y -= 1.7
public class audienceHelper : MonoBehaviour {

    public Transform refTrans;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0, refTrans.rotation.eulerAngles.y, 0);
        transform.position = new Vector3(refTrans.position.x,0,refTrans.position.z);

    }
}
