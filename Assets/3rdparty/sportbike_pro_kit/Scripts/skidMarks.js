#pragma strict
var linkToBike : pro_bike5;
var skidMarkDecal : Transform;
private var hit : WheelHit;	
private var skidMarkPos : Vector3;
private var rotationToLastSkidmark : Quaternion;
private var lastSkidMarkPos : Vector3;
private var relativePos : Vector3;

function Start () {
	linkToBike = this.GetComponent(pro_bike5);
}
function FixedUpdate(){
	//skidmarks for rear wheel(braking drifting)
	if (linkToBike.coll_rearWheel.sidewaysFriction.stiffness < 0.5 && linkToBike.bikeSpeed >1){
		if (linkToBike.coll_rearWheel.GetGroundHit(hit)){
			skidMarkPos = hit.point;
			skidMarkPos.y += 0.02;
			skidMarkDecal.transform.localScale.x = 1.0;
			if (lastSkidMarkPos != Vector3.zero){
				relativePos = lastSkidMarkPos - skidMarkPos;
				rotationToLastSkidmark = Quaternion.LookRotation(relativePos);
				Instantiate(skidMarkDecal, skidMarkPos, rotationToLastSkidmark);
			}
			lastSkidMarkPos = skidMarkPos;
		}	
	}
	//skidmarks for front wheel(braking)
	if (linkToBike.coll_frontWheel.brakeTorque >= (linkToBike.frontBrakePower-10) && linkToBike.bikeSpeed >1){
		if (linkToBike.coll_frontWheel.GetGroundHit(hit)){
			skidMarkPos = hit.point;
			skidMarkPos.y += 0.02;
			skidMarkDecal.transform.localScale.x = 0.6;
			if (lastSkidMarkPos != Vector3.zero){
				relativePos = lastSkidMarkPos - skidMarkPos;
				rotationToLastSkidmark = Quaternion.LookRotation(relativePos);
				Instantiate(skidMarkDecal, skidMarkPos, rotationToLastSkidmark);
			}
			lastSkidMarkPos = skidMarkPos;
		}	
	}
}

function Update () {
}