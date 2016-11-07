using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public Vector3 targetPos;
	public Vector3 origin;
	public Vector3 destination;

	private const float DefaultSpeed = 1.0f;
	public float? Speed;
	public float Delay = 10.0f;
	float timer;
	Rigidbody rigid;

	float startTime;

	// Use this for initialization
	void Start () 
	{
		rigid = this.gameObject.GetComponent<Rigidbody>();
		startTime = Time.time;
		Speed = Speed ?? DefaultSpeed;
	}

	// Update is called once per frame
	void Update () 
	{
		this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(targetPos.x,targetPos.y + 0.5f, targetPos.z), 3.0f * Time.deltaTime);
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
}
