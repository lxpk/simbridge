//http://www.youtube.com/watch?v=X1jKBja6CcM
// see also for ideas: http://forum.unity3d.com/threads/38144-Gravity-gun!
var catchRange = 30.0;
var holdDistance = 4.0;
var minForce = 1000;
var maxForce = 10000;
var forceChargePerSec = 3000;
var layerMask : LayerMask = -1;

enum GravityGunState { Free, Catch, Occupied, Charge, Release};
private var gravityGunState : GravityGunState = 0;
private var rigid : Rigidbody = null;
private var currentForce = minForce;

function FixedUpdate () {
	if(gravityGunState == GravityGunState.Free) {
		if(Input.GetButton("Fire1")) {
			var hit : RaycastHit;
			if(Physics.Raycast(transform.position, transform.forward, hit, catchRange, layerMask)) {
				
				// RECOGNIZE TARGET AND HANDLE IT
				// Rigidify atoms that aren't rigid yet
				if( hit.collider && !hit.rigidbody )
				{
					hit.transform.gameObject.SendMessage("Rigidify");
				}
				
				// IF RIGID, CATCH IT
				if(hit.rigidbody) {
					rigid = hit.rigidbody;
					gravityGunState = GravityGunState.Catch;
					
					// for debuging, remove it
					print("force: " + currentForce);
				}
			}
		}
	}
	else if(gravityGunState == GravityGunState.Catch) {
		rigid.MovePosition(transform.position + transform.forward * holdDistance);
		if(!Input.GetButton("Fire1"))
			gravityGunState = GravityGunState.Occupied;
	}
	else if(gravityGunState == GravityGunState.Occupied) {
		rigid.MovePosition(transform.position + transform.forward * holdDistance);
		if(Input.GetButton("Fire1"))
			gravityGunState = GravityGunState.Charge;
	}
	else if(gravityGunState == GravityGunState.Charge) {
		rigid.MovePosition(transform.position + transform.forward * holdDistance);
		if(currentForce < maxForce) {
			currentForce += forceChargePerSec * Time.deltaTime;
		}
		else {
			currentForce = maxForce;
		}
		if(!Input.GetButton("Fire1"))
			gravityGunState = GravityGunState.Release;
			
		// for debuging, remove it
		print("force: " + currentForce);
	}
	else if(gravityGunState == GravityGunState.Release) {
		rigid.AddForce(transform.forward * currentForce);
		currentForce = minForce;
		gravityGunState = GravityGunState.Free;
		
		// for debuging, remove it
		print("");
	}
}
