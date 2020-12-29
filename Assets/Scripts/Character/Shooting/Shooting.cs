using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	public GameObject WeaponTube;
	public bool isWeaponInHand = false;
	public Transform shootPositionTransform;

	private Transform myTransform;
	private float maxRange;
	private Vector3 colliderHitPoint; //Értékkel fel van töltve, használatra kész!

	public bool canShoot;
	private RaycastHit hit;

	private float range;
	public BulletScript Bullet;

	private Vector3 lookAtHitPoint;
	private GameObject LookingAtThisObject;


	private float fireRate; //2 lövés közti idő
	private float nextFire;


	public Vector3 weaponTarget;
	public bool haveATarget;
	/**
	 * Függvények.
	 */

	void BlockShootingNextToWall() //Ha túl közel áll a falhoz nem lő!
	{
		if(Physics.Raycast(shootPositionTransform.position, myTransform.forward,out hit,2))
		{
			/*Debug.DrawLine(shootPositionTransform.position,hit.point,Color.black);
			Debug.Log(hit.collider.name);*/
			canShoot = false;
		}
		else
		{
			canShoot = true;
		}
	}
		
	
	void ShootOnClick() //Bal egérgomb lenyomására megvizsgálja a távot, majd létrehoz egy Object-et aminek kiszámolja az időtartamát
	{
        if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			BulletScript clone = (BulletScript)Instantiate (Bullet, shootPositionTransform.position, Quaternion.Euler (shootPositionTransform.eulerAngles.x+90,
			                                                                         shootPositionTransform.eulerAngles.y, shootPositionTransform.eulerAngles.z));

			Debug.Log("Lövés: " + hit.point);


			// Távolságmérés
			if (Physics.Raycast(shootPositionTransform.position, shootPositionTransform.up,out hit, maxRange)) 
			{
                if (hit.distance < maxRange) // Találat esetén
				{
					//visszaadja hogy hol találta el a collidert és a távolságból kiszámítja a golyó időtartamát.
					Debug.Log (hit.point);
					clone.range = hit.distance;
					colliderHitPoint = hit.point;
				}
				else // Szimplán, ha nem talált, akkor a max távolság egyenlő a távolsággal;
				{
					clone.range = maxRange;
				}
			}
		}
	}
	/**
	 * Függvények vége.
	 */

	// Use this for initialization
	void Start () {
		myTransform = transform;
		canShoot = true;
		maxRange = 30;
		fireRate = 0.1f;
		nextFire = 0;
	}
	// Update is called once per frame
	void Update () {

		if (isWeaponInHand) {
			Ray ray = Camera.main.ScreenPointToRay (new Vector2 (Screen.width / 2, Screen.height / 2));
			int distance = (int)Vector3.Distance (Camera.main.transform.position, WeaponTube.transform.position);

			if (haveATarget = Physics.Raycast (ray.origin, ray.direction, out hit, 500 + distance)) {
				lookAtHitPoint = hit.point;
				LookingAtThisObject = hit.collider.gameObject;

				#region Debug
				Debug.Log ("Célzás: " + hit.point);
				Debug.DrawLine (Camera.main.transform.position, hit.point, Color.green);
				Debug.DrawLine (WeaponTube.transform.position, hit.point, Color.grey);
				weaponTarget = hit.point;
				Debug.Log(weaponTarget);
				#endregion
			} else {
				LookingAtThisObject = null;
			}
		}
		//Debug.DrawRay(shootPositionTransform.position,shootPositionTransform.up*maxRange,Color.grey);
		if(isWeaponInHand)
		{
			shootPositionTransform = WeaponTube.transform;
			BlockShootingNextToWall ();
			if(canShoot == true){
			ShootOnClick ();
			}
		}
		else
		{
			shootPositionTransform = null;
			WeaponTube = null;
		}
	}
}
