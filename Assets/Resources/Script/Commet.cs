using UnityEngine;
using System.Collections;

public class Commet : MonoBehaviour {

	public GameObject planet;

	Commet(Vector3 pos, float velocity) {
		this.transform.localPosition = pos;
	}

	// Use this for initialization
	void Start () {
		this.transform.localPosition = new Vector3 (0f, 1.8f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		// Z軸を自オブジェクトの回転を使って回転する。
		Vector3 forward = transform.rotation * Vector3.forward;
		// 念のために正規化する。
		forward.Normalize();

		Quaternion q = Quaternion.AngleAxis(1, forward);
		// 自オブジェクトを回転する。
		transform.rotation = q * transform.rotation;

		transform.RotateAround(planet.transform.position, transform.forward, 0.1f);

		/*
		// 親オブジェクトの位置を取得する。
		var parentPosition = planet.transform.position;
		// 親オブジェクトから自オブジェクトへ向かうベクトルを作成する。
		var dir = transform.position - parentPosition;
		// 自オブジェクトの位置を計算するために、原点でベクトルを回転後、親オブジェクトの座標で移動させる。
		transform.position = q * dir + parentPosition;
		*/
	}
}
