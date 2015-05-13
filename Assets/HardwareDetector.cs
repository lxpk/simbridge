using UnityEngine;
using System.Collections;
using Ovr;

public class HardwareDetector : MonoBehaviour {

	public GameObject oculusOVRController;
	public GameObject keyboardMouseFPSController;
	bool isConnected = false;

	// Use this for initialization
	void Start () {

		if (OVRManager.display.isPresent)
		{
			isConnected = true;
		}

		if (isConnected)
		{
			oculusOVRController.SetActive(true);
			keyboardMouseFPSController.SetActive(true);
		}
		else
		{
			keyboardMouseFPSController.SetActive(true);
			oculusOVRController.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
