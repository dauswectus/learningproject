using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkAssignScript : MonoBehaviour {
	[Header("")]
	[Tooltip("The object you have to bring to complete the work.")]
	public GameObject Package;
	[Tooltip("The amount of money you got for completing the work.")]
	public int moneyReward = 0;
	[Tooltip("The amount of packages you have to bring to complete the work.")]
	public int packageCount = 1;

	public Transform packageSpawnPoint;
	public GameObject CheckPointPref;
	public Transform destinationPont;

	// Update is called once per frame
	void Update () {
	
	}

	public GameObject Work (string workType,GameObject Player)
	{
		string workDesc;
		GameObject DestCheckPoint;

		if (workType == "CargoCarry")
		{
			workDesc = "Carry the cargo to the destination! Place the boxes into a trailer and deliver it.";
			moneyReward = 30;
			DestCheckPoint = (GameObject)Instantiate (CheckPointPref, destinationPont.position, destinationPont.rotation);
			DestCheckPoint.GetComponent<CheckPointScript> ().Job (Package, moneyReward, Player, packageCount);
			StartCoroutine (SpawnObjects (Package, 0.5f, packageCount));
			return DestCheckPoint;
		} 
		else if (workType == "TreeCut")
		{
			workDesc = "Carry the cargo to the destination! Place the boxes into a trailer and deliver it.";
			moneyReward = 50;
			DestCheckPoint = (GameObject)Instantiate (CheckPointPref, destinationPont.position, destinationPont.rotation);
			DestCheckPoint.GetComponent<CheckPointScript> ().Job (Package, moneyReward, Player, packageCount);
			//StartCoroutine (SpawnObjects (Package, 0.5f, packageCount));
			return DestCheckPoint;
		} 
		else
		{
			return null;
		}
	}
	IEnumerator SpawnObjects(GameObject obj, float sec, int packageCount)
	{
		for (int i = 0; i < packageCount; i++)
		{
			yield return new WaitForSeconds (sec);
			GameObject cargo = (GameObject)Instantiate (Package, packageSpawnPoint.position, packageSpawnPoint.rotation);
			cargo.name = "Package";
		}
	}
}
