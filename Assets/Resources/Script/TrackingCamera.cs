using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour {

	public Player player = null;
	private Rigidbody rigidbody = null;

	// Use this for initialization
	void Awake()
	{
		Rigidbody rigidbody = this.GetComponentInChildren<Rigidbody>(); 
		
		if ( rigidbody )
		{
			this.rigidbody = rigidbody;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( this.player == null )
			return;

		this.transform.position = new Vector3 (
			this.player.transform.localPosition.x,
			this.player.transform.localPosition.y,
			this.transform.localPosition.z
		);

		if (this.player.IsTouched) {
			this.GetComponent<Rigidbody> ().AddForce (Vector3.forward * -0.1f * this.player.rigidbody.velocity.magnitude, ForceMode.Impulse);
		} else {
			this.GetComponent<Rigidbody> ().AddForce (Vector3.forward * 0.1f, ForceMode.Impulse);
		}

		/*
		this.transform.rotation = Quaternion.Euler (
			this.transform.localRotation.x,
			this.transform.localRotation.y,
			this.player.transform.localPosition.z
		);
		*/
	}
}
