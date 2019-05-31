#pragma strict
/// Writen by Boris Chuprin smokerr@mail.ru
private var myAnimator : Animator;

// variables for turn IK link off for a time
private var IK_rightWeight : int = 1;
private var IK_leftWeight : int = 1;

//variables for moving right/left and forward/backward
private var bikerLeanAngle : float = 0.0;
private var bikerMoveAlong : float = 0.0;

//variables for moving reverse animation
private var reverseSpeed : float = 0.0;

// point of head tracking for(you may disable it or put it on any object you want - the rider will look on that object)
private var lookPoint : Transform;

// standard point of interest for a head
var camPoint : Transform;

// this point may be throwed to anything you want rider looking at
var poi01 : Transform;//for example now it's a gameObject "car". It means when rider near a "car"(distanceToPoi = 50 meters) he will look at "car" until he move far than 50 meters.

//the distance rider will look for POI(point of interest)
var distanceToPoi : float; //in meters = 50 by default

// variables for hand IK joint points
var IK_rightHandTarget :  Transform;
var IK_leftHandTarget :  Transform;

//ragdoll define
var ragDoll : GameObject;

//variable for only one ragdoll create when crashed
private var ragdollLaunched : boolean = false;

//fake joint for physical movement biker to imitate inertia
var fakeCharPhysJoint : Transform;

//we need to know bike we ride on
var bikeRideOn : GameObject;
//why it's here ? because you might use this script with many bikes in one scene

private var bikeStatusCrashed : pro_bike5;// making a link to corresponding bike's script
//why it's here ? because you might use this script with many bikes in one scene

private var ctrlHub : GameObject;// gameobject with script control variables 
private var outsideControls : controlHub;// making a link to corresponding bike's script

function Start () {

ctrlHub = GameObject.Find("gameScenario");//link to GameObject with script "controlHub"
outsideControls = ctrlHub.GetComponent(controlHub);//to connect c# mobile control script to this one

myAnimator = GetComponent(Animator);
lookPoint = camPoint;//use it if you want to rider look at anything when riding

//need to know when bike crashed to launch a ragdoll
bikeStatusCrashed = bikeRideOn.GetComponent(pro_bike5);
myAnimator.SetLayerWeight(2, 0); //to turn off layer with reverse animation which override all other

}

//fundamental mecanim IK script
//just keeps hands on wheelbar :)
function OnAnimatorIK(layerIndex: int) {
	if (IK_rightHandTarget != null){
		myAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand,IK_rightWeight);
    	myAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand,IK_rightWeight);  
    	myAnimator.SetIKPosition(AvatarIKGoal.RightHand,IK_rightHandTarget.position);
    	myAnimator.SetIKRotation(AvatarIKGoal.RightHand,IK_rightHandTarget.rotation);
    }
    if (IK_leftHandTarget != null){
		myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand,IK_leftWeight);
    	myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand,IK_leftWeight);  
    	myAnimator.SetIKPosition(AvatarIKGoal.LeftHand,IK_leftHandTarget.position);
    	myAnimator.SetIKRotation(AvatarIKGoal.LeftHand,IK_leftHandTarget.rotation);
    }
    //same for a head
	myAnimator.SetLookAtPosition(lookPoint.transform.position); 
	myAnimator.SetLookAtWeight(0.5f);//0.5f - means it rotates head 50% mixed with real animations 
}



