using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
					, ITouchPanelEventObserver
{
	public GameObject planet;

	//=== Inspector
	public float gravity;
	public float boost;

	//=== Properties
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

	void Start () 
	{
		TouchPanel.GetInstance().SetObserver(this);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		var g = this.planet.transform.position - this.transform.position;
		g.Normalize ();
		this.rigidbody.AddForce(g * this.gravity, ForceMode.Acceleration);
	}
	
	//=== @interface ITouchPanelEventObserver 
	public void Touching(TouchPanel panel)
	{
		var g = this.transform.position - this.planet.transform.position;
		g.Normalize ();
		var cross = Vector3.Cross (g, Vector3.forward);
		cross.Normalize ();
		var dir = g + cross;
		dir.Normalize ();

		var dot = Vector3.Dot(new Vector3(this.transform.rotation.x, this.transform.rotation.y, 1.0f), dir);
		dot = (dot - 1.0f)*-90;
		this.transform.Rotate (new Vector3(this.transform.rotation.x, this.transform.rotation.y, dir.z), dot);

		this.rigidbody.AddForce(dir * this.boost, ForceMode.Impulse);
	}

	public void Clicked(TouchPanel panel)
	{
		// No action.
	}

	//=== Collision
	void OnCollisionEnter(Collision collision)
	{
		// Hit action
	}
	
	void OnCollisionStay(Collision collision)
	{
	}
	
	void OnCollisionExit(Collision collision)
	{
	}
}
