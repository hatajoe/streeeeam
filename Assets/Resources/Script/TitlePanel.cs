﻿using UnityEngine;
using System.Collections;
using UniRx; using UniRx;
using UnityEngine.UI;


public class TitlePanel : MonoBehaviour 
{
	public Button startButton = null;

	// Use this for initialization
	void Start () 
	{
		startButton.onClick.AsObservable().Subscribe(_ =>StartPlay());
	}

	private void StartPlay()
	{
		MainCanvas.GetInstance().ChangePhase(MainCanvas.Phase.PlayMode);
	}
}
