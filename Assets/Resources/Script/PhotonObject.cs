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
}
