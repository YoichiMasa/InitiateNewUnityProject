using UnityEngine;
using System.Collections;

[System.Serializable]
public class KeyItem: Item
{
	public static readonly int KEYITEM_ID = 300;

	public override void ConfigureItem(string name, int ID, string description, int baseWeight, ItemType type, GameObject sprite)
	{
		base.ConfigureItem (name, ID, description, baseWeight, type, sprite);
		itemName = name;
		itemId = ID;
		itemDesc = description;
		itemWeight = baseWeight;
		itemSprite = sprite;
	}

	public override void useItem()
	{}
}
