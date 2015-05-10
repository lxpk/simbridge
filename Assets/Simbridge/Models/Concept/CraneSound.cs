using UnityEngine;
using System.Collections;

public class CraneSound : MonoBehaviour {
	
	public AudioClip clip;
	public Transform pickedUpObject;
	public Transform cranePickerUpper;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void PlaySound()
	{
		GetComponent<AudioSource>().PlayOneShot( clip );
	}
	
	void Pickup()
	{
		pickedUpObject.parent = cranePickerUpper;	
	}
	
	void Drop()
	{
		pickedUpObject.parent=null;
	}
}