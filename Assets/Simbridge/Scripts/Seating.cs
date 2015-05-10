using UnityEngine;
using System.Collections;

public class Seating : MonoBehaviour {

	public bool seated = false;
	public GameObject player;
	public Vector3 standingPosition;
	public Transform seatedPositionTransform;
	public ActivateTrigger sitTrigger;
	public ActivateTrigger standTrigger;

	public void Sit()
	{
		seated = true;
		standingPosition = player.transform.position;
		player.transform.position = seatedPositionTransform.position;
		sitTrigger.on = false;
		standTrigger.on = true;
	}

	public void Stand()
	{
		seated = false;
		player.transform.position = standingPosition;
		sitTrigger.on = true;
		standTrigger.on = false;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find("CHPlayer");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(seated)
		{
			player.transform.position = seatedPositionTransform.position;
			if(Input.GetAxis("Vertical") < 0 )
			{
				Stand();
			}
		}
	}
}
