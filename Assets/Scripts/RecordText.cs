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

	void OnApplicationQuit()
    {
        WriteToFile.write2csv(Application.dataPath + "/record/score.csv", new string[1] { count.ToString() }, 1);
    }

	void OnApplicationPause(){
		WriteToFile.write2csv(Application.dataPath + "/record/score.csv", new string[1] { count.ToString() }, 1);
	}
}
