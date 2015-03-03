using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

//	public TextAsset instructionTextFile;
//	private string instructions;
	public GameObject instructionWindow;
	private bool isWindowActive = false;

	void Awake()
	{
		instructionWindow.SetActive (isWindowActive);
//		instructions = instructionTextFile.text;
	}

	public void giveInstruct()
	{
		isWindowActive = !isWindowActive;
		instructionWindow.SetActive (isWindowActive);
	}

	public void startNewGame()
	{
		Application.LoadLevel (1);
	}

	public void quitGame()
	{
		Application.Quit();
	}
}
