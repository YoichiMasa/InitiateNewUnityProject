using UnityEngine;
using System.Collections;


[System.Serializable]
public class Food: Item
{
	public static readonly int FOOD_ID = 200;
	public int numFood = -1;
	public int itemHeal = 10;
	
	public void ConfigureItem(string name, int ID, string description, int baseWeight, ItemType type, GameObject sprite, int heal)
	{
		base.ConfigureItem (name, ID, description, baseWeight, type, sprite);
		itemName = name;
		itemId = ID;
		itemDesc = description;
		itemWeight = baseWeight;
		itemSprite = sprite;
		itemHeal = heal;

	}

	public override void useItem()
	{}
}
