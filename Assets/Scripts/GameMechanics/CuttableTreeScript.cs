using UnityEngine;
using System.Collections;

public class CuttableTreeScript : MonoBehaviour {

	public int health;
	public GameObject Package;
	public int packageCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health == 0) {
			/*
			StartCoroutine (SpawnObjects (Package, 0.5f, packageCount));
			Destroy (this.gameObject);*/
			GetComponent<Rigidbody> ().freezeRotation = false;
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			GetComponent<Rigidbody> ().AddForce (Vector3.left *  10000.0f);
		}

	}
	IEnumerator SpawnObjects(GameObject obj, float sec, int packageCount)
	{
		for (int i = 0; i < packageCount; i++)
		{
			yield return new WaitForSeconds (sec);
			GameObject cargo = (GameObject)Instantiate (Package, transform.position, transform.rotation);
			cargo.name = "Tree Log";
		}
	}
	public void OnTriggerEnter(Collider collider)
	{
		StartCoroutine (SpawnLogs(0.1f,collider)); // animációval jobb lenne :(
	}
	IEnumerator SpawnLogs(float secs, Collider collider)
	{
		yield return new WaitForSeconds (secs);
		Vector3 myPos = new Vector3(transform.localPosition.x-5, transform.localPosition.y,transform.localPosition.z);
		if (collider.name == "Terrain") {
			for (int i = 0; i < 8; i++) {
				myPos.x += 1.1f;
				GameObject cargo = (GameObject)Instantiate (Package, myPos, transform.rotation);
				cargo.name = "Tree Log";
			}
			Destroy (this.gameObject);
		}
	}
}
