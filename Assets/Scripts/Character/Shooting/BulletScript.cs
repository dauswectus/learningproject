using UnityEngine;
using System.Collections;

public class BulletScript: MonoBehaviour
{

	public float bulletSpeed = 500.0f; //frame per sec
	public float range; //frame
	private float existTime; //sec

	

	// Use this for initialization
	void Start ()
	{
		//Debug.Log("Örökölt távolság: "+range);
		existTime = range / bulletSpeed;
	}
		// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.up * bulletSpeed * Time.deltaTime);
		
		if (existTime > 0) {
			existTime -= Time.deltaTime;
			//Debug.Log(existTime);
			}
		if (existTime <= 0) {
				Destroy (gameObject);
		}
	}
}

