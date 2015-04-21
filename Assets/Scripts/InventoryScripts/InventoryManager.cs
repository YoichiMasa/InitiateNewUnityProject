using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	public 	List<Item> inventory;
	public  InventoryManager invent;
	public  Inventory spawn;
	public  MoveItem move;
	public GameObject equipPoint;
	private GameObject equipPhysItem = null;

	public  int inventIndex = 0;
	public  Item selectedItem = null;
	public Item equippedItem = null;

	public  float invWeight = 0;
	public  float MAX_WEIGHT = 30f;

	public Camera inventCam;
	public bool visible;
	public  bool select = false;

	void Awake()
	{
		inventCam = GameObject.Find("Inventory").GetComponent<Camera> ();
		equipPoint = GameObject.Find ("EquipPoint");
		if (invent == null) 
		{
			DontDestroyOnLoad(gameObject);
			inventory = new List<Item>();	
			invent = this;
		}
		else
		{
			if( invent != this)
			{
				Destroy(gameObject);
			}
		}
	}

	void Update()
	{
		if (inventCam.enabled == true) {
			visible = true;
		} else {
			visible = false;
		}

		if(visible == true)
		{
			if(inventory.Count > 0)
			{
				if(Input.GetKeyDown(KeyCode.Space))
				{
					selectItem();
					AudioController.instance.PlayItemSound(4);
				}
				
				if(Input.GetKeyDown(KeyCode.KeypadEnter)&& selectedItem != null)
				{
					removeItem();
				}
				
				if(Input.GetKeyDown (KeyCode.D) && selectedItem != null)
				{
					dropItem();
				}
				if(Input.GetKeyDown (KeyCode.V) && selectedItem != null)
				{
					if(selectedItem.itemType == Item.ItemType.Food)
					{	
						selectedItem.useItem();
						float leftoverHeal = selectedItem.getValue();
						if(leftoverHeal == 0)
						{
							removeItem();
						}
					}
					if(selectedItem.itemType == Item.ItemType.Weapon)
					{
						if(equipPhysItem != null)
						{
							Destroy (equipPhysItem);
						}
						equipPhysItem = selectedItem.equipItem(equipPoint, selectedItem);
						equippedItem = selectedItem;
					}
				}

			}
		}
		else
		{
			if(/*inventory.Count > 0 &&*/ selectedItem != null)
			{
				selectedItem.isSelected = select;
			}
		}

	}

	public  void addItem(Item item)
	{
		Item temp = (Item)ScriptableObject.CreateInstance<Item> ();
		temp = item;
		if (temp.itemType == Item.ItemType.Weapon) 
		{
			temp.itemSprite = spawn.addPrefab(temp);
			inventory.Add(temp);
			invWeight = invWeight+temp.itemWeight;
		}
		else if (temp.itemType == Item.ItemType.Food) 
		{
			temp.itemSprite = spawn.addPrefab(temp);
			inventory.Add(temp);
			invWeight = invWeight+temp.itemWeight;
		}
		else if (temp.itemType == Item.ItemType.KeyItem) 
		{
			temp.itemSprite = spawn.addPrefab(temp);
			inventory.Add(temp);
			invWeight = invWeight+temp.itemWeight;
		}
		HUDController.instance.Message ("Obtained: " + temp.itemName + "", 4);
	}

	public  void removeItem()
	{
		inventory.RemoveAt(inventIndex);
		invWeight = invWeight-selectedItem.itemWeight;
		Destroy(selectedItem.itemSprite);
		selectedItem = null;
		selectItem ();
		HUDController.instance.HandleWeight ();
	}

	public void dropItem()
	{
		GameObject dropCurrItem = selectedItem.itemSprite.GetComponent<MoveItem> ().dropItem;
		Transform itemDropPoint = selectedItem.itemSprite.GetComponent<MoveItem> ().itemDropPoint;
		inventory.RemoveAt(inventIndex);
		invWeight = invWeight-selectedItem.itemWeight;
		Instantiate (dropCurrItem, itemDropPoint.position, Quaternion.identity);
		Destroy(selectedItem.itemSprite);
		selectedItem = null;
		selectItem ();
		HUDController.instance.HandleWeight ();
		AudioController.instance.PlayItemSound (2);
	}

	public  bool checkWeight(Item newItem)
	{
		Item temp = newItem;
		bool full = false;
		if (temp.itemWeight + invWeight > MAX_WEIGHT) 
		{
			full = true;
			HUDController.instance.Message("Bag is too heavy to pick up.", 4);
			return full;
		}
		else
		{
			return full;
		}
	}

	public  void selectItem()
	{
		if(selectedItem != null /*&& selectedItem.itemSprite != null*/)
		{
			selectedItem.isSelected = select;
			selectedItem.itemSprite.GetComponent<MoveItem>().swapSprite();
		}
		if ((inventIndex+1) < inventory.Count) 
		{
			inventIndex++;
			selectedItem = inventory[inventIndex];
			selectedItem.itemSprite.GetComponent<MoveItem>().swapSprite();
			Debug.Log (selectedItem.itemName+" "+inventIndex);
			selectedItem.isSelected = !select;
		}
		else if(inventory.Count > 0)
		{
			inventIndex = 0;
			selectedItem = inventory[inventIndex];
			selectedItem.itemSprite.GetComponent<MoveItem>().swapSprite();
			selectedItem.isSelected = !select;
		}
		else
		{
			selectedItem =  null;
		}
	}
}
