using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
					, ITouchPanelEventObserver
{
	//=== Inspector
	public Vector3 boostForce = new Vector3();

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
	void Update () {
	
	}

	//=== @interface ITouchPanelEventObserver 
	public void Touching(TouchPanel panel)
	{
		this.rigidbody.AddForce(boostForce, ForceMode.Acceleration);
	}

	public void Clicked(TouchPanel panel)
	{
		// No action.
	}

}
