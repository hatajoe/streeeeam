﻿using UnityEngine;
using System.Collections;
using UniRx; using UniRx;
using UnityEngine.UI;


public class PlaymodePanel : MonoBehaviour 
{
	public Button onlineButton  = null;
	public Button endlessButton = null;

	public RandomMatchMaker matchMaker = null;


	// Use this for initialization
	void Start () 
	{
		this.onlineButton.onClick.AsObservable().Subscribe(_ => this.ChoseOnline());
		this.endlessButton.onClick.AsObservable().Subscribe(_ => this.ChoseEndless());
	}

	private void ChoseOnline()
	{
		this.matchMaker.Make();
		this.DisabelButtons();
	}

	private void ChoseEndless()
	{
		MainCanvas.GetInstance().ChangePhase(MainCanvas.Phase.InitPlay);
		this.DisabelButtons();
	}

	private void DisabelButtons()
	{
		this.onlineButton.enabled = false;
		this.endlessButton.enabled = false;
	}
}
