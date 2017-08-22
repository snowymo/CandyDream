using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIPRecord : MonoBehaviour
{

    public Transform compareObjs;

    public Transform becomparedObj;  // controller

    public string disFilePath;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string[] contents = new string[1];
        float minDis = 20.0f;
        for (int i = 0; i < compareObjs.childCount; i++)
        {
            //calculate the closest one
            float curDis = Vector3.Distance(compareObjs.GetChild(i).position, becomparedObj.position);
            if (minDis > curDis)
                minDis = curDis;

        }
        contents[0] = minDis.ToString();
        WriteToFile.write2csv(Application.dataPath + "/record/" + disFilePath, contents);
    }
}
