using UnityEngine;
using System.Collections;

public class PhotonObject : Photon.MonoBehaviour 
{
	public Player player = null;

	void Awake()
	{
		if ( photonView.isMine && player )
		{
			player.RegisterToTrackingCamera();
		}
	}

	void FixedUpdate()
	{
		if ( photonView.isMine == false )
			return;

		if ( this.player == null )
			return;

		if ( this.player.IsDead() == true )
		{
			MainCanvas.GetInstance().ChangePhase(MainCanvas.Phase.GoodBye);
		}
	}
}
