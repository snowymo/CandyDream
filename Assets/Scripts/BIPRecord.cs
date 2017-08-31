using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BIPRecord : MonoBehaviour
{

    public Transform compareObjs;

    public Transform becomparedObj;  // controller

    public string bipFilePath;

	static int maxWriteSize = 10000;
	//[SerializeField]
	string[] contents;
	int contentIdx;

    bool _threadRunning;
    Thread _thread;

    string datapath;

    // Use this for initialization
    void Start()
    {
		contents = new string[maxWriteSize];
		contentIdx = 0;

		string[] header = new string[1];
		header[0] = "controllerToClosetCandies";

        datapath = Application.dataPath;

        WriteToFile.writeheader(Application.dataPath + "/record/" + bipFilePath, header, 1);
        //		WriteToFile.writeheader(Application.dataPath + "/record/" + bipFilePath, header);

        
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
		//contents[contentIdx++] = minDis.ToString() + "\n";
        
		if (contentIdx >= maxWriteSize) {
            string[] copycontents = new string[contents.Length];
            contents.CopyTo(copycontents, 0);
            print ("BIP: " + Time.time);
            //StartCoroutine(YieldingWork());
           // if (_thread.ThreadState == ThreadState.Unstarted)
            _thread = new Thread(ThreadedWork);
            _thread.Start(copycontents);
            //else
            /*{
                _thread.Join();
                _thread.Start(copycontents);
            }*/
            //WriteToFile.write2csv(Application.dataPath + "/record/" + bipFilePath, contents);
            contentIdx = 0;
			print (Time.time);
		}
//		WriteToFile.write2csv(Application.dataPath + "/record/" + bipFilePath, contents);
    }
    
    void ThreadedWork(object copycontents)
    {
        _threadRunning = true;
        bool workDone = false;

        // This pattern lets us interrupt the work at a safe point if neeeded.
        while (_threadRunning && !workDone)
        {
            WriteToFile.write2csv(datapath + "/record/" + bipFilePath, (string[])copycontents, 1);
            workDone = true;
        }
        _threadRunning = false;
    }

    IEnumerator YieldingWork()
    {
        bool workDone = false;

        while (!workDone)
        {
            // Let the engine run for a frame.
            yield return null;

            // Do Work...
            WriteToFile.write2csv(Application.dataPath + "/record/" + bipFilePath, contents, 1);
            workDone = true;
        }
        
    }

	void OnApplicationQuit(){
		if (contentIdx > 0) {
			WriteToFile.write2csv(Application.dataPath + "/record/" + bipFilePath, contents, 1);
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
			WriteToFile.write2csv (Application.dataPath + "/record/" + bipFilePath, contents, 1);
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
