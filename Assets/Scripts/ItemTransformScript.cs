using UnityEngine;
using System.Collections;

public class ItemTransformScript : MonoBehaviour {

	public GameObject itemCreateFrom;
	public GameObject itemToCreate;
	public string nameOfCreatedItem;
	public int moneyAmount;
	public GameObject spawnPoint;
	public int quantity;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter (Collider collider){
		if (itemCreateFrom.name == collider.gameObject.name)
		{
			for (int i = 0; i < quantity; i++) {
				transformIntoPlank (collider.gameObject);
			}
		}
		else
		{
			Destroy (collider.gameObject);
		}
	}

	public void transformIntoPlank(GameObject transformableObject)
	{
		Destroy (transformableObject);
		GameObject clone;
		if (spawnPoint == null) 
		{
			clone = (GameObject)Instantiate (itemToCreate, transform.position, transform.rotation);
		}
		else 
		{
			clone = (GameObject)Instantiate (itemToCreate, spawnPoint.transform.position, spawnPoint.transform.rotation);
		}
		clone.name = nameOfCreatedItem;
		if (clone.GetComponent<InteractableItemScript> () != null && clone.GetComponent<InteractableItemScript> ().itemType == "Money") {
			clone.GetComponent<InteractableItemScript> ().moneyAmount = moneyAmount;
			clone.name = "Money: u" + moneyAmount;
		}
	}
}
