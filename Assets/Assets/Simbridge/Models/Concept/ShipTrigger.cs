using UnityEngine;
using System.Collections;

public class ShipTrigger : MonoBehaviour {

	public Animation targetAnimation;
	public bool playAutomatically = false;
	// Use this for initialization
	void Start () {	
		if ( playAutomatically )
		{
			Play ();	
		}
	}
	
	void OnTriggerEnter( Collider collidee )
	{
		// if not already playing, start the shipment!
		if ( collidee.tag == "Player" && !targetAnimation.isPlaying )
		{
			Play ();
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
	
	void Play()
	{
		targetAnimation.Play();
	}
}
