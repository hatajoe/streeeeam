﻿using UnityEngine;
using System.Collections;

public class MainCanvas : MonoBehaviour 
{
	public GameObject	title    = null; 
	public GameObject   playmode = null; 
	public GameObject   goodbye  = null;
	public GameObject   pause    = null;

	[System.NonSerialized] public static MainCanvas s_instance = null;

	public enum Phase
	{
		Title = 1,
		PlayMode,
		GoodBye,
		Pause,
		Play,
	}

	private Phase phase = 0;
	

	// Use this for initialization
	void Start ()
	{
		MainCanvas.s_instance = this;
		this.ChangePhase(Phase.Title);
	}

	public static MainCanvas GetInstance()
	{
		return s_instance;
	}


	public bool IsPhase( MainCanvas.Phase phase )
	{
		if ( this.phase == phase )
			return true;

		return false;
	}


	public void ChangePhase( MainCanvas.Phase phase )
	{
		this.phase = phase; 

		switch( this.phase )
		{
		case MainCanvas.Phase.Title:
		{
			this.SetupTitle();
			break;
		}
		case MainCanvas.Phase.PlayMode:
		{
			this.SetupPlayMode();
			break;
		}
		case MainCanvas.Phase.GoodBye:
		{
			this.SetupGoodBye();
			break;
		}
		case MainCanvas.Phase.Pause:
		{
			this.SetupPause();
			break;
		}
		case MainCanvas.Phase.Play:
		{
			this.SetupPlay();
			break;
		}
		}
	}

	private void SetupTitle()
	{
		this.title.SetActive(true);

		this.playmode.SetActive(false);
		this.goodbye.SetActive(false); 
		this.pause.SetActive(false);
	}


	private void SetupPlayMode()
	{
		this.playmode.SetActive(true);

		this.title.SetActive(false);		
		this.goodbye.SetActive(false); 
		this.pause.SetActive(false);
	}

	private void SetupGoodBye()
	{
		this.goodbye.SetActive(true);

		this.title.SetActive(false);
		this.playmode.SetActive(false);
		this.pause.SetActive(false);
	}

	private void SetupPause()
	{
		this.pause.SetActive(true);

		this.title.SetActive(false);		
		this.playmode.SetActive(false);
		this.goodbye.SetActive(false); 
	}

	private void SetupPlay()
	{
		this.title.SetActive(false);		
		this.playmode.SetActive(false);
		this.goodbye.SetActive(false); 
		this.pause.SetActive(false);
	}
}