using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
public class PickUp: MonoBehaviour {

	public int itemID;
	public float range;
	private SphereCollider trigger;
	public bool canPickUp;
	public Item item;

	void Awake()
	{
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
		}
	}

	void OnTriggerExit(Collider col)
	{
		canPickUp = false;
	}

	void Update()
	{
		if (canPickUp) 
		{
			if (Input.GetKeyDown (KeyCode.F)) 
			{
				InventoryManager.addItem (item);
				Destroy (gameObject);
			}
		}
	}
}
