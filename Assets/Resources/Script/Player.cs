using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour 
					, ITouchPanelEventObserver
{
	public static float DEADLINE_DISTANCE = 50f;
	public GameObject planet;

	public bool IsTouched = false;
	public float gas = 100.0f;
	public Vector3 lastDir = new Vector3();
	public int money = 0;

	public Vector3 blackhole1 = new Vector3();
	public Vector3 blackhole2 = new Vector3();
	public Vector3 blackhole3 = new Vector3();

	public GameObject alertPanel;
	public Animator    animator;

	public AudioSource jetSE;
	public AudioSource itemSE;
	public AudioSource coinSE;

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
		AudioSource[] audioSources = GetComponents<AudioSource>();
		this.jetSE = audioSources[0];
		this.itemSE = audioSources[1];
		this.coinSE = audioSources[2];
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

		// blackhole
		var b1 = this.blackhole1 - this.transform.position;
		var b2 = this.blackhole2 - this.transform.position;
		var b3 = this.blackhole3 - this.transform.position;
		var gra = this.gravity;
		if (b1.magnitude <= 12.0f) {
			b1.Normalize();
			g = b1;
			gra *= 2.0f;
		} else if (b2.magnitude <= 12.0f) {
			b2.Normalize();
			g = b2;
			gra *= 2.0f;
		} else if (b3.magnitude <= 12.0f) {
			b3.Normalize();
			g = b3;
			gra *= 2.0f;
		}

		var b = this.transform.position - this.planet.transform.position;
		b.Normalize ();

		if (this.gas <= 0) {
			this.rigidbody.AddForce(this.lastDir * gra, ForceMode.Acceleration);
		} else {
			this.rigidbody.AddForce(g * gra, ForceMode.Acceleration);
		}
	}

	public void RegisterToTrackingCamera()
	{
		GameObject camera = GameObject.Find("Main Camera");
		
		if ( camera )
		{
			TrackingCamera tracking = camera.GetComponentInChildren<TrackingCamera>(); 
			
			if ( tracking )
			{
				tracking.player = this;
			}
		}
	}

	public void RegisterBlackhole(Vector3 bl1, Vector3 bl2, Vector3 bl3)
	{
		this.blackhole1 = bl1;
		this.blackhole2 = bl2;
		this.blackhole3 = bl3;
	}

	public void SetBoostLevel( float gas, Animator anim )
	{
		int lv = 0;

		if      ( gas >= 60 ) lv = 3;
		else if ( gas >= 30 ) lv = 2;
		else if ( gas >   0 ) lv = 1;

		anim.SetInteger("Level", lv);
	}

	//=== @interface ITouchPanelEventObserver 
	public void Touching(TouchPanel panel)
	{
		if (this.gas <= 0) {
			return;
		}
		this.IsTouched = true;
		this.animator.SetBool("Touched", this.IsTouched);

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

		var prevGas = this.gas;
		this.gas -= 0.1f;
		if (prevGas > 0 && this.gas <= 0) {
			this.lastDir = cross;
		}

		this.SetBoostLevel(this.gas, this.animator);
	}

	public void Release(TouchPanel panel)
	{
		this.IsTouched = false;
		this.animator.SetBool("Touched", this.IsTouched);
	}

	public void Down(TouchPanel panel)
	{
		if (this.gas <= 0) {
			return;
		}
		this.jetSE.PlayOneShot (this.jetSE.clip);
	}

	public void Clicked(TouchPanel panel)
	{
		// No action.
	}
	
	//=== Collision
	void OnCollisionEnter(Collision collision)
	{
		GameObject target = collision.gameObject; 
		if (target.CompareTag ("Satelite")) {
			this.rigidbody.AddForce (this.rigidbody.velocity * -4.0f, ForceMode.Impulse);
		} else if (target.CompareTag ("Item")) {
			this.itemSE.PlayOneShot (this.itemSE.clip);
			this.gas = 100.0f;
		} else if (target.CompareTag ("Money")) {
			this.itemSE.PlayOneShot (this.coinSE.clip);
			this.money += 1;
			var m = this.alertPanel.transform.Find("MoneyCount");
			Text t = m.GetComponent<Text> ();
			t.text = this.money.ToString();
		} else if (target.CompareTag ("Blackhole")) {
			var dir = new Vector3(Random.value, Random.value, 0f);
			dir.Normalize();
			this.rigidbody.AddForce(dir * 50.0f, ForceMode.Impulse);
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
