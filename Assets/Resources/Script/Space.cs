using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Space : MonoBehaviour {

	public static int   COMMET_COUNT = 50;
	public static float COMMET_INITIAL_POS_Y_MIN = 12.00f;
	public static float COMMET_INITIAL_POS_Y_MAX = 32.00f;
	public static float COMMET_INITIAL_DELTA_MIN = 300.0f;
	public static float COMMET_INITIAL_DELTA_MAX = 1050.0f;
	public static float COMMET_VELOCITY_MIN = 0.03f;
	public static float COMMET_VELOCITY_MAX = 0.08f;
	public static float COMMET_SCALE_MIN = 2.8f;
	public static float COMMET_SCALE_MAX = 6.0f;

	public static int   MONEY_COUNT = 10;
	public static float MONEY_INITIAL_POS_Y_MIN = 22.00f;
	public static float MONEY_INITIAL_POS_Y_MAX = 42.00f;
	public static float MONEY_INITIAL_DELTA_MIN = 300.0f;
	public static float MONEY_INITIAL_DELTA_MAX = 1050.0f;
	public static float MONEY_VELOCITY_MIN = 0.03f;
	public static float MONEY_VELOCITY_MAX = 0.08f;

	public GameObject commet;
	public GameObject money;
	public GameObject player;
	public GameObject demo;
	private List<Commet> commets;
	private List<Money> moneys;

	// Use this for initialization
	void Start () {

		this.CreateCommet(COMMET_COUNT);

		StartCoroutine ("GenerateMoney");
	}

	private IEnumerator GenerateMoney() {
		while (true) {
			this.CreateMoney(MONEY_COUNT);
			yield return new WaitForSeconds (60.0f);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	private float GetRandomByRange(float min, float max) {
		return Random.value * (max - min) + min;
	}

	public void Setup()
	{
		this.RemoveCommets(); 
		this.CreateCommet(COMMET_COUNT);
		this.CreatePlayer();
	}

	public void SetupForPUN()
	{
		this.RemoveCommets(); 
		this.CreateCommetForPUN(COMMET_COUNT);
	}

	private void RemoveCommets()
	{
		if ( this.commets == null )
			return;

		foreach (Commet commet in this.commets)
		{
			Destroy(commet.gameObject);
		}
	}

	private void CreateMoney( int count) {
		this.moneys = new List<Money> ();
		
		for (int i = 0; i < count; i++) {
			float y = this.GetRandomByRange(MONEY_INITIAL_POS_Y_MIN, MONEY_INITIAL_POS_Y_MAX);
			float initialDelta = this.GetRandomByRange(MONEY_INITIAL_DELTA_MIN, MONEY_INITIAL_DELTA_MAX);
			float velocity = this.GetRandomByRange(MONEY_VELOCITY_MIN, MONEY_VELOCITY_MAX);

			GameObject obj = (GameObject)Instantiate (this.money);
			
			Money m = obj.GetComponentInChildren<Money>();
			m.Init(y, initialDelta, velocity);
			this.moneys.Add (m);
		}
	}

	private void CreateCommet( int count )
	{
		this.commets = new List<Commet> ();
		
		for (int i = 0; i < count; i++) {
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
	
	private void CreateCommetForPUN( int count )
	{
		this.commets = new List<Commet> ();

		for (int i = 0; i < count; i++) {
			float y = this.GetRandomByRange(COMMET_INITIAL_POS_Y_MIN, COMMET_INITIAL_POS_Y_MAX);
			float initialDelta = this.GetRandomByRange(COMMET_INITIAL_DELTA_MIN, COMMET_INITIAL_DELTA_MAX);
			float velocity = this.GetRandomByRange(COMMET_VELOCITY_MIN, COMMET_VELOCITY_MAX);
			float scale  = this.GetRandomByRange(COMMET_SCALE_MIN, COMMET_SCALE_MAX);

			GameObject obj = PhotonNetwork.Instantiate(commet.name, commet.transform.position, commet.transform.localRotation, 0);

			Commet c = obj.GetComponentInChildren<Commet>();
			c.Init(y, initialDelta, velocity, scale);
			this.commets.Add (c);
		}
	}

	public void CreatePlayer()
	{
		GameObject obj = (GameObject)Instantiate(this.player, demo.transform.position, demo.transform.localRotation);
	
		obj.GetComponentInChildren<Player>().RegisterToTrackingCamera();
		obj.transform.SetParent(this.gameObject.transform.parent);

		Destroy(demo);
	}

	public void CreatePlayerForPUN()
	{
		GameObject obj = PhotonNetwork.Instantiate("playerPUN", demo.transform.position, demo.transform.localRotation, 0);

		obj.transform.SetParent(this.gameObject.transform.parent);
		Destroy(demo);
	}
}
