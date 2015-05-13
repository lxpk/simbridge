/*using UnityEngine;
using System.Collections;

public class walk.js : MonoBehaviour {
AudioClip[] walkSounds;
AudioClip painLittle;
AudioClip painBig;
AudioClip die;
float audioStepLength = 0.3f;

void PlayStepSounds (){
	CharacterController controller = GetComponent<CharacterController>();

	while (true) {
		if (controller.isGrounded && controller.velocity.magnitude > 0.3f) {
			audio.clip = walkSounds[Random.Range(0, walkSounds.length)];
			audio.Play();
			yield return new WaitForSeconds(audioStepLength);
		} else {
			yield return 0;
		}
	}
}
}*/