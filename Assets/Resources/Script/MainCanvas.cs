using UnityEngine;
using System.Collections;

public class MainCanvas : MonoBehaviour 
{
	public GameObject	title    = null; 
	public GameObject   playmode = null; 
	public GameObject   goodbye  = null;
	public GameObject   pause    = null;
	public Space        space    = null;

	public AudioSource tapSE;

	[System.NonSerialized] public static MainCanvas s_instance = null;

	public enum Phase
	{
		Title = 1,
		PlayMode,
		GoodBye,
		Pause,
		Play,
		InitPlay,
		InitOnline,
	}

	private Phase phase = 0;
	

	// Use this for initialization
	void Start ()
	{
		MainCanvas.s_instance = this;
		this.ChangePhase(Phase.Title);

		this.tapSE = GetComponent<AudioSource>();
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
			this.tapSE.PlayOneShot (this.tapSE.clip);
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
			this.tapSE.PlayOneShot (this.tapSE.clip);
			break;
		}
		case MainCanvas.Phase.Play:
		{
			this.SetupPlay();
			this.tapSE.PlayOneShot (this.tapSE.clip);
			break;
		}
		case MainCanvas.Phase.InitPlay:
		{
			this.SetupInitPlay();
			break;
		}
		case MainCanvas.Phase.InitOnline:
		{
			this.SetupInitOnline();
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

	private void SetupInitPlay()
	{
		this.space.Setup();
		this.ChangePhase(MainCanvas.Phase.Play);
	}

	private void SetupInitOnline()
	{
		this.space.SetupForPUN();
		this.ChangePhase(MainCanvas.Phase.Play);
	}

}
