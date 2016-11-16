using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public Vector3 targetPos;
	public Vector3 origin;
	public Vector3 destination;

	private const float DefaultSpeed = 1.0f;
	public float? Speed;
	public float Delay = 100.0f;
	public float GooDmg = 1.0f;
	float timer;
	Rigidbody rigid;

	float startTime;

	private GUIManagerScript guiManager;

	// Use this for initialization
	void Start () 
	{
		rigid = this.gameObject.GetComponent<Rigidbody>();
		startTime = Time.time;
		Speed = Speed ?? DefaultSpeed;
		guiManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManagerScript>();
		//rigid.velocity = (new Vector3(targetPos.x, targetPos.y * Dist, targetPos.z) - this.transform.position).normalized * 2.0f;
	}

	// Update is called once per frame
	void Update () 
	{
		//this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(targetPos.x,targetPos.y + 0.5f, targetPos.z), 3.0f * Time.deltaTime);
		//rigid.AddForce((targetPos - this.transform.position).normalized * 3.0f);
		//Vector3.MoveTowards(transform.position, targetPos, 10.0f);*/

		//float fracJourney = (Time.time - startTime) * Speed.GetValueOrDefault();

		//this.transform.position = Vector3.Lerp (origin, destination, fracJourney);


		timer += Time.deltaTime;

		if(timer >= Delay) {
			timer = 0;
			Destroy(this.gameObject);
		}

	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.CompareTag("Player"))
		{
			other.GetComponent<Player.Controller>().health -= GooDmg;
			guiManager.canShowDamageOverlay = true;
			Destroy(this.gameObject);
		}

	}
}
