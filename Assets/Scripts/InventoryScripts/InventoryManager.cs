using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	private IList<Item> inventory;
	public int invWeight = 0;
	public int MAX_WEIGHT = 25;

	protected InventoryManager()
	{
		inventory = new List<Item>();
	}
	
	private static InventoryManager instance = null;
	
	public static InventoryManager getInstance
	{
		get
		{
			if (InventoryManager.instance == null)
			{
				instance = GameObject.FindObjectOfType<InventoryManager>();
				//InventoryManager.instance = new InventoryManager();
			}
			return instance;
		}
	}

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			if(this != instance)
			{
				Destroy(this.gameObject);
			}
		}
	}

	public void addItem(Item newItem)
	{

		Item temp;
		if(!checkWeight(newItem))
		if (newItem.itemType == Item.ItemType.Weapon) 
		{
			temp = (Weapon) newItem;
			inventory.Add(temp);
			invWeight = invWeight+temp.itemWeight;
		}
		else if (newItem.itemType == Item.ItemType.Food) 
		{
			temp = (Food) newItem;
			inventory.Add(temp);
			invWeight = invWeight+temp.itemWeight;
		}
		else if (newItem.itemType == Item.ItemType.KeyItem) 
		{
			temp = (KeyItem) newItem;
			inventory.Add(temp);
			invWeight = invWeight+temp.itemWeight;
		}

	}

	public bool checkWeight(Item newItem)
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
