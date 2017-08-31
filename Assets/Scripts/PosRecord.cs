using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PosRecord : MonoBehaviour {

    public Transform[] logObjs;
    

    public string posFilePath;

	static int maxWriteSize = 15000;
	//[SerializeField]
	string[] contents;
	int contentIdx;

    bool _threadRunning;
    Thread _thread;

    string datapath;

    // Use this for initialization
    void Start () {
		contents = new string[maxWriteSize+logObjs.Length];
		contentIdx = 0;

		string[] header = new string[8];
		header [0] = "user x";
		header [1] = "user y";
		header [2] = "table x";
		header [3] = "table y";
		header [4] = "chair x";
		header [5] = "chair y";
        header[6] = "audience x";
        header[7] = "audience y";
		WriteToFile.writeheader(Application.dataPath + "/record/" + posFilePath, header, 8);
        //		WriteToFile.writeheader(Application.dataPath + "/record/" + disFilePath, header);

        datapath = Application.dataPath;
    }
	
	// Update is called once per frame
	void Update () {
//        string[] contents = new string[becomparedObjs.Length * compareObjs.Length];
        for (int i = 0; i < logObjs.Length; i++)
        {
//                contents[j * compareObjs.Length + i] = dis.ToString();
			contents[contentIdx++] = logObjs[i].position.x.ToString();
            contents[contentIdx++] = logObjs[i].position.y.ToString();
        }
		//xcontents [contentIdx-1] += "\n"; 
		if (contentIdx >= maxWriteSize) {
            string[] copycontents = new string[contents.Length];
            contents.CopyTo(copycontents, 0);
            _thread = new Thread(ThreadedWork);
            _thread.Start(copycontents);

            print ("dis " + Time.time);
			//WriteToFile.write2csv(Application.dataPath + "/record/" + disFilePath, contents);
			contentIdx = 0;
			print ("dis " + Time.time);
		}
//        WriteToFile.write2csv(Application.dataPath + "/record/" + disFilePath, contents);
    }

    void ThreadedWork(object copycontents)
    {
        _threadRunning = true;
        bool workDone = false;

        // This pattern lets us interrupt the work at a safe point if neeeded.
        while (_threadRunning && !workDone)
        {
            WriteToFile.write2csv(datapath + "/record/" + posFilePath, (string[])copycontents, 8);
            workDone = true;
        }
        _threadRunning = false;
    }

    void OnApplicationQuit(){
		if (contentIdx > 0) {
			WriteToFile.write2csv(Application.dataPath + "/record/" + posFilePath, contents, 8);
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
			WriteToFile.write2csv (Application.dataPath + "/record/" + posFilePath, contents, 8);
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
