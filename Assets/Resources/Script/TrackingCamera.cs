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
		this.transform.Rotate(
			0,
			0,
			this.player.transform.rotation.z	
		);
	}
}
