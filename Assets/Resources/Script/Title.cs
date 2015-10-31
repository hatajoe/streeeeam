using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour 
{
	private Animator animator = null;
	private bool     started = false;

	// Use this for initialization
	void Awake () 
	{
		animator = this.gameObject.GetComponentInChildren<Animator>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{

		if ( animator == null )
			return; 

		// start animation.
		if ( MainCanvas.GetInstance().IsPhase(MainCanvas.Phase.Play) && started == false )
		{
			animator.SetTrigger("Start");
			started = true;
		}
	}

}
