using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;


public class RandomMatchMaker : MonoBehaviour 
{
	Space space = null;


	void Awake()
	{
		GameObject obj = GameObject.Find("space");

		if ( obj )
		{
			space = obj.GetComponentInChildren<Space>();
		}
	}

	// Use this for initialization
	void Start()
	{}

	public void Make()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		PhotonNetwork.logLevel = PhotonLogLevel.Full;
	}

	void OnCreatedRoom()
	{
		if ( this.space )
			return; 

		this.space.SetupForPUN();
	}

	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}

	void OnJoinedRoom()
	{
		this.space.CreatePlayerForPUN();
		MainCanvas.GetInstance().ChangePhase(MainCanvas.Phase.InitOnline);
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom("STREEEEAM");
	}

	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		int count = PhotonNetwork.room.playerCount;
		Debug.Log("Player Count :"+ count);
 	}

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}
