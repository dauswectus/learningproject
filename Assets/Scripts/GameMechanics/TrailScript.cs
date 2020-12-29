using UnityEngine;
using System.Collections;

public class TrailScript : MonoBehaviour {
	public float speed = 0.1f;
	public bool trailIsActive;
	public bool alwaysActive;
	void OnTriggerStay(Collider collider)
	{
		if (trailIsActive) {
			collider.transform.position += transform.right * speed * Time.deltaTime;
		}
	}

	// Use this for initialization
	void Start () {
		trailIsActive = false;
		if (alwaysActive) {
			trailIsActive = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
			
	}
}
