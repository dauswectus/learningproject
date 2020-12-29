using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {

	//Headerezni!!!!!!!!

    private RaycastHit hit;
    private Transform myTransfrom;
    private bool isMainWeapon;
    private bool isSideWeapon;
	[Header("Is there something in the hand or rotated?")]
    public bool isHandFull;
	public int isTwoHandRotated;
	[Header("Is the player in car?")]
	public bool isInCar;
	[Header("GameObjects")]
	public GameObject twoHand;
	public GameObject Player;
	private GameObject Weapon;
	private GameObject WeaponTube;
	public GameObject LookingAtThisObject;
	public GameObject BackPocket;
	public GameObject SidePocket;
	public GameObject activeObject;
	public GameObject actionBar;
	public GameObject canvas;
	[Header("Range of reaching items")]
	public float range;
	[Header("Array for WeaponPockets")]
	public GameObject[,] WeaponSlots = new GameObject[3,2];
	[Header("Player Graphics")]
	public GameObject grafika;

	// Use this for initialization
	void Start () {
			
		myTransfrom = transform;
		isHandFull = false;
		isInCar = false;
		range = 3;
		isTwoHandRotated = 1;
	}
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
        int distance = (int)Vector3.Distance(Camera.main.transform.position, myTransfrom.position);

		#region Rotate Object In Hand
		if (Input.GetKeyDown (KeyCode.R)) {
			isTwoHandRotated++;
			if (isTwoHandRotated > 4) {
				isTwoHandRotated = 1;
			}
			if (isTwoHandRotated == 1)
			{
				twoHand.transform.localRotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
			}
			else if (isTwoHandRotated == 2)
			{
				twoHand.transform.localRotation = Quaternion.Euler (0.0f, 0.0f, 90.0f);
			}
			else if (isTwoHandRotated == 3)
			{
				twoHand.transform.localRotation = Quaternion.Euler (0.0f, -90.0f, 90.0f);
			}
			else
			{
				twoHand.transform.localRotation = Quaternion.Euler (0.0f, -90.0f, 0.0f);
			}
		}
		#endregion

        if (!isInCar)
        {
			if (Physics.Raycast(ray.origin, ray.direction, out hit, range + distance))
            {
                LookingAtThisObject = hit.collider.gameObject;
				if (Input.GetButtonDown ("Use") && isHandFull == true) 
				{
					activeObject.GetComponent<InteractableItemScript> ().UseObject(Player);
				}
				if (Input.GetButtonDown("Use") && isHandFull == false)
                {
                    PickUpWeapon();
					UseItem ();
                    GetInTheCar();
                }

				// if(Input.GetButton("Use"))...
                #region Debug
                Debug.DrawLine(myTransfrom.position, hit.point, Color.cyan);
                #endregion
            }
            else
            {
				if (Input.GetButtonDown ("Use") && isHandFull == true) 
				{
					activeObject.GetComponent<InteractableItemScript> ().UseObject(Player);
				}
				//actionBar.SetActive (false);
                LookingAtThisObject = null;
            }
            DropWeapon();
        }
        else
        {
            if (Input.GetButtonDown("Use"))
            {
                GetOutFromTheCar();
            }
        }
        if (Input.GetButtonDown("No Weapon"))
        {
            PlaceWeaponToPocket();
        }
		PickMainWeapon ();
		PickSideWeapon ();

    }
    void ToggleCharacterMovement(bool toggle)
	{
		GetComponentInParent<CharacterController>().enabled = toggle;
		GetComponentInParent<FPSInputController>().enabled = toggle;
		GetComponentInParent<CharacterMotor>().enabled = toggle;
	}
    void UseItem()
    {
		GameObject UsableItem = LookingAtThisObject; 
        if (hit.collider.tag == "UseableItem")
        {

			if (UsableItem.GetComponent<InteractableItemScript> () != null)
			{
				Interact interAct = GetComponent<Interact> ();
				GetComponentInParent<PlayerStatsScript>().Money += UsableItem.GetComponent<InteractableItemScript> ().UseItem (interAct,Player);
				UsableItem.GetComponent<InteractableItemScript> ().twoHand = twoHand;
			}
				
        }
    }

	#region CarFunctions
    IEnumerator CarExitTimer(float sec)
    {
        yield return new WaitForSeconds(sec);
        activeObject.GetComponent<Rigidbody>().detectCollisions = true;
		GetComponentInParent<CharacterController> ().enabled = true;
		GetComponentInParent<FPSInputController> ().enabled = true;
        Debug.Log("Ido");
    }
    void GetInTheCar()
	{
		activeObject = LookingAtThisObject;
		if(hit.collider.tag == "Car") 
		{
			PlaceWeaponToPocket();
			Debug.Log ("Beszallas a kocsiba");
			ToggleCharacterMovement(false);
			activeObject.GetComponent<AltCarMove>().active = true;
			activeObject.GetComponent<Rigidbody>().detectCollisions = true;
			GetComponentInParent<CharacterMotorExt>().isInCar = true;
			GetComponentInParent<CharacterMotorExt>().carSeat = LookingAtThisObject.transform.Find("CarSeat").gameObject;
			GetComponentInParent<CharacterMotorExt>().activeCar = LookingAtThisObject;
			grafika.GetComponent<MeshRenderer> ().enabled = false;
			isInCar = true;
			LookingAtThisObject = null;
		}
	}
	void GetOutFromTheCar()
	{
        PlaceWeaponToPocket();
		Debug.Log ("Kiszallas a kocsibol");
		ToggleCharacterMovement(true);
        activeObject.GetComponent<AltCarMove>().active = false;
        GetComponentInParent<CharacterMotorExt> ().exitCar = true;
		activeObject.GetComponent<AltCarMove>().Brake();
		GetComponentInParent<CharacterMotorExt> ().isInCar = false;
		GetComponentInParent<CharacterMotorExt> ().carSeat = null;
		GetComponentInParent<CharacterMotorExt> ().activeCar = null;
        activeObject.GetComponent<Rigidbody>().detectCollisions = false;
		GetComponentInParent<CharacterController> ().enabled = false;
		grafika.GetComponent<MeshRenderer> ().enabled = true;
        isInCar = false;
        StartCoroutine(CarExitTimer(0.2f));
    }
	#endregion

	#region WeaponFunctions
    void PickUpWeapon()
	{
		if((hit.collider.tag == "SideWeapon" || hit.collider.tag == "MainWeapon") && !isHandFull)
		{
			Weapon = hit.collider.gameObject;
			isHandFull = true;
			WeaponTube = Weapon.GetComponent<WeaponPickup>().WeaponTube;
				
			Weapon.GetComponent<WeaponPickup>().inThisHand = gameObject;
			GetComponent<Shooting>().WeaponTube = WeaponTube;
			GetComponent<Shooting>().isWeaponInHand = true;
				
			Weapon.GetComponent<BoxCollider>().enabled = false;
			Weapon.GetComponent<Rigidbody>().useGravity = false;

		}
	}
	void DropWeapon()
	{
		if(Input.GetButtonDown("Drop Weapon"))
		{
			Debug.Log("q lenyomva");
			if(Weapon != null)
			{
				GetComponent<Shooting>().isWeaponInHand = false;
				Weapon.GetComponent<WeaponPickup>().inThisHand = null;
				
				Weapon.GetComponent<Rigidbody>().useGravity = true;
				Weapon.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
				Weapon.GetComponent<BoxCollider>().enabled = true;

				WeaponTube = null;
				Weapon = null;
				isHandFull = false;
			}
		}
	}
	void PlaceWeaponToPocket()
	{
		if(isHandFull && Weapon != null)
		{
			Debug.Log("0 lenyomva");
			
			
			if(Weapon.tag == "MainWeapon" && WeaponSlots[0, 0] == null)
			{
				WeaponSlots[0,0] = Weapon;
				WeaponSlots[0,1] = WeaponTube;
				Weapon.GetComponent<WeaponPickup>().BackPocket = BackPocket;
                Weapon.GetComponent<WeaponPickup>().isInPocket = true;
                Weapon.GetComponent<WeaponPickup>().inThisHand = null;
                GetComponent<Shooting>().isWeaponInHand = false;

                isHandFull = false;
                Weapon = null;
                WeaponTube = null;
            }
			else if(Weapon.tag == "SideWeapon" && WeaponSlots[1,0] == null)
			{
				WeaponSlots[1,0] = Weapon;
				WeaponSlots[1,1] = WeaponTube;
				Weapon.GetComponent<WeaponPickup>().SidePocket = SidePocket;
                Weapon.GetComponent<WeaponPickup>().isInPocket = true;
                Weapon.GetComponent<WeaponPickup>().inThisHand = null;
                GetComponent<Shooting>().isWeaponInHand = false;

                isHandFull = false;
                Weapon = null;
                WeaponTube = null;
            }
			
			
		}
	}
    void PickMainWeapon()
	{
		if (Input.GetButtonDown("Main Weapon") && isHandFull == false)
		{
			Debug.Log("1 lenyomva");
			if(WeaponSlots[0,0] != null)
			{
				Weapon = WeaponSlots[0,0];
				WeaponTube = WeaponSlots[0,1];
				Weapon.GetComponent<WeaponPickup>().isInPocket = false;
				Weapon.GetComponent<WeaponPickup>().inThisHand = gameObject;
				GetComponent<Shooting>().WeaponTube = WeaponTube;
				GetComponent<Shooting>().isWeaponInHand = true;
				isHandFull = true;
				WeaponSlots[0,0] = null;
				WeaponSlots[0,1] = null;
			}
		}
	}
	void PickSideWeapon()
	{
		if (Input.GetButtonDown("Side Weapon")&& isHandFull == false)
		{
			Debug.Log("2 lenyomva");
			if(WeaponSlots[1,0] != null)
			{
				Weapon = WeaponSlots[1,0];
				WeaponTube = WeaponSlots[1,1];
				Weapon.GetComponent<WeaponPickup>().isInPocket = false;
				Weapon.GetComponent<WeaponPickup>().inThisHand = gameObject;
				GetComponent<Shooting>().WeaponTube = WeaponTube;
				GetComponent<Shooting>().isWeaponInHand = true;
				isHandFull = true;
				WeaponSlots[1,1] = null;
				WeaponSlots[1,0] = null;
			}
		}
	}
	#endregion
}
