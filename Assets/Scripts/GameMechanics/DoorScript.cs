using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public bool isItOpen;
	public GameObject Door;

	// Use this for initialization
	void Start () {
		isItOpen = true;
	}
	
	// Update is called once per frame
	void Update () {
	 
	}

	public void UseItem()
	{
		if (isItOpen)
		{
			Door.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			isItOpen = false;
		}
		else
		{
			Door.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			isItOpen = true;
		}
	}
}
