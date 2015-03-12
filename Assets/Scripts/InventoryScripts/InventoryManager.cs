using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	public static IList<Item> inventory;
	public static InventoryManager invent;
	public static Inventory spawn;

	public static int invWeight = 0;
	public static int MAX_WEIGHT = 25;

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


	public static void addItem(Item item)
	{
		Item temp = (Item)ScriptableObject.CreateInstance<Item> ();
		temp = item;
		Debug.Log ("Name: " + temp.itemName);
		if(!checkWeight(temp))
		{
			if (temp.itemType == Item.ItemType.Weapon) 
			{
				temp = temp as Weapon;
				inventory.Add(item);
				spawn.addPrefab(item);
				//invWeight = invWeight+temp.itemWeight;
			}
			else if (temp.itemType == Item.ItemType.Food) 
			{
				temp = temp as Food;
				inventory.Add(temp);
				invWeight = invWeight+temp.itemWeight;
			}
			else if (temp.itemType == Item.ItemType.KeyItem) 
			{
				temp = temp as KeyItem;
				inventory.Add(temp);
				invWeight = invWeight+temp.itemWeight;
			}
		}
		else
		{
			Debug.Log ("Bag too Heavy");
		}

	}

	public static void removeItem(Item currItem)
	{
	}

	public static bool checkWeight(Item newItem)
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
}
