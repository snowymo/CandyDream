using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisRecord : MonoBehaviour {

    public Transform[] compareObjs;

    public Transform[] becomparedObjs;  // pos, controller

    public string disFilePath;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        string[] contents = new string[becomparedObjs.Length * compareObjs.Length];
        for (int j = 0; j < becomparedObjs.Length; j++)
        {
            for (int i = 0; i < compareObjs.Length; i++)
            {
                float dis = Vector3.Distance(compareObjs[i].position, becomparedObjs[j].position);
                contents[j * compareObjs.Length + i] = dis.ToString();
            }
            
        }
        WriteToFile.write2csv(Application.dataPath + "/record/" + disFilePath, contents);
    }
}
