using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public Camera inventoryCamera;
	public Vector3 spawn;
	public bool isVisible = false;
	public Transform thing;

	void Awake()
	{
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
		}
	}

	public void addPrefab(Item item)
	{
			Debug.Log ("Item is: " + item);
			Instantiate (item.itemSprite, spawn, Quaternion.identity);

	}

}
