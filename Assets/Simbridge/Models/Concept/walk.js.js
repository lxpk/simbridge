var walkSounds : AudioClip[];
var painLittle : AudioClip;
var painBig : AudioClip;
var die : AudioClip;
var audioStepLength = 0.3;

function PlayStepSounds () {
	var controller : CharacterController = GetComponent(CharacterController);

	while (true) {
		if (controller.isGrounded && controller.velocity.magnitude > 0.3) {
			GetComponent.<AudioSource>().clip = walkSounds[Random.Range(0, walkSounds.length)];
			GetComponent.<AudioSource>().Play();
			yield WaitForSeconds(audioStepLength);
		} else {
			yield;
		}
	}
}