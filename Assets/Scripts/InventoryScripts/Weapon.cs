using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon: Item
{
	public static readonly int WEAPON_ID = 100;
	public int wpnDamage = 10;
	public int wpnDurability = 10;

	public void ConfigureItem(string name, int ID, string description, int baseWeight, ItemType type, GameObject sprite,
	                            int damage,	int durability)
	{
		base.ConfigureItem (name, ID, description, baseWeight, type, sprite);
		itemName = name;
		itemId = ID;
		itemDesc = description;
		itemWeight = baseWeight;
		itemSprite = sprite;
		wpnDamage = damage;
		wpnDurability = durability;
	}

	public override void useItem()
	{}

	public override GameObject equipItem(GameObject equipPoint, Item equipItem)
	{
		GameObject dropCurrItem = equipItem.itemSprite.GetComponent<MoveItem> ().dropItem;
		Quaternion corrRotation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localRotation;
		GameObject equip = (GameObject)Instantiate (dropCurrItem, equipPoint.transform.position, Quaternion.identity);
		equip.transform.localRotation = corrRotation;
		equip.transform.parent = equipPoint.transform;
		return equip;
	}
}
