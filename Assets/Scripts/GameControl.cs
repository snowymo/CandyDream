using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

	[SerializeField]
	float score;

	public RecordText rt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rt.count = score;
	}

	void gameControl(){
		
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name.Contains("candy"))
		{
//			Destroy(col.gameObject);
			score += 1;
		}
	}
}
