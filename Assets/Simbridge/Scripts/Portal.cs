using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	public GameObject black;
	public void Portalize()
	{
		black.SetActive(true);
		Application.LoadLevel(Application.loadedLevel);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
