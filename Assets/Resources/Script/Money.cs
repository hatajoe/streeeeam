using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {

	public GameObject planet;
	private float velocity;
	
	public void Init(float y, float initialDelta, float velocity) {
		this.velocity = velocity;
		this.transform.localPosition = new Vector3 (0f, y, this.transform.localPosition.z);
		this.Move (initialDelta);
	}
	
	// Use this for initialization
	void Start () {
		
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
		
		Quaternion q = Quaternion.AngleAxis(this.velocity * -10f, forward);
		// 自オブジェクトを回転する。
		transform.rotation = q * transform.rotation;
		
		transform.RotateAround(planet.transform.position, transform.forward * -1.0f, delta);
	}
	
	private void Crash()
	{
		Destroy(this.gameObject);
	}
	
	//=== Collision
	void OnCollisionEnter(Collision collision)
	{
		GameObject target = collision.gameObject; 
		
		if ( target.CompareTag("Man") )
		{
			this.Crash();
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
	}
	
	void OnCollisionExit(Collision collision)
	{
	}
}
