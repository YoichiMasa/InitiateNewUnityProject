using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public Camera inventoryCamera;
	public bool isVisible = false;
	public InventoryManager inventory;
	public ItemManager itemManager;

	void Awake()
	{
		itemManager = ItemManager.getInstance;
		inventory = InventoryManager.getInstance;
		inventoryCamera.enabled = isVisible;
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



	public void addItem(int Id)
	{
		Item temp = itemManager.getItem (Id);
		inventory.addItem (temp);
	}


	public void useItem()
	{

	}
}
