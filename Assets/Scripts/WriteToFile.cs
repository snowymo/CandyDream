using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System;

public class WriteToFile {

    static string buildcsv(string[] content, int perline)
    {
        //before your loop
        StringBuilder csv = new StringBuilder();

        string format = "";
        //in your loop
        for(int i = 0; i < content.Length; i++)
        {
            
            format += "{" + i.ToString() + "},";
            if ((i+1) % perline == 0)
                format += "\n";
        }
        //format.Remove(format.Length - 1);
        string newLine = string.Format(format, content);
        csv.AppendLine(newLine);
        return csv.ToString();
    }

	public static void write2csv(string path, string[] content, int perline)
    {
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            File.SetAttributes(path, FileAttributes.Normal);
        }
        //Debug.Log("before build " + Time.time);
        string newline = buildcsv(content, perline);
        //Debug.Log("after build " + Time.time);
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path, true))
        {

            file.WriteLine(newline);
			//Debug.Log ("here we go " + Time.time);
        }
    }

	public static void writeheader(string path, string[] content, int perline)
	{
		if (!File.Exists(path))
		{
			File.Create(path).Dispose();
            File.SetAttributes(path, FileAttributes.Normal);
        }
		string newline = buildcsv(content, perline);
		StreamWriter file = new StreamWriter(@path, true);
		file.WriteLine(newline);
		file.Flush();
		file.Close ();
//			Debug.Log (newline);

	}
}
