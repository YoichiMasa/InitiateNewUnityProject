using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager: MonoBehaviour{
	public static IList<Item> itemDatabase;
	public static ItemManager im;
	void Awake () 
	{
		if (im == null) 
		{
			DontDestroyOnLoad(gameObject);
			itemDatabase = new List<Item>();
			im = this;
		}
		else
		{
			if(im != this)
			{
				Destroy(gameObject);
			}
		}
		Item newItem = (Item)ScriptableObject.CreateInstance<Item> ();
		GameObject go = Resources.Load ("Crowbar") as GameObject;
		newItem.ConfigureItem ("Crowbar", 100, "It's a Box", 1, Item.ItemType.Weapon, go);
		itemDatabase.Add (newItem);
	}

	public static Item getItem(int Id)
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
