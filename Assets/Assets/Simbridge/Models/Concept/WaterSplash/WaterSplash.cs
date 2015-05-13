/// <summary>
/// WasterSplash gets attached to water surfaces.
/// Water must be a trigger.
/// </summary>
using UnityEngine;
using System.Collections;

public class WaterSplash : MonoBehaviour {
	
	public Transform splashPrefab;
	public AudioClip[] splashSounds = new AudioClip[5];
	public GameObject lastObject;
	public Vector3 lastPosition;
	public float lastTime;
	private float splashDistance = 2.0f; // 2m Minimum distance to travel before triggering another splash
	private float splashTime = 0.1f; // .1s Minimum time to elapse before triggering another splash
	
	void OnTriggerEnter( Collider collidee )
	{
//		if ( Time.time - lastTime >= splashTime )
//		{
			Splash( collidee );

//		}
	}
	
	void Splash ( Collider collidee )
	{
		Vector3 splashPosition = new Vector3( collidee.transform.position.x, transform.position.y+0.01f, collidee.transform.position.z);
		GameObject splash = Instantiate(splashPrefab, splashPosition, this.transform.rotation ) as GameObject; // assumes water splashes pointing up, ///TODO: rotate for surface normal
		AudioSource.PlayClipAtPoint( splashSounds[ Random.Range( 0, splashSounds.Length ) ], splashPosition );
		lastPosition = collidee.transform.position;
		lastTime = Time.time;
		lastObject = collidee.gameObject;
		//splash.transform.localScale = collidee.transform.lossyScale; // scale splash to collidee size
	}
	
	void OnTriggerStay( Collider collidee )
	{
		// If we haven't triggered a splash within splashTime, maybe we should trigger another
		if ( Time.time - lastTime >= splashTime )
		{
//			if ( collidee.gameObject != lastObject )
//			{
//				Splash ( collidee );
//			}
			// If the object that collided before is still moving and reaches splashDistance, reset lastPosition count and play splash audio
			if ( collidee.gameObject == lastObject && Vector3.Distance( collidee.transform.position, lastPosition ) > splashDistance )
			{
				Splash( collidee );
			}
			// either way, if player set lastObject to this one
//			if ( collidee.tag == "Player" )
//			{
//				lastObject = collidee.gameObject;
//			}
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

