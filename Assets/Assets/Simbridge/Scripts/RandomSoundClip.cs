using UnityEngine;
using System.Collections;

public class RandomSoundClip : MonoBehaviour {
	
	public AudioClip[] sounds;
	public bool playOnStart = false;
	
	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().clip = sounds[ Random.Range (0,sounds.Length-1) ];
		if(playOnStart)
		{
			Play ();
		}
	}
	
	public void Play() {
		GetComponent<AudioSource>().clip = sounds[ Random.Range (0,sounds.Length-1) ];
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
