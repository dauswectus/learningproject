using UnityEngine;
using System.Collections;

public class ItemContainerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider collider)
	{
		Transform transformSave;
		transformSave = collider.gameObject.transform;
		if (collider.gameObject.GetComponent<InteractableItemScript> ().itemType == "Pickup") {
			collider.attachedRigidbody.isKinematic = true;
			collider.transform.localPosition = transform.position;
		}
	}
}