function Update () {
	//moves character with fake inertia
	if (fakeCharPhysJoint){
		this.transform.localEulerAngles.x = fakeCharPhysJoint.localEulerAngles.x;
		this.transform.localEulerAngles.y = fakeCharPhysJoint.localEulerAngles.y;
		this.transform.localEulerAngles.z = fakeCharPhysJoint.localEulerAngles.z;
	} else return;
	
	//the character should play animations when player press control keys
	//horizontal movement
	if (outsideControls.Horizontal <0 && bikerLeanAngle > -1.0){
		bikerLeanAngle = bikerLeanAngle -= 8 * Time.deltaTime;//8 - "magic number" of speed of pilot's body movement across. Just 8 - face it :)
		if (bikerLeanAngle < outsideControls.Horizontal) bikerLeanAngle = outsideControls.Horizontal;//this string seems strange but it's necessary for mobile version
		myAnimator.SetFloat("lean", bikerLeanAngle);//the character play animation "lean" for bikerLeanAngle more and more
	}
	if (outsideControls.Horizontal >0 && bikerLeanAngle < 1.0){
		bikerLeanAngle = bikerLeanAngle += 8 * Time.deltaTime;
		if (bikerLeanAngle > outsideControls.Horizontal) bikerLeanAngle = outsideControls.Horizontal;
		myAnimator.SetFloat("lean", bikerLeanAngle);
	}
	//vertical movement
	if (outsideControls.Vertical > 0 && bikerMoveAlong < 1.0){
		bikerMoveAlong = bikerMoveAlong += 3 * Time.deltaTime;
		if (bikerMoveAlong > outsideControls.Vertical) bikerMoveAlong = outsideControls.Vertical;
		myAnimator.SetFloat("moveAlong", bikerMoveAlong);
	}
	if (outsideControls.Vertical < 0 && bikerMoveAlong > -1.0){
		bikerMoveAlong = bikerMoveAlong -= 3 * Time.deltaTime;
		if (bikerMoveAlong < outsideControls.Vertical) bikerMoveAlong = outsideControls.Vertical;
		myAnimator.SetFloat("moveAlong", bikerMoveAlong);
	}
	
	//pilot's mass tranform movement
	if (outsideControls.HorizontalMassShift <0 && bikerLeanAngle > -1.0){
		bikerLeanAngle = bikerLeanAngle -= 6 * Time.deltaTime;
		if (bikerLeanAngle < outsideControls.HorizontalMassShift) bikerLeanAngle = outsideControls.HorizontalMassShift;
		myAnimator.SetFloat("lean", bikerLeanAngle);
	}
	if (outsideControls.HorizontalMassShift >0 && bikerLeanAngle < 1.0){
		bikerLeanAngle = bikerLeanAngle += 6 * Time.deltaTime;
		if (bikerLeanAngle > outsideControls.HorizontalMassShift) bikerLeanAngle = outsideControls.HorizontalMassShift;
		myAnimator.SetFloat("lean", bikerLeanAngle);
	}
	if (outsideControls.VerticalMassShift > 0 && bikerMoveAlong < 1.0){
		bikerMoveAlong = bikerMoveAlong += 3 * Time.deltaTime;
		if (bikerLeanAngle > outsideControls.VerticalMassShift) bikerLeanAngle = outsideControls.VerticalMassShift;
		myAnimator.SetFloat("moveAlong", bikerMoveAlong);
	}
	if (outsideControls.VerticalMassShift < 0 && bikerMoveAlong >- 1.0){
		bikerMoveAlong = bikerMoveAlong -= 3 * Time.deltaTime;
		if (bikerLeanAngle < outsideControls.VerticalMassShift) bikerLeanAngle = outsideControls.VerticalMassShift;
		myAnimator.SetFloat("moveAlong", bikerMoveAlong);
	}
	
	//in a case of restart
	if (outsideControls.restartBike){
		//delete ragdoll when restarting scene
		var RGtoDestroy = GameObject.Find("char_ragDoll(Clone)");
		Destroy(RGtoDestroy);
		//make character visible again
		var riderBodyVis = transform.Find("root/Hips");
		riderBodyVis.gameObject.SetActive(true);
		//now we can crash again
		ragdollLaunched = false;
	}
	
	//function for avarage rider pose
	bikerComeback();
	
	//in case of crashed call ragdoll
	if (bikeRideOn.transform.name == "rigid_bike"){
		if (bikeStatusCrashed.crashed && !ragdollLaunched){	
			createRagDoll();
		}
	}

	//scan do rider see POI
	if (poi01.gameObject.SetActive && distanceToPoi > Vector3.Distance(this.transform.position, poi01.transform.position)){
		lookPoint = poi01;
		//if not - still looking forward for a rigidbody POI right before bike
	} else lookPoint = camPoint;
	
	// pull leg(s) down when bike stopped
	if (Mathf.Round((bikeRideOn.GetComponent.<Rigidbody>().velocity.magnitude * 3.6)*10) * 0.1 <= 15 && !bikeStatusCrashed.isReverseOn){//no reverse speed
	reverseSpeed = 0.0;
	myAnimator.SetFloat("reverseSpeed", reverseSpeed);
	
		if (bikeRideOn.transform.localEulerAngles.z <=10 || bikeRideOn.transform.localEulerAngles.z >=350){
			if (bikeRideOn.transform.localEulerAngles.x <=10 || bikeRideOn.transform.localEulerAngles.x >=350){
				var legOffValue = (15-(Mathf.Round((bikeRideOn.GetComponent.<Rigidbody>().velocity.magnitude * 3.6)*10) * 0.1))/15;//need to define right speed to begin put down leg(s)
				myAnimator.SetLayerWeight(3, legOffValue);//leg is no layer 3 in animator
		 	}
		}
	}
	//when using reverse speed
	if (Mathf.Round((bikeRideOn.GetComponent.<Rigidbody>().velocity.magnitude * 3.6)*10) * 0.1 <= 15 && bikeStatusCrashed.isReverseOn){//reverse speed

	myAnimator.SetLayerWeight(3, legOffValue);
	myAnimator.SetLayerWeight(2, 1); //to turn on layer with reverse animation which override all other

		reverseSpeed = bikeStatusCrashed.bikeSpeed/3;
		myAnimator.SetFloat("reverseSpeed", reverseSpeed);
		if (reverseSpeed >= 1.0){
			reverseSpeed = 1.0;
		}
		
		myAnimator.speed = reverseSpeed;
	} else 
	if (Mathf.Round((bikeRideOn.GetComponent.<Rigidbody>().velocity.magnitude * 3.6)*10) * 0.1 > 15){
		reverseSpeed = 0.0;
		myAnimator.SetFloat("reverseSpeed", reverseSpeed);
		myAnimator.SetLayerWeight(3, legOffValue);
		myAnimator.SetLayerWeight(2, 0); //to turn off layer with reverse animation which override all other
		myAnimator.speed = 1;
	}
	

}

