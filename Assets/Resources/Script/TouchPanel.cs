using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public interface ITouchPanelEventObserver
{
	void Touching(TouchPanel panel); 
	void Clicked(TouchPanel panel);
	void Release(TouchPanel panel);
}


public class TouchPanel : MonoBehaviour 
						, IPointerDownHandler
						, IPointerUpHandler
						, IPointerClickHandler
{
	[System.NonSerialized] public static TouchPanel s_Instance = null;

	private List<ITouchPanelEventObserver> observers = null;
	private bool touching = false;


	public static TouchPanel GetInstance()
	{
		if ( TouchPanel.s_Instance )
			return TouchPanel.s_Instance;

		GameObject obj = GameObject.Find ("TouchPanel");

		if ( obj )
		{
			TouchPanel.s_Instance = obj.GetComponentInChildren<TouchPanel>(); 
		}

		return TouchPanel.s_Instance;
	}


	// Use this for initialization
	void Awake () 
	{
		observers = new List<ITouchPanelEventObserver>(); 
	}

	void FixedUpdate()
	{
		if ( touching == false )
			return; 

		if ( observers == null )
			return; 
		
		foreach ( ITouchPanelEventObserver observer in observers )
		{
			observer.Touching(this);
		}
	}

	public void SetObserver( ITouchPanelEventObserver observer )
	{
		if ( observers.Contains(observer) == true )
			return; 

		observers.Add(observer);
	}
	 
	public void OnPointerDown (PointerEventData eventData) 
	{
		touching = true;
	}

	public void OnPointerUp (PointerEventData eventData) 
	{
		touching = false;
		if ( observers == null )
			return; 
		
		foreach ( ITouchPanelEventObserver observer in observers )
		{
			observer.Release(this);
		}
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if ( observers == null )
			return; 

		foreach ( ITouchPanelEventObserver observer in observers )
		{
			observer.Clicked(this);
		}
	}
}
