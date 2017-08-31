using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading;

public class GazeRecord : MonoBehaviour {

    public Transform[] comparedObjs;

    private Vector3[] compareVecs;

    public string gazeFilePath;

	static int maxWriteSize = 10000;
	string[] contents;
	int contentIdx;

    bool _threadRunning;
    Thread _thread;

    string datapath;

    void updatePos()
    {
        for (int i = 0; i < comparedObjs.Length; i++)
            compareVecs[i] = comparedObjs[i].position - Camera.main.transform.position;
		compareVecs[compareVecs.Length - 1] = Camera.main.transform.rotation * Vector3.forward;
    }

	// Use this for initialization
	void Start () {
        compareVecs = new Vector3[comparedObjs.Length + 1];
        updatePos();
		contents = new string[maxWriteSize + comparedObjs.Length];
		contentIdx = 0;

		string[] header = new string[4];
		header [0] = "chair to collider angle";
		header [1] = "table to collider angle";
		header [2] = "audience to collider angle";
//		header [3] = "gaze to collider angle";
		WriteToFile.writeheader(Application.dataPath + "/record/" + gazeFilePath, header, 3);
        //		WriteToFile.writeheader(Application.dataPath + "/record/" + gazeFilePath, header);
        datapath = Application.dataPath;
    }
	
	// Update is called once per frame
	void Update () {

        updatePos();

        //RaycastHit hitInfo;
        Debug.DrawRay(Camera.main.transform.position, (compareVecs[compareVecs.Length - 1]) * 20, Color.green);
        //if (Physics.Raycast(
        //        Camera.main.transform.position,
        //        Camera.main.transform.forward,
        //        out hitInfo,
        //        20.0f,
        //        Physics.DefaultRaycastLayers))
        //{
            // If the Raycast has succeeded and hit a hologram
            // hitInfo's point represents the position being gazed at
            // hitInfo's collider GameObject represents the hologram being gazed at
            // here I record the angle between [forward,table,chair,other user]
            //print("gaze at " + hitInfo.collider.name);
            //            string[] contents = new string[compareVecs.Length];
            //print("gaze start " + Time.time);
            for(int i = 0; i < comparedObjs.Length; i++)
            {
                float angle = Vector3.Angle(compareVecs[i], compareVecs[compareVecs.Length-1]);
//				print ("contentIdx " + contentIdx);
				contents[contentIdx++] = angle.ToString();
            }
			//contents [contentIdx-1] += "\n";
			if (contentIdx >= maxWriteSize) {
                string[] copycontents = new string[contents.Length];
                contents.CopyTo(copycontents, 0);
                _thread = new Thread(ThreadedWork);
                _thread.Start(copycontents);
                //print ("gaze time " + Time.time);
               // WriteToFile.write2csv(Application.dataPath + "/record/" + gazeFilePath, contents);
				contentIdx = 0;
				//print ("gaze time " + Time.time);
			}
            //print("gaze end " + Time.time);
        //}
    }

    void ThreadedWork(object copycontents)
    {
        _threadRunning = true;
        bool workDone = false;

        // This pattern lets us interrupt the work at a safe point if neeeded.
        while (_threadRunning && !workDone)
        {
            WriteToFile.write2csv(datapath + "/record/" + gazeFilePath, (string[])copycontents, 3);
            workDone = true;
        }
        _threadRunning = false;
    }

    void OnApplicationQuit(){
		if (contentIdx > 0) {
			WriteToFile.write2csv(Application.dataPath + "/record/" + gazeFilePath, contents, 3);
			contentIdx = 0;
		}
        if (_threadRunning)
        {
            // This forces the while loop in the ThreadedWork function to abort.
            _threadRunning = false;

            // This waits until the thread exits,
            // ensuring any cleanup we do after this is safe. 
            _thread.Join();
        }
    }

	void OnApplicationPause(){
		if (contentIdx > 0) {
			WriteToFile.write2csv (Application.dataPath + "/record/" + gazeFilePath, contents, 3);
			contentIdx = 0;
		}
        if (_threadRunning)
        {
            // This forces the while loop in the ThreadedWork function to abort.
            _threadRunning = false;

            // This waits until the thread exits,
            // ensuring any cleanup we do after this is safe. 
            _thread.Join();
        }
    }
}
