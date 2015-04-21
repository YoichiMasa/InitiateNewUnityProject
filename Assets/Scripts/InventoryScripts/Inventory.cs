using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public Camera inventoryCamera;
	public Vector3 spawn;
	public bool isVisible = false;
	public Transform thing;
	public InventoryManager invent;
	public static Inventory curr;
	public List<Item> view;

	void Awake()
	{
		if (curr == null) 
		{
			DontDestroyOnLoad(gameObject);
			curr = this;
		}
		else
		{
			if( curr != this)
			{
				Destroy(gameObject);
			}
		}
		inventoryCamera.enabled = isVisible;
		spawn = thing.position;
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.I))
		{
			isVisible = !isVisible;
			inventoryCamera.enabled = isVisible;
			HUDController.instance.BarBacking.enabled = isVisible;
			HUDController.instance.weightText.enabled = isVisible;

			if(isVisible)
			{
				HUDController.instance.HandleWeight();
				AudioController.instance.PlayItemSound(0);
			}
			if(!isVisible)
			{
				AudioController.instance.PlayItemSound(1);
			}
		}
	}

	public GameObject addPrefab(Item item)
	{
		Debug.Log ("Item is: " + item);
		GameObject tempPrefab = (GameObject) Resources.Load (item.itemName);
		GameObject temp = null;
		if (GameObject.Find (item.itemName))
		{
			temp = (GameObject)Instantiate (GameObject.Find (item.itemName), spawn, Quaternion.identity);
			Debug.Log("Cloned");
		}
		else
		{
			temp = (GameObject)Instantiate (tempPrefab, spawn, Quaternion.identity);
		}
		temp.name = item.itemName;
		return temp;
	}

}
