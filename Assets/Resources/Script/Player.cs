using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour 
					, ITouchPanelEventObserver
{
	public static float DEADLINE_DISTANCE = 50f;
	public GameObject planet;
	public bool IsTouched = false;

	public GameObject alertPanel;

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

		this.alertPanel = GameObject.Find ("AlertPanel");
	}

	void Start () 
	{
		TouchPanel.GetInstance().SetObserver(this);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		var diff = this.transform.position - this.planet.transform.position;
		Image alert = this.alertPanel.GetComponent<Image> ();
		alert.color = new Color (
			alert.color.r,
			alert.color.g,
			alert.color.b,
			(diff.magnitude/255)*3f
		);
		if (diff.magnitude >= DEADLINE_DISTANCE) {
			MainCanvas.GetInstance().ChangePhase(MainCanvas.Phase.GoodBye);
			this.gravity = 0f;
		}

		var g = this.planet.transform.position - this.transform.position;

		g.Normalize ();
		var cross = Vector3.Cross (g, Vector3.forward);
		cross.Normalize ();
		var dir = g + cross;
		dir.Normalize ();

		var b = this.transform.position - this.planet.transform.position;
		b.Normalize ();

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
