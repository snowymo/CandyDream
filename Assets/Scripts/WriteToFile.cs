using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System;

public class WriteToFile {

    static string buildcsv(string[] content)
    {
        //before your loop
        StringBuilder csv = new StringBuilder();

        string format = "";
        //in your loop
        for(int i = 0; i < content.Length; i++)
        {
            format += "{" + i.ToString() + "},";
        }
        format.Remove(format.Length - 1);
        string newLine = string.Format(format, content);
        csv.AppendLine(newLine);
        return csv.ToString();
    }

	public static void write2csv(string path, string[] content)
    {
        if (!File.Exists(path))
        {
            File.Create(path);
        }
        //Debug.Log("before build " + Time.time);
        string newline = buildcsv(content);
        //Debug.Log("after build " + Time.time);
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path, true))
        {

            file.WriteLine(newline);
			//Debug.Log ("here we go " + Time.time);
        }
    }

	public static void writeheader(string path, string[] content)
	{
		if (!File.Exists(path))
		{
			File.Create(path);
		}
		string newline = buildcsv(content);
		StreamWriter file = new StreamWriter(@path, true);
		file.WriteLine(newline);
		file.Flush();
		file.Close ();
//			Debug.Log (newline);

	}
}
