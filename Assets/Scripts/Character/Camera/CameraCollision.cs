using UnityEngine;
using System.Collections;

public class CameraCollision : MonoBehaviour {
	
	private RaycastHit hitWall;
	private Transform myTransform; 
	public GameObject Player;
	public GameObject CameraOrigin;
	private float aimStep;
	
	private float step;
	public int layerMask;
		
	// Use this for initialization
	void Start () {
		myTransform = transform;
		step = 0;
		layerMask = 1 << 8;
		
	}
	// Update is called once per frame
	void Update () {

		if(myTransform.position == CameraOrigin.transform.position)
		{
			aimStep = 0;
		}
		else
		{
			aimStep = 1;
		}


		
		//Mindig a player felé néz a kamera.
		transform.LookAt(Player.transform.position);

		/*Debug.DrawLine(Player.transform.position,CameraOrigin.transform.position,Color.blue);
		Debug.DrawLine(Player.transform.position,myTransform.position,Color.magenta);*/

		//Linecastok

		//Ha a kamera hozzáér a falhoz vagy a földhöz ott megáll és a felületén csúszik.
		if (Physics.Linecast(Player.transform.position,CameraOrigin.transform.position,out hitWall, layerMask | 1))
		{
			step = 0;
			myTransform.position = new Vector3(hitWall.point.x,hitWall.point.y,hitWall.point.z);
			/*Debug.Log(hitWall.point);
			Debug.Log(hitWall.collider.gameObject);
			Debug.DrawLine(hitWall.point,Player.transform.position,Color.red);*/
		}
		//Amikor a kamera már nem érint semmit, akkor a "step"-et gyorsítja és azzal a sebességgel megy a helyére (CameraOrigin-ből GameObject-be).
		if(Vector3.Distance(hitWall.point,myTransform.position) > 0.01f)
		{
			myTransform.position = Vector3.MoveTowards(myTransform.position, CameraOrigin.transform.position, step);
			
			if(myTransform.position == CameraOrigin.transform.position) //Ha a helyén van a kamera, nem gyorsítja a "step"-et.
			{
				step = 0;
			}
			else
			{
				step += 0.3f * Time.deltaTime * aimStep;
			}
		}
	}
}
