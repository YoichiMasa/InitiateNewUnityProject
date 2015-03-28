using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ItemManager: MonoBehaviour{
	public static IList<Item> itemDatabase;
	public static ItemManager im;
	public ItemReader reader;
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
		reader = new ItemReader ();
		itemDatabase = reader.read ();
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

public class ItemReader
{
	public ItemReader()
	{
		
	}
	public IList<Item> read()
	{
		TextAsset csvFile = (TextAsset)Resources.Load("ItemList");
		IList<Item> items = new List<Item> ();
		string readText = csvFile.text;
		string[] readLine = readText.Split ("\n"[0]);
		for (int i = 0; i < readLine.Length-1; i++) 
		{
			string[] line = readLine [i].Split ("," [0]);

			int ID = int.Parse (line[1]);
			if(ID >= 100 && ID < 200)
			{
				Weapon newWpn = (Weapon)ScriptableObject.CreateInstance<Weapon>();
				newWpn.ConfigureItem(line[0], ID, line[2], int.Parse(line[3]), Item.ItemType.Weapon, null, int.Parse (line[4]), int.Parse (line[5]));
				items.Add(newWpn);
			}
			else if(ID >= 200 && ID < 300)
			{
				Food newFood = (Food)ScriptableObject.CreateInstance<Food>();
				newFood.ConfigureItem(line[0], ID, line[2], int.Parse(line[3]), Item.ItemType.Weapon, null, int.Parse (line[4]));
				items.Add(newFood);
			}
			else if(ID >= 300)
			{
				KeyItem newKey = (KeyItem)ScriptableObject.CreateInstance<KeyItem>();
				newKey.ConfigureItem(line[0], ID, line[2], int.Parse(line[3]), Item.ItemType.Weapon, null);
				items.Add(newKey);
			}
		}
		return items;
	}
	
}
