using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
					, ITouchPanelEventObserver
{
	public GameObject planet;
	public bool IsTouched = false;

	//=== Inspector
	public float gravity;
	public float boost;

	//=== Properties
	public Rigidbody rigidbody = null;

	// Use this for initialization
	void Awake()
	{
		Rigidbody rigidbody = this.GetComponentInChildren<Rigidbody>(); 

		if ( rigidbody )
		{
			this.rigidbody = rigidbody;
		}
	}

	void Start () 
	{
		TouchPanel.GetInstance().SetObserver(this);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		var g = this.planet.transform.position - this.transform.position;

		g.Normalize ();
		var cross = Vector3.Cross (g, Vector3.forward);
		cross.Normalize ();
		var dir = g + cross;
		dir.Normalize ();

		var b = this.transform.position - this.planet.transform.position;
		b.Normalize ();

		if (!this.IsTouched) {
			var dot = Vector3.Dot (Vector3.up, b);
			dot = (dot - 1.0f)*90;
			if (b.x < 0.0f) {
				dot = 360 -dot;
			}
//			this.transform.rotation = Quaternion.AngleAxis(dot, Vector3.forward);
		}
		this.rigidbody.AddForce(g * this.gravity, ForceMode.Acceleration);
	}
	
	//=== @interface ITouchPanelEventObserver 
	public void Touching(TouchPanel panel)
	{
		this.IsTouched = true;

		var b = this.transform.position - this.planet.transform.position;
		b.Normalize ();
		var cross = Vector3.Cross (b, Vector3.forward);
		cross.Normalize ();
		var dir = b + cross;
		dir.Normalize ();

		var dot = Vector3.Dot (Vector3.up, b);
		dot = (dot - 1.0f)*90;
		if (b.x < 0.0f) {
			dot = 360 -dot;
		}
		this.transform.rotation = Quaternion.AngleAxis(dot, Vector3.forward);
		this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.AddForce(dir * this.boost, ForceMode.Impulse);
	}

	public void Release(TouchPanel panel)
	{
		this.IsTouched = false;
	}

	public void Clicked(TouchPanel panel)
	{
		// No action.
	}

	//=== Collision
	void OnCollisionEnter(Collision collision)
	{
		GameObject target = collision.gameObject; 
		if ( target.CompareTag("Satelite") ) {
			this.rigidbody.AddForce(this.rigidbody.velocity * -4.0f, ForceMode.Impulse);
		} else if (this.rigidbody.velocity.magnitude < 8.0f) {
			this.rigidbody.AddForce(this.rigidbody.velocity * -3.0f, ForceMode.Impulse);
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
	}
	
	void OnCollisionExit(Collision collision)
	{
	}
}
