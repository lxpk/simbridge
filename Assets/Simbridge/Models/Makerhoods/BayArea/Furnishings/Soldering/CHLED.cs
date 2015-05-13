using UnityEngine;
using System.Collections;

public class CHLED : MonoBehaviour {

	public bool on = false;
	public Light light;
	public Material emitterMaterial;
	public bool blink = false;
	public float blinkRate = 0.01f;
	public float lastBlink = 0f;

	// Use this for initialization
	void Start () {
		light = GetComponentInChildren<Light>();
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers)
		{
			if(r.name == "Mesh1")
			{
				emitterMaterial = r.materials[1];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (on)
		{
			emitterMaterial.color = Color.red;
			light.enabled = true;
		}
		else
		{
			emitterMaterial.color = Color.black;
			light.enabled = false;
		}
		if(blink && (Time.time - lastBlink > blinkRate))
		{
			on = !on;
			lastBlink = Time.time;
		}
	}
}
