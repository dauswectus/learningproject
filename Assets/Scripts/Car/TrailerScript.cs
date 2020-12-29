using UnityEngine;
using System.Collections;

public class TrailerScript : MonoBehaviour {

	public GameObject centerOfMass;
	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.centerOfMass = centerOfMass.transform.position;
	}
}
