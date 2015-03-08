using UnityEngine;
using System.Collections;

public class QuitController : MonoBehaviour 
{
	public Camera quitGUI;
	public TextAsset instructionTextFile;
	private string instructions;

	void Awake()
	{
		quitGUI.enabled = false;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			quitGUI.enabled = !quitGUI.enabled;
		}

	}

	public void quitGame()
	{
		Application.Quit();
	}

}
