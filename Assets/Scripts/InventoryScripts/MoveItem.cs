using UnityEngine;
using System.Collections;

public class MoveItem : MonoBehaviour {

	public float moveSpeed;
	public SpriteRenderer loader;
	public Sprite currSprite;
	public Sprite tempSprite;
	private Sprite holdSprite;
	public Rigidbody2D item;
	public InventoryManager invent;

	void Awake ()
	{
		invent = GameObject.FindGameObjectWithTag ("GameController").GetComponent<InventoryManager> ();

		loader = this.GetComponent<SpriteRenderer> ();
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
					Debug.Log("Hit");
					item.gravityScale = 0;
					Vector3 moveVector = new Vector3(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0);
					Vector3 desiredPosition = transform.position + moveVector;
					if (desiredPosition.x > -2.3f &&desiredPosition.x < 2.5f)
					{
						transform.position = desiredPosition;
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
