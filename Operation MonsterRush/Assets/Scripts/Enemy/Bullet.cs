using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Vector3 targetPos;
	public Vector3 Direction;
	public float Speed;
	public float Delay = 1.0f;
	float timer;

	// Use this for initialization
	void Start () {
		Direction = Vector3.Normalize(targetPos - transform.position);
	}

	// Update is called once per frame
	void Update () {

		transform.Translate (Direction * Speed, Space.World);

		timer += Time.deltaTime;

		if(timer >= Delay) {
			timer = 0;
			Destroy(this.gameObject);
		}
	}
}