function bikerComeback(){
	if (outsideControls.Horizontal == 0 && outsideControls.HorizontalMassShift == 0){
		if (bikerLeanAngle > 0){
			bikerLeanAngle = bikerLeanAngle -= 6 * Time.deltaTime;//6 - "magic number" of speed of pilot's body movement back across. Just 6 - face it :)
			myAnimator.SetFloat("lean", bikerLeanAngle);
		}
		if (bikerLeanAngle < 0){
			bikerLeanAngle = bikerLeanAngle += 6 * Time.deltaTime;
			myAnimator.SetFloat("lean", bikerLeanAngle);
		}
	}
	if (outsideControls.Vertical == 0 && outsideControls.VerticalMassShift == 0){
		if (bikerMoveAlong > 0){
			bikerMoveAlong = bikerMoveAlong -= 2 * Time.deltaTime;//3 - "magic number" of speed of pilot's body movement back along. Just 3 - face it :)
			myAnimator.SetFloat("moveAlong", bikerMoveAlong);
		}
		if (bikerMoveAlong < 0){
			bikerMoveAlong = bikerMoveAlong += 2 * Time.deltaTime;
			myAnimator.SetFloat("moveAlong", bikerMoveAlong);
		}
	}
}


//creating regdoll(we need to scan every bone of character when crashed and copy that preset to created ragdoll)
function createRagDoll(){
  if(!ragdollLaunched){
	var pilotHips = transform.Find("root/Hips");
	var pilotChest = transform.Find("root/Hips/Spine/Chest");
	var pilotHead = transform.Find("root/Hips/Spine/Chest/Neck/Head");
	var pilotLeftArm = transform.Find("root/Hips/Spine/Chest/lShoulder/lArm");
	var pilotLeftForeArm = transform.Find("root/Hips/Spine/Chest/lShoulder/lArm/lForearm");
	var pilotRightArm = transform.Find("root/Hips/Spine/Chest/rShoulder/rArm");
	var pilotRightForeArm = transform.Find("root/Hips/Spine/Chest/rShoulder/rArm/rForearm");
	var pilotLeftUpperLeg = transform.Find("root/Hips/lUpperLeg");
	var pilotLeftLeg = transform.Find("root/Hips/lUpperLeg/lLeg");
	var pilotRightUpperLeg = transform.Find("root/Hips/rUpperLeg");
	var pilotRightLeg = transform.Find("root/Hips/rUpperLeg/rLeg");
	// looking for an current angles of bones rotation
	var pilotHipsAngle = pilotHips.transform.localRotation;
	var pilotChestAngle = pilotChest.transform.localRotation;
	var pilotHeadAngle = pilotHead.transform.localRotation;
	var pilotLeftArmAngle = pilotLeftArm.transform.localRotation;
	var pilotLeftForeArmAngle = pilotLeftForeArm.transform.localRotation;
	var pilotRightArmAngle = pilotRightArm.transform.localRotation;
	var pilotRightForeArmAngle = pilotRightForeArm.transform.localRotation;
	var pilotLeftUpperLegAngle = pilotLeftUpperLeg.transform.localRotation;
	var pilotLeftLegAngle = pilotLeftLeg.transform.localRotation;
	var pilotRightUpperLegAngle = pilotRightUpperLeg.transform.localRotation;
	var pilotRightLegAngle = pilotRightLeg.transform.localRotation;
	//hiding the rider
	var riderBodyVis = transform.Find("root/Hips");
	var currentPilotPosition = this.transform.position;
	var currentPilotRotation = this.transform.rotation;
	riderBodyVis.gameObject.SetActive(false);
	// creating ragdoll
	Instantiate (ragDoll, currentPilotPosition, currentPilotRotation);
	// new empty varables to fill it with learned angles later
	var RDpilotHips = ragDoll.transform.Find("root/Hips");
	var RDpilotChest = ragDoll.transform.Find("root/Hips/Spine/Chest");
	var RDpilotHead = ragDoll.transform.Find("root/Hips/Spine/Chest/Neck/Head");
	var RDpilotLeftArm = ragDoll.transform.Find("root/Hips/Spine/Chest/lShoulder/lArm");
	var RDpilotLeftForeArm = ragDoll.transform.Find("root/Hips/Spine/Chest/lShoulder/lArm/lForearm");
	var RDpilotRightArm = ragDoll.transform.Find("root/Hips/Spine/Chest/rShoulder/rArm");
	var RDpilotRightForeArm = ragDoll.transform.Find("root/Hips/Spine/Chest/rShoulder/rArm/rForearm");
	var RDpilotLeftUpperLeg = ragDoll.transform.Find("root/Hips/lUpperLeg");
	var RDpilotLeftLeg = ragDoll.transform.Find("root/Hips/lUpperLeg/lLeg");
	var RDpilotRightUpperLeg = ragDoll.transform.Find("root/Hips/rUpperLeg");
	var RDpilotRightLeg = ragDoll.transform.Find("root/Hips/rUpperLeg/rLeg");
	// copy known angles to new bones
	RDpilotHips.localRotation = pilotHipsAngle;
	RDpilotChest.localRotation = pilotChestAngle;
	RDpilotHead.localRotation = pilotHeadAngle;
	RDpilotLeftArm.localRotation = pilotLeftArmAngle;
	RDpilotLeftForeArm.localRotation = pilotLeftForeArmAngle;
	RDpilotRightArm.localRotation = pilotRightArmAngle;
	RDpilotRightForeArm.localRotation = pilotRightForeArmAngle;
	RDpilotLeftUpperLeg.localRotation = pilotLeftUpperLegAngle;
	RDpilotLeftLeg.localRotation = pilotLeftLegAngle;
	RDpilotRightUpperLeg.localRotation = pilotRightUpperLegAngle;
	RDpilotRightLeg.localRotation = pilotRightLegAngle;
	ragdollLaunched = true;

	if (bikeRideOn.transform.name == "rigid_bike" && !bikeStatusCrashed.crashed){//check for crahsed status
		bikeStatusCrashed.crashed = true;
		bikeStatusCrashed.GetComponent.<Rigidbody>().centerOfMass = Vector3(0, -0.2, 0);
	}
  }
}