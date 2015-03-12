using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon: Item
{
	public static readonly int WEAPON_ID = 100;
	public int wpnDamage = 10;
	public int wpnDurability = 10;

	public void ConfigureWeapon(string name, int ID, string description, int baseWeight, int height, int width, ItemType type, int damage,
	              	int durability)
	{
		base.ConfigureItem (name, ID, description, baseWeight, height, width, type);
		itemName = name;
		itemId = ID;
		itemDesc = description;
		itemWeight = baseWeight;
		wpnDamage = damage;
		wpnDurability = durability;
	}
}
