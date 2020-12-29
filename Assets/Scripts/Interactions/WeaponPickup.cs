using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {
	private Transform myTransform;
	public GameObject inThisHand;
	public Vector3 backPocket;
	public GameObject BackPocket;
	public GameObject SidePocket;

	public bool isInPocket = false;
	private BoxCollider myCollider;


	public GameObject WeaponTube;
	public Vector3 tubePos;
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (inThisHand != null) 
		{
			if (inThisHand.GetComponent<Shooting> ().haveATarget == true)
			{
				myTransform.position = inThisHand.transform.position;
				myTransform.rotation = inThisHand.transform.rotation;
				myTransform.LookAt (inThisHand.GetComponent<Shooting> ().weaponTarget);
			}
			else 
			{
				myTransform.position = inThisHand.transform.position;
				myTransform.rotation = inThisHand.transform.rotation;
			}
		}
		if (isInPocket == true && BackPocket != null)
		{
			myTransform.position = BackPocket.transform.position;
			myTransform.rotation = BackPocket.transform.rotation;
		}
		else if(isInPocket == true && SidePocket != null)
		{
			myTransform.position = SidePocket.transform.position;
			myTransform.rotation = SidePocket.transform.rotation;
		}
	}
}
