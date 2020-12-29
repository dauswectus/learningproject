using UnityEngine;
using System.Collections;

public class WeaponSpawnerScript: MonoBehaviour {

    public bool useable;
    public GameObject ItemSpawn;
    public Transform spawnPoint;
	public string spawnedItemName;
	public string spawnedItemType;

	// Use this for initialization
	void Start () {
        useable = true;
    }

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.K))
		{
			UseItem (ItemSpawn);
		}
	}

	public void UseItem(GameObject itemSpawn)
    {
        if (useable)
        {
			GameObject SpawnedItem = (GameObject)Instantiate(itemSpawn, spawnPoint.position, spawnPoint.rotation);
			SpawnedItem.name = ""+spawnedItemName;
			SpawnedItem.GetComponent<InteractableItemScript> ().itemType = ""+spawnedItemType;
        }
    }
}
