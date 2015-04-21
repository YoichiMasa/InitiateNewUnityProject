using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
public class PickUp: MonoBehaviour {

	public int itemID;
	public float range;
	private SphereCollider trigger;
	public bool canPickUp;
	public Item item;
	public InventoryManager invent;

	void Awake()
	{
		invent = GameObject.FindGameObjectWithTag ("GameController").GetComponent<InventoryManager> ();
		trigger = GetComponent<SphereCollider> ();
		trigger.isTrigger = true;
		trigger.radius = range;
		canPickUp = false;
		item = (Item)ScriptableObject.CreateInstance<Item> ();
		item = ItemManager.getItem (itemID);
	}

	void OnTriggerStay(Collider col)
	{
		if(col.tag == "Player")
		{
			canPickUp = true;
			HUDController.instance.MessageStay("Press [F] to Pick Up");
		}
	}

	void OnTriggerExit(Collider col)
	{
		canPickUp = false;
		HUDController.instance.MessageStay ("");
	}

	void Update()
	{
		if (canPickUp) 
		{
			if (Input.GetKeyDown (KeyCode.F) && !invent.checkWeight(item)) 
			{
				invent.addItem (item);
				Destroy (gameObject);
				AudioController.instance.PlayItemSound(2);
			}
		}
	}
}
