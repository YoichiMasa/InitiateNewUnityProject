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
		Weapon newItem = (Weapon)ScriptableObject.CreateInstance<Weapon> ();
		//GameObject go = Resources.Load ("Crowbar") as GameObject;
		newItem.ConfigureItem ("Crowbar", 100, "It's a Box", 1, Item.ItemType.Weapon, null, 10, 10);
		KeyItem newThing = (KeyItem)ScriptableObject.CreateInstance<KeyItem> ();
		//GameObject go = Resources.Load ("Crowbar") as GameObject;
		newThing.ConfigureItem ("Sledgehammer", 101, "It's a Box", 1, Item.ItemType.KeyItem, null);
		itemDatabase.Add (newItem);
		itemDatabase.Add (newThing);
	}

	public static Item getItem(int Id)
	{
		for (int i = 0; i < itemDatabase.Count; i++) 
		{
			if(itemDatabase[i].itemId == Id)
			{
				if(itemDatabase[i].itemType == Item.ItemType.Weapon)
				{
					Weapon newItem = (Weapon)ScriptableObject.CreateInstance<Weapon> ();
					Weapon temp = (Weapon)itemDatabase[i];
					newItem.ConfigureItem (temp.itemName, temp.itemId, temp.itemDesc, temp.itemWeight, temp.itemType, temp.itemSprite, temp.wpnDamage, temp.wpnDurability);
					return newItem;
				}
				else if(itemDatabase[i].itemType == Item.ItemType.Food)
				{
					Food newItem = (Food)ScriptableObject.CreateInstance<Food> ();
					Food temp = (Food)itemDatabase[i];
					newItem.ConfigureItem (temp.itemName, temp.itemId, temp.itemDesc, temp.itemWeight, temp.itemType, temp.itemSprite, temp.itemHeal);
					return newItem;
				}
				else if(itemDatabase[i].itemType == Item.ItemType.KeyItem)
				{
					KeyItem newItem = (KeyItem)ScriptableObject.CreateInstance<KeyItem> ();
					KeyItem temp = (KeyItem)itemDatabase[i];
					newItem.ConfigureItem (temp.itemName, temp.itemId, temp.itemDesc, temp.itemWeight, temp.itemType, temp.itemSprite);
					return newItem;
				}
			}
		}
		return null;
	}

			
}
