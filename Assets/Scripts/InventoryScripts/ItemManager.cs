using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager{
	public IList<Item> itemDatabase;
	protected ItemManager()
	{
		itemDatabase = new List<Item>();
	}
	private static ItemManager instance = null;

	public static ItemManager getInstance
	{
		get
		{
			if (ItemManager.instance == null)
			{
				ItemManager.instance = new ItemManager();
			}
			return instance;
		}
	}

	public Item getItem(int Id)
	{
		Item newItem = (Item)ScriptableObject.CreateInstance<Item> ();
		for (int i = 0; i < itemDatabase.Count; i++) 
		{
			if(itemDatabase[i].itemId == Id)
			{
				newItem = itemDatabase[i];
			}
		}
		return newItem;
	}

			
}
