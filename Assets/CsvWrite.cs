using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;


public class CsvWrite : MonoBehaviour {
	
	public static CsvWrite saveFile;
	private const string FILE_NAME = "PlayTestData.csv";
	
	private List<string[]> rowData = new List<string[]>();
	string startTime;
	public bool isFinished;
	private string filePath;
	
	void Start()
	{
		startTime = System.DateTime.Now.ToString ("HH:mm:ss");
		isFinished = false;
		filePath = Application.dataPath + "/" + FILE_NAME;
	}
	
	
	public void Save(){
		
		string[] rowDataTemp;
		if (!File.Exists (FILE_NAME)) {
			// Creating First row of titles manually..
			rowDataTemp = new string[4];
			rowDataTemp [0] = "Date";
			rowDataTemp [1] = "Start Time";
			rowDataTemp [2] = "End Time";
			rowDataTemp [3] = "Finished";
			rowData.Add (rowDataTemp);
			
			string[][] output = new string[rowData.Count][];
			
			for (int i = 0; i < output.Length; i++) {
				output [i] = rowData [i];
			}
			
			int length = output.GetLength (0);
			string delimiter = ",";
			
			StringBuilder sb = new StringBuilder ();
			
			for (int index = 0; index < length; index++) {
				sb.AppendLine (string.Join (delimiter, output [index]));
			}
			
			StreamWriter outStream = System.IO.File.CreateText (filePath);
			outStream.WriteLine (sb);
			outStream.Close ();
			
		}
		
		rowDataTemp = new string[4];
		rowDataTemp [0] = System.DateTime.Now.ToString ("MM/dd/yyyy");
		rowDataTemp [1] = startTime;
		rowDataTemp [2] = System.DateTime.Now.ToString ("HH:mm:ss");
		rowDataTemp [3] = isFinished.ToString();
		rowData.Add (rowDataTemp);
		
		string[][] outputAdd = new string[rowData.Count][];
		
		for (int i = 0; i < outputAdd.Length; i++) {
			outputAdd [i] = rowData [i];
		}
		
		int lengthAdd = outputAdd.GetLength (0);
		string delimiterAdd = ",";
		
		StringBuilder sbAdd = new StringBuilder ();
		
		for (int index = 0; index < lengthAdd; index++) {
			sbAdd.AppendLine (string.Join (delimiterAdd, outputAdd [index]));
		}
		
		using (StreamWriter sw = File.AppendText(filePath)) {
			sw.WriteLine (sbAdd);
			sw.Close ();
		}
	}
	
	
	
}
