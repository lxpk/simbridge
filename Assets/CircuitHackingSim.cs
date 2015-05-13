using UnityEngine;
using System.Collections;

public class CircuitHackingSim : MonoBehaviour {

	public GameObject ledTakenGO;
	public bool ledHeld = false;

	public GameObject socketsGO;

	public GameObject ledMountedGO;

	public void TakeLED(string arg)
	{
		// Show the LED we are holding
		ledTakenGO.SetActive(true);
		ledHeld = true;
		socketsGO.SetActive(true);
	}

	public void PlaceLED(string arg)
	{
		ledTakenGO.SetActive(false);
		ledHeld = false;
		socketsGO.SetActive(false);
		ledMountedGO.SetActive(true);
	}

	public void MountLED(string arg)
	{
		PlaceLED("0,0");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
