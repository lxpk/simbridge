using UnityEngine;
using System.Collections;

public class DepthOfFieldTarget : MonoBehaviour {

	private RaycastHit rayHit;
	private bool raycastReturn;
	public GameObject depthOfFieldTarget;

	// Update is called once per frame
	void Update () 
	{
		raycastReturn = Physics.Raycast(transform.position, transform.forward, out rayHit, Mathf.Infinity);
		// check non-layer filtered mouseover here - copyableCode etc, change crosshair color
		if ((raycastReturn))
		{
			depthOfFieldTarget.transform.position = rayHit.point;
		}
	}
}
