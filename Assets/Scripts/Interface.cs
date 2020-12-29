using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Interface : MonoBehaviour {

	/**
	 * Perf javítás: Struktúrába gyűjteni a hasonló attribútumokat.
	 */

	public GameObject bottomUIBar;
	private bool isHelpActive;
	public Text interactionText;
	public Text Speed;
	public Text time;
	public GameObject help;
	private Interact interact;
	public GameObject Hand;
	public GameObject Player;
	private GameObject Weapon;

	private int count;
	private float sec;
	private int min;
	private int hour;

	// Use this for initialization
	void Awake () {
		interactionText = bottomUIBar.transform.GetChild(0).GetComponent<Text>();
		Speed = bottomUIBar.transform.GetChild(1).GetComponent<Text>(); 
		time = bottomUIBar.transform.GetChild(2).GetComponent<Text>(); 

		count = 0;
		sec = 0;
		min = 0;
		hour = 0;
	}
	// Update is called once per frame
	void Update () {
		
		sec += Time.deltaTime;

		if (sec >= 60) {
			min++;
			sec = 0;
		}
		if (min == 60) {
			hour++;
			min = 0;
		}
		time.text = hour.ToString("00")+":"+min.ToString("00")+":"+sec.ToString("00");

		if (Player.GetComponent<CharacterMotorExt> ().isInCar == true) 
		{
			if (Input.GetKeyDown (KeyCode.P)) 
			{
				Player.GetComponent<CharacterMotorExt> ().activeCar.transform.rotation = Quaternion.Euler (0, 0, 0);
				Player.GetComponent<CharacterMotorExt> ().activeCar.transform.position += new Vector3(0,2,0);
				Player.GetComponent<CharacterMotorExt> ().activeCar.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}
			Speed.text ="Car name: " + Player.GetComponent<CharacterMotorExt> ().activeCar.name +
				"\nSpeed: " + Player.GetComponent<CharacterMotorExt> ().activeCar.GetComponent<AltCarMove> ().currentspeed.ToString() + " Km/H";
		}
		else
		{
			Speed.text = "";
		}

		if (Input.GetKeyDown (KeyCode.Escape) && !isHelpActive) {
			help.gameObject.SetActive (false);
			isHelpActive = true;
		} 
		else if(Input.GetKeyDown (KeyCode.End) && isHelpActive)
		{
			help.gameObject.SetActive (true);
			isHelpActive = false;
		}

        interact = Hand.GetComponent<Interact>();
        string interactTag;

        if(interact.LookingAtThisObject != null)
        {
            interactTag = interact.LookingAtThisObject.tag;
        }
        else
        {
            interactTag = "";
        }

            switch (interactTag)
            {
                case "SideWeapon":
		case "MainWeapon": interactionText.text = "F to pick up: " + interact.LookingAtThisObject.GetComponent<WeaponPickup>().name; break;
		case "UseableItem": interactionText.text = "" + interact.LookingAtThisObject.name; break;
		case "Car":	interactionText.text = "F to enter car: " + interact.LookingAtThisObject.name; break;
		default:  interactionText.text = ""; break;
            }
	}

}
