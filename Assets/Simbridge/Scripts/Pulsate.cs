using UnityEngine;
using System.Collections;

public class Pulsate : MonoBehaviour {

    public Material materialToBlend;
    public float startTime = 0f;
    private float elapsedTime = 0f;
    public float pulsateSpeed;

    public Color colorStart = Color.white;
    public Color colorEnd = Color.white;
    public float duration = 10;
    public float bpm = 120;
    private bool pulseInRealTime = true;
	// Use this for initialization
	void Start () {
	    if (pulseInRealTime)
	    {
	        startTime = Time.realtimeSinceStartup;
	    }
	    else
	    {
	        startTime = Time.time;
	    }
	    //colorStart.a = 1.0;
        //colorEnd.a = 0.0;
	    Time.timeScale = 1.0f;
        //duration = bpm / 60f;
	}
	
	// Update is called once per frame
	void Update () {
	    if (pulseInRealTime)
	    {
	        elapsedTime = Time.realtimeSinceStartup - startTime;
	    }
	    else
	    {
	        elapsedTime = Time.time - startTime;
	    }
	    float lerpTime = Mathf.PingPong (elapsedTime, duration) / duration;
        GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerpTime);
	}
}
#pragma strict
