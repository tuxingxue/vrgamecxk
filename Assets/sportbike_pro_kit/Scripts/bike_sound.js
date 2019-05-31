#pragma strict
/// Writen by Boris Chuprin smokerr@mail.ru
var linkToBike : pro_bike5;// making a link to corresponding bike's script
//why it's here ? because you might use this script with many bikes in one scene

var lastGear : int;//we need to know what gear is now
private var highRPMAudio : AudioSource;// makeing second audioSource for mixing idle and high RPMs
private var skidSound : AudioSource;// makeing another audioSource for skidding sound

// creating sounds(Link it to real sound files at editor)
var engineStartSound : AudioClip;
var gearingSound : AudioClip;
var IdleRPM : AudioClip;
var highRPM : AudioClip;
var skid : AudioClip;

//we need to know is any wheel skidding
var isSkidingFront : boolean = false;
var isSkidingRear : boolean = false;

private var ctrlHub : GameObject;// gameobject with script control variables 
private var outsideControls : controlHub;// making a link to corresponding bike's scriptt


function Start () {
	ctrlHub = GameObject.Find("gameScenario");//link to GameObject with script "controlHub"
	outsideControls = ctrlHub.GetComponent(controlHub);//to connect c# mobile control script to this one
	
	//assign sound to audioSource
	highRPMAudio = gameObject.AddComponent(AudioSource);
	highRPMAudio.loop = true;
    highRPMAudio.playOnAwake = false;
    highRPMAudio.clip = highRPM;
    highRPMAudio.pitch = 0;
    highRPMAudio.volume = 0.0;
    //same assign for skid sound
    skidSound = gameObject.AddComponent(AudioSource);
	skidSound.loop = false;
    skidSound.playOnAwake = false;
    skidSound.clip = skid;
    skidSound.pitch = 1.0;
    skidSound.volume = 1.0;
    
	//real-time linking to current bike
 	linkToBike = this.GetComponent(pro_bike5);
	GetComponent.<AudioSource>().PlayOneShot(engineStartSound);
	playEngineWorkSound();
	lastGear = linkToBike.CurrentGear;
}


function Update(){
	
	//Idle plays high at slow speed and highRPM sound play silent at same time. And vice versa.
	GetComponent.<AudioSource>().pitch = Mathf.Abs(linkToBike.EngineRPM  / linkToBike.MaxEngineRPM) + 1.0;
	GetComponent.<AudioSource>().volume = 1.0 - (Mathf.Abs(linkToBike.EngineRPM  / linkToBike.MaxEngineRPM));
	highRPMAudio.pitch = Mathf.Abs(linkToBike.EngineRPM  / linkToBike.MaxEngineRPM);
	highRPMAudio.volume = Mathf.Abs(linkToBike.EngineRPM  / linkToBike.MaxEngineRPM);

	// all engine sounds stop when restart
	if (outsideControls.restartBike){
		GetComponent.<AudioSource>().Stop();
		GetComponent.<AudioSource>().pitch = 1.0;
		GetComponent.<AudioSource>().PlayOneShot(engineStartSound);
		playEngineWorkSound();
	}
	
	//gear change sound
	if (linkToBike.CurrentGear != lastGear){
		GetComponent.<AudioSource>().PlayOneShot(gearingSound);//звук переключения передач
		lastGear = linkToBike.CurrentGear;
	}
	//skids sound
	if (linkToBike.coll_rearWheel.sidewaysFriction.stiffness < 0.5 && !isSkidingRear && linkToBike.bikeSpeed >1){//почему 0.5 ? как бы лучше это обыграть ?
			skidSound.Play();
			isSkidingRear = true;
	} else if (linkToBike.coll_rearWheel.sidewaysFriction.stiffness >= 0.5 && isSkidingRear || linkToBike.bikeSpeed <=1){
				skidSound.Stop();
				isSkidingRear = false;
	}
	if (linkToBike.coll_frontWheel.brakeTorque >= (linkToBike.frontBrakePower-10) && !isSkidingFront && linkToBike.bikeSpeed >1){
			skidSound.Play();
			isSkidingFront = true;
	} else if (linkToBike.coll_frontWheel.brakeTorque < linkToBike.frontBrakePower && isSkidingFront || linkToBike.bikeSpeed <=1){
				skidSound.Stop();
				isSkidingFront = false;
	}
}

function playEngineWorkSound(){
	yield WaitForSeconds(1);//need a pause to hear ingnition sound first 
	GetComponent.<AudioSource>().clip = IdleRPM;
	GetComponent.<AudioSource>().Play();
	highRPMAudio.Play();
}