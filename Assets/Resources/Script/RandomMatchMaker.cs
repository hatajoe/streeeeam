using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;


public class RandomMatchMaker : MonoBehaviour 
{

	// Use this for initialization
	void Start()
	{}

	public void Make()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		PhotonNetwork.logLevel = PhotonLogLevel.Full;
	}

	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}

	void OnJoinedRoom()
	{
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

		if ( count >= 2 )
		{
			MainCanvas.GetInstance().ChangePhase(MainCanvas.Phase.InitPlay);
		}
 	}

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}
