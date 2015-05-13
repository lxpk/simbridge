using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

    public AudioSource source;
    public void Play()
    {
        source.enabled = true;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
