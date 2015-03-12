using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {
	public static GameController control;


	void Awake () 
	{
		if (control == null) 
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else
		{
			if(control != this)
			{
				Destroy(gameObject);
			}
		}

	}

	public void Save()
	{
		BinaryFormatter saver = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/saveData.dat");

		SaveData info = new SaveData ();
		info.inventory = new List<Item> ();
		for(int i = 0; i < InventoryManager.inventory.Count; i++)
		{
			info.inventory.Add(InventoryManager.inventory[i]);
		}

		saver.Serialize (file, info);
		file.Close ();
	}

	public void Load()
	{
		if(File.Exists (Application.persistentDataPath + "/saveData.dat"))
		{
			BinaryFormatter loader = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/saveData.dat", FileMode.Open);
			SaveData info = (SaveData)loader.Deserialize(file);
			file.Close ();
			for(int i = 0; i < info.inventory.Count; i++)
			{
				InventoryManager.inventory.Add (info.inventory[i]);
			}
		}
	}
	

}

[Serializable]
class SaveData
{
	public IList<Item> inventory;
}
