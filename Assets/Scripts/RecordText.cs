using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordText : MonoBehaviour {

	public TextMesh countText;

	public float count;

	// Use this for initialization
	void Start () {
		//countText = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
//		count = Random.Range (5, 10);
		countText.text = "Record: " + count.ToString ();
	}
}
