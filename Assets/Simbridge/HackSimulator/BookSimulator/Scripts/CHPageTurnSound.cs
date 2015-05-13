using UnityEngine;
using System.Collections;

/// <summary>
/// Use in combination with a MegaBook and a RandomizeSound script.
/// </summary>
public class CHPageTurnSound : MonoBehaviour {

	public void NextPage()
	{
		this.gameObject.SendMessage("Play");
	}

	public void PrevPage()
	{
		this.gameObject.SendMessage("Play");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
