using UnityEngine;

public class ActivateTrigger : MonoBehaviour {
	public enum Mode {
		Trigger   = 0, // Just broadcast the action on to the target
		Replace   = 1, // replace target with source
		Activate  = 2, // Activate the target GameObject
		Enable    = 3, // Enable a component
		Animate   = 4, // Start animation on target
		Deactivate= 5, // Decativate target GameObject
		Message   = 6	// Send a message to the target
	}

	/// The action to accomplish
	public Mode action = Mode.Activate;

	/// The game object to affect. If none, the trigger work on this game object
	public bool on = true;
	public Object target;
	public GameObject source;
	public int triggerCount = 1;///
	public bool repeatTrigger = false;
	public string triggerTagRequirement = "Player";
	public bool activateOnCollision = true;
	public bool activateOnShoot = false;
	public string activateOnShootMessage = "";
	public string animationName ="";
	public string messageMethod = "DoActivateTrigger";
	public string messageArgument = "";
	public void Shoot()
	{
		if(activateOnShoot)
		{
			DoActivateTrigger();
		}
	}
	public void DoActivateTrigger () {
		if(on)
		{
			triggerCount--;

			if (triggerCount > 0 || repeatTrigger) {
				Object currentTarget = target != null ? target : gameObject;
				Behaviour targetBehaviour = currentTarget as Behaviour;
				GameObject targetGameObject = currentTarget as GameObject;
				if (targetBehaviour != null)
					targetGameObject = targetBehaviour.gameObject;
			
				switch (action) {
					case Mode.Trigger:
						targetGameObject.BroadcastMessage ("DoActivateTrigger");
						break;
					case Mode.Replace:
						if (source != null) {
							Object.Instantiate (source, targetGameObject.transform.position, targetGameObject.transform.rotation);
							DestroyObject (targetGameObject);
						}
						break;
					case Mode.Activate:
						targetGameObject.active = true;
						break;
					case Mode.Enable:
						if (targetBehaviour != null)
							targetBehaviour.enabled = true;
						break;	
					case Mode.Animate:
					targetGameObject.GetComponent<Animation>().Play(animationName,PlayMode.StopAll);
						break;	
					case Mode.Deactivate:
						targetGameObject.active = false;
						break;
					case Mode.Message:
						if(messageArgument != "")
						{
							targetGameObject.SendMessage(messageMethod);
						}
						else
						{
							targetGameObject.SendMessage(messageMethod,messageArgument);
						}
						break;
				}
			}
		}
	}

	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == triggerTagRequirement)
		{
			DoActivateTrigger ();
		}
	}
}