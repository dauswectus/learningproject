using UnityEngine;
using System.Collections;

public class BouncePlayer : MonoBehaviour {

	private CharacterController controller;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("The " + transform.gameObject.tag + " bounced " + collision.gameObject.tag);
		collision.gameObject.GetComponent<CharacterController> ().Move(transform.GetComponent<Rigidbody>().velocity);

	}*/
}
