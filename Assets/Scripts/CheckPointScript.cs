using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {

	public string rewardType;
	public int moneyReward;
	public bool despawn;

	private int packageCount;
	private int requiredPackageCount;
	private Transform checkPointGraphics;
	private GameObject package;
	private GameObject Player;
	private GameObject[] packages = new GameObject[10];
	public GameObject moneyPref;

	// Use this for initialization
	void Start () {
		checkPointGraphics = transform.GetChild (0);
		despawn = false;
	}
	
	// Update is called once per frame
	void Update () {
		checkPointGraphics.transform.Rotate (Vector3.up,20*Time.deltaTime);
		if (packageCount == requiredPackageCount)
		{
			GameObject Money = (GameObject)Instantiate(moneyPref, transform.position, transform.rotation);
			Money.name = "Salary: u" + moneyReward;
			Money.GetComponent<InteractableItemScript> ().moneyAmount = moneyReward;
			for (int i = 0; i < 10; i++)
			{
				if (packages [i] != null)
				{
					Destroy (packages [i]);
				}
			}
			Destroy (gameObject);

		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == "Package" || collider.gameObject.name == "Planks")
		{
			packages [packageCount] = collider.gameObject;	
			collider.gameObject.name = "Package_Carried";
			packageCount++;
			Debug.Log (packageCount);
		}
	}
	public void Job(GameObject package, int moneyReward, GameObject Player, int packageCount)
	{
		this.requiredPackageCount = packageCount;
		this.Player = Player;
		this.moneyReward = moneyReward;
		this.package = package;
	}
}
