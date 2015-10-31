using UnityEngine;
using System.Collections;

public class Satelite : MonoBehaviour {

	public GameObject planet;
	private float velocity;

	// Use this for initialization
	void Start () {
		this.velocity = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		this.Move (this.velocity);
	}
	
	private void Move(float delta) {
		// Z軸を自オブジェクトの回転を使って回転する。
		Vector3 forward = transform.rotation * Vector3.forward;
		// 念のために正規化する。
		forward.Normalize();
		
		Quaternion q = Quaternion.AngleAxis(this.velocity, forward);
		// 自オブジェクトを回転する。
		transform.rotation = q * transform.rotation;
		
		transform.RotateAround(planet.transform.position, transform.forward * -1.0f, delta);
	}

	//=== Collision
	void OnCollisionEnter(Collision collision)
	{
		GameObject target = collision.gameObject; 
		
		if ( target.CompareTag("Player") )
		{
			//StartCoroutine("Crash");
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
	}
	
	void OnCollisionExit(Collision collision)
	{
	}

}
