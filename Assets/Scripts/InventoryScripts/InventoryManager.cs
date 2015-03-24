using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	public List<Item> inventory;
	public  InventoryManager invent;
	public  Inventory spawn;
	public  MoveItem move;

	public  int inventIndex = 0;
	public  Item selectedItem;

	public  int invWeight = 0;
	public  int MAX_WEIGHT = 25;

	public Camera inventCam;
	public bool visible;
	public  bool select = false;

	void Awake()
	{
		spawn = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		if (invent == null) 
		{
			DontDestroyOnLoad(gameObject);
			inventory = new List<Item>();	
			invent = this;
		}
		else
		{
			if( invent != this)
			{
				Destroy(gameObject);
			}
		}
	}

	void Update()
	{
		if (inventCam.enabled == true) {
			visible = true;
		} else {
			visible = false;
		}

		if(visible == true)
		{
			if(inventory.Count > 0)
			{
				if(Input.GetKeyDown(KeyCode.Space))
				{
					selectItem();
				}
				Debug.Log (inventIndex);

			}
		}
		else
		{
			if(inventory.Count > 0 && selectedItem != null)
			{
				selectedItem.isSelected = select;
			}
		}


		if(Input.GetKeyDown(KeyCode.KeypadEnter)&& inventory.Count > 0)
		{

			inventory.RemoveAt(inventIndex);
			Destroy(selectedItem.itemSprite);
			selectedItem = null;
			selectItem ();
			Debug.Log (inventory.Count);
		}
	}

	public  void addItem(Item item)
	{
		Item temp = (Item)ScriptableObject.CreateInstance<Item> ();
		temp = item;
		Debug.Log ("Name: " + temp.itemName);
		if(!checkWeight(temp))
		{
			if (temp.itemType == Item.ItemType.Weapon) 
			{
				temp.itemSprite = spawn.addPrefab(temp);
				inventory.Add(temp);
				invWeight = invWeight+temp.itemWeight;
			}
			else if (temp.itemType == Item.ItemType.Food) 
			{
				inventory.Add(temp);
				invWeight = invWeight+temp.itemWeight;
			}
			else if (temp.itemType == Item.ItemType.KeyItem) 
			{
				temp.itemSprite = spawn.addPrefab(temp);
				inventory.Add(temp);
				invWeight = invWeight+temp.itemWeight;
			}
		}
		else
		{
			Debug.Log ("Bag too Heavy");
		}

	}

	public  void removeItem(Item currItem)
	{
	}

	public  bool checkWeight(Item newItem)
	{
		Item temp = newItem;
		bool full = false;
		if (temp.itemWeight + invWeight > MAX_WEIGHT) 
		{
			full = true;
			return full;
		}
		else
		{
			return full;
		}
	}

	public  void selectItem()
	{
		if(selectedItem != null /*&& selectedItem.itemSprite != null*/)
		{
			Debug.Log ("Got in");
			selectedItem.isSelected = select;
			selectedItem.itemSprite.GetComponent<MoveItem>().swapSprite();

		}
		if ((inventIndex+1) < inventory.Count) 
		{
			//selectedItem.isSelected = select;
			inventIndex++;
			selectedItem = inventory[inventIndex];
			selectedItem.itemSprite.GetComponent<MoveItem>().swapSprite();
			Debug.Log (selectedItem.itemName+" "+inventIndex);
			selectedItem.isSelected = !select;

		}
		else
		{
			inventIndex = 0;
			selectedItem = inventory[inventIndex];
			selectedItem.itemSprite.GetComponent<MoveItem>().swapSprite();
			selectedItem.isSelected = !select;
		}
	}
}
