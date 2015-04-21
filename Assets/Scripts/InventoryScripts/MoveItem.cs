using UnityEngine;
using System.Collections;

public class MoveItem : MonoBehaviour {

	public float moveSpeed;
	public SpriteRenderer loader;
	public Sprite currSprite;
	public Sprite tempSprite;
	private Sprite holdSprite;
	public Rigidbody2D item;
	public GameObject dropItem;
	public InventoryManager invent;
	public Transform itemDropPoint;

	void Awake ()
	{
		invent = GameObject.FindGameObjectWithTag ("GameController").GetComponent<InventoryManager> ();

		loader = this.GetComponent<SpriteRenderer> ();
		itemDropPoint = GameObject.Find ("DropItemPoint").GetComponent<Transform> ();
	}
	// Update is called once per frame
	void Update () 
	{
		if(invent.selectedItem != null && invent.selectedItem.itemSprite != null)
		{	
			if(invent.selectedItem.isSelected == true)
			{
				//loader.sprite = tempSprite;
				if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
				{
					item.gravityScale = 0;
					Vector3 moveVector = new Vector3(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0);
					Vector3 desiredPosition = invent.selectedItem.itemSprite.transform.position + moveVector;
					Debug.Log (desiredPosition);
					if (desiredPosition.x > 38.5f && desiredPosition.x < 43.1f && desiredPosition.y > -2.6f)
					{
						invent.selectedItem.itemSprite.transform.position = desiredPosition;
					}
				}
				else
				{
					item.gravityScale = 1;
				}
			}
			else
			{
				//loader.sprite = currSprite;
			}
		}
	}

	public void swapSprite()
	{
		holdSprite = loader.sprite;
		loader.sprite = tempSprite;
		tempSprite = holdSprite;
	}
}
