﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Space : MonoBehaviour {

	public static int COMMENT_COUNT = 30;
	public static float COMMET_INITIAL_POS_Y_MIN = 12.00f;
	public static float COMMET_INITIAL_POS_Y_MAX = 22.00f;
	public static float COMMET_INITIAL_DELTA_MIN = 300.0f;
	public static float COMMET_INITIAL_DELTA_MAX = 1050.0f;
	public static float COMMET_VELOCITY_MIN = 0.03f;
	public static float COMMET_VELOCITY_MAX = 0.08f;
	public static float COMMET_SCALE_MIN = 2.8f;
	public static float COMMET_SCALE_MAX = 6.0f;

	public GameObject commet;
	private List<Commet> commets;

	// Use this for initialization
	void Start () {
		this.commets = new List<Commet> ();
		for (int i = 0; i < COMMENT_COUNT; i++) {
			float y = this.GetRandomByRange(COMMET_INITIAL_POS_Y_MIN, COMMET_INITIAL_POS_Y_MAX);
			float initialDelta = this.GetRandomByRange(COMMET_INITIAL_DELTA_MIN, COMMET_INITIAL_DELTA_MAX);
			float velocity = this.GetRandomByRange(COMMET_VELOCITY_MIN, COMMET_VELOCITY_MAX);
			float scale  = this.GetRandomByRange(COMMET_SCALE_MIN, COMMET_SCALE_MAX);

			GameObject obj = (GameObject)Instantiate (commet);

			Commet c = obj.GetComponentInChildren<Commet>();
			c.Init(y, initialDelta, velocity, scale);
			this.commets.Add (c);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private float GetRandomByRange(float min, float max) {
		return Random.value * (max - min) + min;
	}
}
