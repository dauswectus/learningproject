using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatsScript : MonoBehaviour {

	public Text statusText;
	public int Money;
	public int HP;
	public int Armor;
	public int maxHp;
	public float hunger;
	public float thirst;

	void Start()
	{
		Money = 0;
		HP = 100;
		Armor = 0;
		hunger = 100;
		thirst = 100;
	}

	void FixedUpdate()
	{
		
		statusText.text = "$"+Money+"\nHP: "+HP+"\nHunger: "+(int)hunger+" %";

		hunger -= Time.deltaTime*0.1f;
	}
}
