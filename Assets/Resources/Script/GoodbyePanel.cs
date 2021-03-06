﻿using UnityEngine;
using System.Collections;
using UniRx; using UniRx;
using UnityEngine.UI;

public class GoodbyePanel : MonoBehaviour {

	public Button retryButton  = null;
	public Button homeButton = null;

	
	// Use this for initialization
	void Start () 
	{
		this.retryButton.onClick.AsObservable().Subscribe(_ => this.ChoseRetry());
		this.homeButton.onClick.AsObservable().Subscribe(_ => this.ChoseHome());
	}
	
	private void ChoseRetry()
	{
		MainCanvas.GetInstance().ChangePhase(MainCanvas.Phase.InitPlay);
		this.DisabelButtons();
	}
	
	private void ChoseHome()
	{
		//MainCanvas.GetInstance().ChangePhase(MainCanvas.Phase.Title);
		this.DisabelButtons();
		Application.LoadLevel("Main");
	}

	private void DisabelButtons()
	{
		this.retryButton.enabled = false;
		this.homeButton.enabled = false;
	}
}
