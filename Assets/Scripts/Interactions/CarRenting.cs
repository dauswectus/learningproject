using UnityEngine;
using System.Collections;

public class CarRenting : MonoBehaviour {

	public GameObject rentableCar;
	public GameObject spawnPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Rent()
	{
		Instantiate (rentableCar, spawnPoint.transform.position, spawnPoint.transform.rotation);
	}
}
