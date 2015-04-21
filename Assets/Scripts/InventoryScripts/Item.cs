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
	public GameObject itemSprite;
	public bool isSelected;

	public virtual void ConfigureItem(string name, int ID, string description, int baseWeight, ItemType type, GameObject sprite)
	{
		itemName = name;
		itemId = ID;
		itemDesc = description;
		itemWeight = baseWeight;
		itemType = type;
		itemSprite = sprite;
	}

	public virtual void useItem(){}

	public virtual float getValue()
	{
		return 0;
	}

	public virtual GameObject equipItem(GameObject equipPoint, Item equipItem)
	{
		return null;
	}
}





