using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : ScriptableObject {

	public enum ItemType
	{
		Food,
		Weapon,
		KeyItem
	}
	public string itemName = "";
	public int itemId = 0;
	public string itemDesc = "";
	public int itemWeight = 0;
	public ItemType itemType;

	public Item()
	{}

	public void ConfigureItem(string name, int ID, string description, int baseWeight, int height, int width, ItemType type)
	{
		itemName = name;
		itemId = ID;
		itemDesc = description;
		itemWeight = baseWeight;
		itemType = type;
	}
}





