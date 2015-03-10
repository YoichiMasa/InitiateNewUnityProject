using UnityEngine;
using System.Collections;

public interface IInventory
{

	void addItem(Item item);

	void removeItem(Item item);

	void useItem(Item item);

	void checkWeight(Item item);

	
}
