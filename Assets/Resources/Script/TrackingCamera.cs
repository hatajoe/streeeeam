using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (
			this.player.transform.localPosition.x,
			this.player.transform.localPosition.y,
			this.transform.localPosition.z
		);
		/*
		this.transform.Rotate(
			this.player.transform.rotation.x,
			this.player.transform.rotation.y,
			this.transform.position.z	
		);
		*/
	}
}
