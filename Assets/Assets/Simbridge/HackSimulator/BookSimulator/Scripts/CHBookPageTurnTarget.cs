using UnityEngine;
using System.Collections;

public class CHBookPageTurnTarget : MonoBehaviour {
	public enum NextOrPrevious
	{
		Next,
		Previous
	}
	public NextOrPrevious nextOrPrevious = NextOrPrevious.Next;
	public MegaBookControl megaBookControl;

	public void DoActivateTrigger()
	{
		if (nextOrPrevious == NextOrPrevious.Next)
		{
			megaBookControl.book.gameObject.SendMessage("NextPage");
		}
		else
		{
			megaBookControl.book.gameObject.SendMessage("PrevPage");
		}
	}
	// Use this for initialization
	void Start () {
		megaBookControl = Component.FindObjectOfType<MegaBookControl>();
	}

	public void OnMouseDown()
	{
		DoActivateTrigger();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
