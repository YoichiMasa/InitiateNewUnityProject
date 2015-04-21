using UnityEngine;
using System.Collections;


[System.Serializable]
public class Food: Item
{
	public static readonly int FOOD_ID = 200;
	public int numFood = -1;
	public float itemHeal = 10;
	
	public void ConfigureItem(string name, int ID, string description, int baseWeight, ItemType type, GameObject sprite, float heal)
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
	{
		Debug.Log (itemHeal);
		//dans code
		float remainder = HUDController.instance.maxStamina - HUDController.instance.Stamina;

		float tempHeal = HUDController.instance.Stamina + (float)itemHeal;
		if(HUDController.instance.Stamina != HUDController.instance.maxStamina)
		{
			if(tempHeal > HUDController.instance.maxStamina)
			{
				HUDController.instance.Message(""+ remainder.ToString(".00") + " Calories Consumed", 4);
				itemHeal = tempHeal - HUDController.instance.maxStamina;
				HUDController.instance.Stamina = HUDController.instance.maxStamina;
			}
			else
			{
				HUDController.instance.Message(""+ itemHeal.ToString(".00") + " Calories Consumed", 4);
				HUDController.instance.Stamina = tempHeal;
				itemHeal = 0;
			}
		}
	}

	public override float getValue ()
	{
		return itemHeal;	
	}
}
