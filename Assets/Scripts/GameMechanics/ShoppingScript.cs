using UnityEngine;
using System.Collections;

public class ShoppingScript : MonoBehaviour {

	public GameObject buyableItem;
	public GameObject spawnPoint;

	// Use this for initialization
	void Start () {
		gameObject.name = "Buy \"" + buyableItem.name + "\" for $" + buyableItem.GetComponent<InteractableItemScript> ().moneyAmount; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject BuyItem()
	{
		GameObject shoppedItem = (GameObject)Instantiate (buyableItem, spawnPoint.transform.position, spawnPoint.transform.rotation);
		shoppedItem.name = buyableItem.name;
		return shoppedItem;
	}
}
