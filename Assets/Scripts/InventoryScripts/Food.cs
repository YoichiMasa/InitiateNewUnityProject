using UnityEngine;
using System.Collections;


[System.Serializable]
public class Food: Item
{
	public static readonly int FOOD_ID = 200;
	public int numFood = -1;
	public int itemHeal = 10;
	
	public void ConfigureFood(string name, int ID, string description, int baseWeight, int height, int width, ItemType type, int heal)
	{
		base.ConfigureItem (name, ID, description, baseWeight, height, width, type);
		itemName = name;
		itemId = ID;
		itemDesc = description;
		itemWeight = baseWeight;
		itemHeal = heal;
	}

}
