using UnityEngine;
using System.Collections;

public class IgnoreCollision : MonoBehaviour {

	public Collider IgnorableCollider;
	public Collider BodyCollider;
	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision (IgnorableCollider, BodyCollider);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
