using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIPRecord : MonoBehaviour
{

    public Transform compareObjs;

    public Transform becomparedObj;  // controller

    public string bipFilePath;

	static int maxWriteSize = 10000;
	[SerializeField]
	string[] contents;
	int contentIdx;

    // Use this for initialization
    void Start()
    {
		contents = new string[maxWriteSize];
		contentIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {
//        string[] contents = new string[1];
        float minDis = 20.0f;
        for (int i = 0; i < compareObjs.childCount; i++)
        {
            //calculate the closest one
            float curDis = Vector3.Distance(compareObjs.GetChild(i).position, becomparedObj.position);
            if (minDis > curDis)
                minDis = curDis;

        }
//        contents[0] = minDis.ToString();
		contents[contentIdx++] = minDis.ToString() + "\n";
		if (contentIdx >= maxWriteSize) {
			WriteToFile.write2csv(Application.dataPath + "/record/" + bipFilePath, contents);
			contentIdx = 0;
		}
//		WriteToFile.write2csv(Application.dataPath + "/record/" + bipFilePath, contents);
    }

	void OnApplicationQuit(){
		if (contentIdx > 0) {
			WriteToFile.write2csv(Application.dataPath + "/record/" + bipFilePath, contents);
			contentIdx = 0;
		}
	}

	void OnApplicationPause(){
		if (contentIdx > 0) {
			WriteToFile.write2csv (Application.dataPath + "/record/" + bipFilePath, contents);
			contentIdx = 0;
		}
	}
}
