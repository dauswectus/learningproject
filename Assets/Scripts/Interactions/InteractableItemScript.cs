using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractableItemScript : MonoBehaviour {

	[Header("Item Main Properties")]
	[Tooltip("Milyen funkciót töltsön be a tárgy")]
	public string itemType;
	[Tooltip("Mit csináljon ha használjuk a tárgyat")]
	public string itemUsage;
	[Tooltip("Munka típusa")]
	public string workType;
	[Tooltip("Mennyibe kerüljön a tárgy, vagy ha pénz, akkor mekkora legyen az összeg")]
	public int moneyAmount;

	[Header("Settings")]
	private GameObject hand;
	public GameObject twoHand;
	private GameObject localPlayerObj;
	private Interact localInteract;
	private Backpack backPack;
	private bool isInHand;
	private bool isItOn;
	private GameObject checkPoint = null;
	public GameObject UiObject;
	private Vector3 myScales;

	// Use this for initialization
	void Start () {
		myScales = transform.localScale;
	}

	public int UseItem(Interact interAct, GameObject Player)
	{
		localInteract = interAct;
		hand = interAct.gameObject;
		if (isInHand == false && hand.GetComponent<Interact> ().isHandFull == false) {
			switch (itemType) {
			case "Pickup":
				ItemPickup ();
				isInHand = true;
				hand.GetComponent<Interact> ().isHandFull = true;
				return 0;
			case "Button":
				LampSwitch (hand);
				return 0;
			case "Money":
				return  Money ();
			case "WorkBoard":
				Work (Player);
				return 0;
			case "Shop":
				return -Shopping (Player);
			case "Rent":
				return -RentCar ();
			default : 
				return 0;
			}
		}
		else 
		{
			Debug.Log ("Tele a kezed");
			UseObject (Player);
			return 0;
		}
	}

	public void UseObject(GameObject Player)
	{
		switch (itemUsage) 
		{
		case "Consume": 
			EatItem (Player);
			hand.GetComponent<Interact> ().isHandFull = false;
			isInHand = false;
			break;
		case "Cut":
			AxeUsage(hand.GetComponent<Interact>().LookingAtThisObject);
			break;
		default:
 			break;
		}
	}


	void Update()
	{
		if (isInHand == true)
		{
			if (itemUsage == "Cut") 
			{
				transform.position = hand.transform.position;
				transform.rotation = hand.transform.rotation;
			}
			else 
			{
				transform.position = twoHand.transform.position;
				transform.rotation = twoHand.transform.rotation;
			}
			Component[] colliders;
			colliders = GetComponentsInChildren<Collider> ();
			foreach(Collider collider in colliders)
			{
				collider.enabled = false;
			}

			GetComponent<Collider> ().enabled = false;
			GetComponent<Rigidbody> ().useGravity = false;
			localInteract.isHandFull = true;
			if (Input.GetButtonDown ("Drop Weapon"))
			{
				foreach(Collider collider in colliders)
				{
					collider.enabled = true;
				}
				GetComponent<Collider> ().enabled = true;
				GetComponent<Rigidbody> ().useGravity = true;
				localInteract.isHandFull = false;
				isInHand = false;
			}
			if (Input.GetButtonDown ("No Weapon"))
			{
				HandToInventory ();
			}
		} 
	}

	public int Shopping(GameObject player)
	{
		if (player.GetComponent<PlayerStatsScript> ().Money >= GetComponent<ShoppingScript> ().buyableItem.GetComponent<InteractableItemScript> ().moneyAmount) {
			GameObject boughtItem = GetComponent<ShoppingScript> ().BuyItem ();
			boughtItem.GetComponent<InteractableItemScript> ().itemType = "Pickup";
			return boughtItem.GetComponent<InteractableItemScript> ().moneyAmount;
			//boughtItem.GetComponent<InteractableItemScript>().ItemPickup (boughtItem, hand, twoHand);
			//boughtItem.GetComponent<InteractableItemScript>().HandToInventory ();
		} 
		else 
		{
			return 0;
		}
	}
	public void HandToInventory()
	{
		if (UiObject != null) {
			Transform handUI = null;
			int inventorySize = hand.gameObject.GetComponent<Interact> ().canvas.transform.Find ("ActionBar").childCount ;
			bool canPlace = false;
			for (int i = 0; i < inventorySize; i++) 
			{
				handUI = hand.gameObject.GetComponent<Interact> ().canvas.transform.Find ("ActionBar").Find ("ActionSlot" + i);
				if (handUI.childCount == 0) 
				{
					canPlace = true;
					break;
				}
				else
				{
					canPlace = false;
				}
			}
			if (canPlace) 
			{
				GameObject inventorUiItem = (GameObject)Instantiate (UiObject, transform.position, transform.rotation);
				gameObject.transform.SetParent (inventorUiItem.transform);
				gameObject.SetActive (false);
				inventorUiItem.transform.SetParent (handUI);
				inventorUiItem.AddComponent<DragHandeler> ();
				inventorUiItem.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
				inventorUiItem.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (0, 0, 0);
				localInteract.isHandFull = false;
				isInHand = false;
			}
		}
	}

	public bool InventoryToHand()
	{
		//kézbe milyen item legyen
		localInteract.activeObject = this.gameObject;
		if (localInteract.isHandFull == false) {
			gameObject.SetActive (true);
			transform.SetParent (null);
			gameObject.transform.localScale = myScales;
			localInteract.isHandFull = true;
			isInHand = true;
			return true;
		}
		return false;
	}

	public void Work(GameObject Player)
	{
		if (checkPoint == null)
		{
			checkPoint = GetComponent<WorkAssignScript> ().Work (workType, Player);
		}
	}
		
	public void LampSwitch(GameObject buttonSwitch)
	{
		this.GetComponent<LightSwitchScript> ().UseItem ();
	}
	public void ItemPickup()
	{

		isInHand = true;
	}
	public void ItemPickup(GameObject boughtItem, GameObject thisHand, GameObject twoHand)
	{
		if (isInHand == false && thisHand.GetComponent<Interact>().isHandFull == false)
		{
			isInHand = true;
		}
	}
	public int Money()
	{
		hand.GetComponent<Interact> ().isHandFull = false;
		isInHand = false;
		Destroy (gameObject);
		return moneyAmount;
	}
	public int EatItem(GameObject player)
	{
		player.GetComponent<PlayerStatsScript> ().hunger += 20;
		Debug.Log ("Item használva!");
		Destroy (gameObject);
		return 0;
	}
	public void AxeUsage(GameObject lookingAtThisObject)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
		int distance = (int)Vector3.Distance(Camera.main.transform.position, transform.position);
		if (Physics.Raycast (ray.origin, ray.direction, out hit, 2 + distance)) {
			Debug.DrawLine(transform.position, hit.point, Color.red);
			Debug.Log (lookingAtThisObject);
			if (lookingAtThisObject.tag == "CuttableTree")
			{
				lookingAtThisObject.GetComponent<CuttableTreeScript> ().health -= 1;
			}
		}
	}
	public int RentCar()
	{
		gameObject.GetComponent<CarRenting> ().Rent();
		return moneyAmount;
	}
}
