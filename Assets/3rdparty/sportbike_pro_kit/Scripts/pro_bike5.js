#pragma strict
/// Writen by Boris Chuprin smokerr@mail.ru
///////////////////////////////////////////////////////// wheels ///////////////////////////////////////////////////////////
// defeine wheel colliders
var coll_frontWheel : WheelCollider;
var coll_rearWheel : WheelCollider;
// visual for wheels
var meshFrontWheel : GameObject;
var meshRearWheel : GameObject;
// check isn't front wheel in air for front braking possibility
private var isFrontWheelInAir : boolean = true;

//////////////////////////////////////// Stifness, CoM(center of mass), crahsed /////////////////////////////////////////////////////////////
//for stiffness counting when rear brake is on. Need that to lose real wheel's stiffness during time
private var stiffPowerGain : float = 0.0;
//for CoM moving along and across bike. Pilot's CoM.
private var tmpMassShift : float = 0.0;
// crashed status. To know when we need to desable controls because bike is too leaned.
public var crashed : boolean = false;
// there is angles when bike takes status crashed(too much lean, or too much stoppie/wheelie)
var crashAngle01: float;//crashed status is on if bike have more Z(side fall) angle than this												// 70 sport, 55 chopper
var crashAngle02: float;//crashed status is on if bike have less Z(side fall) angle than this 												// 290 sport, 305 chopper
var crashAngle03: float;//crashed status is on if bike have more X(front fall) angle than this 												// 70 sport, 70 chopper
var crashAngle04: float;//crashed status is on if bike have more X(back fall) angle than this												// 280 sport, 70 chopper
											
// define CoM of bike
var CoM : Transform; //CoM object
var normalCoM : float; //normalCoM is for situation when script need to return CoM in starting position										// -0.77 sport, -0.38 chopper
var CoMWhenCrahsed : float; //we beed lift CoM for funny bike turning around when crahsed													// -0.2 sport, 0.2 chopper



//////////////////// "beauties" of visuals - some meshes for display visual parts of bike ////////////////////////////////////////////
var rearPendulumn : Transform; //rear pendulumn
var steeringWheel : Transform; //wheel bar
var suspensionFront_down : Transform; //lower part of front forge
private var normalFrontSuspSpring : int; // we need to declare it to know what is normal front spring state is
private var normalRearSuspSpring : int; // we need to declare it to know what is normal rear spring state is
private var forgeBlocked : boolean  = true; // variable to lock front forge for front braking
//why we need forgeBlocked ?
//There is little bug in PhysX 3.3 wheelCollider - it works well only with car mass of 1600kg and 4 wheels.
//If your vehicle is not 4 wheels or mass is not 1600 but 400 - you are in troube.
//Problem is absolutely epic force created by suspension spring when it's full compressed, stretched or wheel comes underground between frames(most catastrophic situation)
//In all 3 cases your spring will generate crazy force and push rigidbody to the sky.
//so, my solution is find this moment and make spring weaker for a while then return to it's normal condition.

private var baseDistance : float; // need to know distance between wheels - base. It's for wheelie compensate(dont want wheelie for long bikes)

// we need to clamp wheelbar angle according the speed. it means - the faster bike rides the less angle you can rotate wheel bar
var wheelbarRestrictCurve : AnimationCurve = new AnimationCurve(new Keyframe(0f, 20f), new Keyframe(100f, 1f));//first number in Keyframe is speed, second is max wheelbar degree

// temporary variable to restrict wheel angle according speed
private var tempMaxWheelAngle : float;

//variable for cut off wheel bar rotation angle at high speed
private var wheelPossibleAngle : float = 0.0;

//for wheels vusials match up the wheelColliders
private var wheelCCenter : Vector3;
private var hit : RaycastHit;

/////////////////////////////////////////// technical variables ///////////////////////////////////////////////////////
static var bikeSpeed : float; //to know bike speed km/h
static var isReverseOn : boolean = false; //to turn On and Off reverse speed
// Engine
var frontBrakePower : float; //brake power absract - 100 is good brakes																		// 100 sport, 70 chopper
var EngineTorque : float; //engine power(abstract - not in HP or so)																		// 85 sport, 100 chopper
// airRes is for wind resistance to large bikes more than small ones
var airRes : float; //Air resistant 																										// 1 is neutral
/// GearBox
var MaxEngineRPM : float; //engine maximum rotation per minute(RPM) when gearbox should switch to higher gear 								// 12000 sport, 7000 chopper
var EngineRedline : float; 																													// 12200 sport, 7200 chopper
var MinEngineRPM : float; //lowest RPM when gear need to be switched down																	// 6000 sport, 2500 chopper
static var EngineRPM : float; // engine current rotation per minute(RPM)
// gear ratios(abstract)
var GearRatio: float[];//array of gears                                                                                                 	// 6 sport, 5 chopper

var CurrentGear : int = 0; // current gear

private var ctrlHub : GameObject;// gameobject with script control variables 
private var outsideControls : controlHub;// making a link to corresponding bike's script

///////////////////////////////////////////////////  ESP system ////////////////////////////////////////////////////////
private var ESP : boolean = false;//ESP turned off by default


////////////////////////////////////////////////  ON SCREEN INFO ///////////////////////////////////////////////////////
function OnGUI ()
{
	var biggerText = new GUIStyle("label");
  	biggerText.fontSize = 40;
  	var middleText = new GUIStyle("label");
  	middleText.fontSize = 22;
  	var smallerText = new GUIStyle("label");
  	smallerText.fontSize = 14;
  	
  	//to show in on display interface: speed, gear, ESP status
	GUI.color = Color.black;
    GUI.Label(Rect(Screen.width*0.875,Screen.height*0.9, 120, 80), String.Format(""+ "{0:0.}", bikeSpeed), biggerText);
    GUI.Label (Rect (Screen.width*0.76,Screen.height*0.88, 60, 80), "" + (CurrentGear+1),biggerText);
    if (!ESP){
		GUI.color = Color.grey;
		GUI.Label (Rect (Screen.width*0.885, Screen.height*0.86,60,40), "ESP", middleText);
	} else {
		GUI.color = Color.green;
		GUI.Label (Rect (Screen.width*0.885, Screen.height*0.86,60,40), "ESP", middleText);
	}
	 if (!isReverseOn){
		GUI.color = Color.grey;
		GUI.Label (Rect (Screen.width*0.885, Screen.height*0.96,60,40), "REAR", smallerText);
	} else {
		GUI.color = Color.red;
		GUI.Label (Rect (Screen.width*0.885, Screen.height*0.96,60,40), "REAR", smallerText);
	}
    
    // user info help lines
    GUI.color = Color.white;
    GUI.Box (Rect (10,10,180,20), "A,W,S,D - main control", smallerText);
    GUI.Box (Rect (10,25,220,20), "2 - more power to accelerate", smallerText);
    GUI.Box (Rect (10,40,120,20), "X - rear brake", smallerText);
    GUI.Box (Rect (10,55,320,20), "Q,E,F,V - shift center of mass of biker", smallerText);
    GUI.Box (Rect (10,70,320,20), "R - restart / RightShift+R - full restart", smallerText);
    GUI.Box (Rect (10,85,180,20), "RMB - rotate camera around", smallerText);
  	GUI.Box (Rect (10,100,120,20), "Z - turn on/off ESP", smallerText);
  	GUI.Box (Rect (10,115,320,20), "C - toggle reverse", smallerText);
  	GUI.Box (Rect (10,130,320,20), "Esc - return to main menu", smallerText);
  	GUI.color = Color.black; 
  	
  	
}


function Start () {
	
	ctrlHub = GameObject.Find("gameScenario");//link to GameObject with script "controlHub"
	outsideControls = ctrlHub.GetComponent(controlHub);//to connect c# mobile control script to this one

	var setInitialTensor : Vector3 = GetComponent.<Rigidbody>().inertiaTensor;//this string is necessary for Unity 5.3 with new PhysX feature when Tensor decoupled from center of mass
	GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);// now Center of Mass(CoM) is alligned to GameObject "CoM"
	GetComponent.<Rigidbody>().inertiaTensor = setInitialTensor;////this string is necessary for Unity 5.3 with new PhysX feature when Tensor decoupled from center of mass
	
	// wheel colors for understanding of accelerate, idle, brake(white is idle status)
	meshFrontWheel.GetComponent.<Renderer>().material.color = Color.black;
	meshRearWheel.GetComponent.<Renderer>().material.color = Color.black;
	
	//for better physics of fast moving bodies
	GetComponent.<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
	
	// too keep EngineTorque variable like "real" horse powers
	EngineTorque = EngineTorque * 20;
	
	//*30 is for good braking to keep frontBrakePower = 100 for good brakes. So, 100 is like sportsbike's Brembo
	frontBrakePower = frontBrakePower * 30;//30 is abstract but necessary for Unity5
	
	//tehcnical variables
	normalRearSuspSpring = coll_rearWheel.suspensionSpring.spring;
	normalFrontSuspSpring = coll_frontWheel.suspensionSpring.spring;
	
	baseDistance = coll_frontWheel.transform.localPosition.z - coll_rearWheel.transform.localPosition.z;// now we know distance between two wheels
}


function FixedUpdate (){
	
	// if RPM is more than engine can hold we should shift gear up
	EngineRPM = coll_rearWheel.rpm * GearRatio[CurrentGear];
	if (EngineRPM > EngineRedline){
		EngineRPM = MaxEngineRPM;
	}
	ShiftGears();
	// turn ESP on (no stoppie, no wheelie, no falls when brake is on when leaning)
	ESP = outsideControls.ESPMode;

	ApplyLocalPositionToVisuals(coll_frontWheel);
	ApplyLocalPositionToVisuals(coll_rearWheel);
 	
 	
 	//////////////////////////////////// part where rear pendulum, wheelbar and wheels meshes matched to wheelsColliers and so on
 	//beauty - rear pendulumn is looking at rear wheel
 	rearPendulumn.transform.localRotation.eulerAngles.x = 0-8+(meshRearWheel.transform.localPosition.y*100);
 	//beauty - wheel bar rotating by front wheel
	suspensionFront_down.transform.localPosition.y =(meshFrontWheel.transform.localPosition.y - 0.15);
	meshFrontWheel.transform.localPosition.z = meshFrontWheel.transform.localPosition.z - (suspensionFront_down.transform.localPosition.y + 0.4)/5;

	// debug - all wheels are white in idle(no accelerate, no brake)
	meshFrontWheel.GetComponent.<Renderer>().material.color = Color.black;
	meshRearWheel.GetComponent.<Renderer>().material.color = Color.black;
	
	// drag and angular drag for emulate air resistance
	if (!crashed){
		GetComponent.<Rigidbody>().drag = GetComponent.<Rigidbody>().velocity.magnitude / 210  * airRes; // when 250 bike can easy beat 200km/h // ~55 m/s
		GetComponent.<Rigidbody>().angularDrag = 7 + GetComponent.<Rigidbody>().velocity.magnitude/20;
	}
	
	//determinate the bike speed in km/h
	bikeSpeed = Mathf.Round((GetComponent.<Rigidbody>().velocity.magnitude * 3.6)*10) * 0.1; //from m/s to km/h

//////////////////////////////////// acceleration & brake /////////////////////////////////////////////////////////////
//////////////////////////////////// ACCELERATE /////////////////////////////////////////////////////////////
		if (!crashed && outsideControls.Vertical >0 && !isReverseOn){//case with acceleration from 0.0 to 0.9 throttle
			coll_frontWheel.brakeTorque = 0;//we need that to fix strange unity bug when bike stucks if you press "accelerate" just after "brake".
			coll_rearWheel.motorTorque = EngineTorque * outsideControls.Vertical;

			// debug - rear wheel is green when accelerate
			meshRearWheel.GetComponent.<Renderer>().material.color = Color.green;
			
			// when normal accelerating CoM z is averaged
			CoM.localPosition.z = 0.0 + tmpMassShift;
			CoM.localPosition.y = normalCoM;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		//case for reverse
		if (!crashed && outsideControls.Vertical >0 && isReverseOn){
			coll_rearWheel.motorTorque = EngineTorque * -outsideControls.Vertical/10+(bikeSpeed*50);//need to make reverse really slow

			// debug - rear wheel is green when accelerate
			meshRearWheel.GetComponent.<Renderer>().material.color = Color.green;
			
			// when normal accelerating CoM z is averaged
			CoM.localPosition.z = 0.0 + tmpMassShift;
			CoM.localPosition.y = normalCoM;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		
//////////////////////////////////// ACCELERATE full throttle ///////////////////////////////////////////////////////
		if (!crashed && outsideControls.Vertical >0.9 && !isReverseOn){// acceleration >0.9 throttle for wheelie	
			coll_frontWheel.brakeTorque = 0;//we need that to fix strange unity bug when bike stucks if you press "accelerate" just after "brake".
			coll_rearWheel.motorTorque = EngineTorque * 1.2;//1.2 mean it's full throttle
			meshRearWheel.GetComponent.<Renderer>().material.color = Color.green;
			GetComponent.<Rigidbody>().angularDrag  = 20;//for wheelie stability
			
		
			if (!ESP){// when ESP we turn off wheelie
				CoM.localPosition.z = -(2-baseDistance/1.4) + tmpMassShift;// we got 1 meter in case of sportbike: 2-1.4/1.4 = 1; When we got chopper we'll get ~0.8 as result
				//still working om best wheelie code
				var stoppieEmpower : float = (bikeSpeed/3)/100;
				// need to supress wheelie when leaning because it's always fall and it't not fun at all
				if (this.transform.localEulerAngles.z < 70){	
					var angleLeanCompensate = this.transform.localEulerAngles.z/30;
						if (angleLeanCompensate > 0.5){
							angleLeanCompensate = 0.5;
						}
				}
				if (this.transform.localEulerAngles.z > 290){
					angleLeanCompensate = (360-this.transform.localEulerAngles.z)/30;
						if (angleLeanCompensate > 0.5){
							angleLeanCompensate = 0.5;
						}
				}
					
				if (stoppieEmpower + angleLeanCompensate > 0.5){
					stoppieEmpower = 0.5;
				}
				CoM.localPosition.y =  -(1 - baseDistance/2.8) - stoppieEmpower ;
				CoM.localPosition.y = CoM.localPosition.y += 0.002;
			}
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		
			//this is attenuation for rear suspension targetPosition
			//I've made it to prevent very strange launch to sky when wheelie in new Phys3
			if (this.transform.localEulerAngles.x > 200.0){
				coll_rearWheel.suspensionSpring.spring = normalRearSuspSpring + (360-this.transform.localEulerAngles.x)*500;
				if (coll_rearWheel.suspensionSpring.spring >= normalRearSuspSpring + 20000) coll_rearWheel.suspensionSpring.spring = normalRearSuspSpring + 20000;
			}
		} else RearSuspensionRestoration();
		
		
//////////////////////////////////// BRAKING /////////////////////////////////////////////////////////////
//////////////////////////////////// front brake /////////////////////////////////////////////////////////
		if (!crashed && outsideControls.Vertical <0 && !isFrontWheelInAir){

			coll_frontWheel.brakeTorque = frontBrakePower * -outsideControls.Vertical;
			coll_rearWheel.motorTorque = 0; // you can't do accelerate and braking same time.
			
			//more user firendly gomeotric progession braking. But less stoppie and fun :( Boring...
			//coll_frontWheel.brakeTorque = frontBrakePower * -outsideControls.Vertical-(1 - -outsideControls.Vertical)*-outsideControls.Vertical;
			
			if (!ESP){ // no stoppie when ESP is on
				if (bikeSpeed >1){// no CoM pull up when speed is zero
					
					//when rear brake is used it helps a little to prevent stoppie. Because in real life bike "stretch" a little when you using rear brake just moment before front.
					if(outsideControls.rearBrakeOn){
						var rearBrakeAddon = 0.0025;
					}
					CoM.localPosition.y = CoM.localPosition.y += (frontBrakePower/200000)+tmpMassShift/50-rearBrakeAddon;
					
					CoM.localPosition.z = CoM.localPosition.z += 0.05;
				} 	
					else if (bikeSpeed <=1 && !crashed && this.transform.localEulerAngles.z < 45 || bikeSpeed <=1 && !crashed && this.transform.localEulerAngles.z >315){
							if (this.transform.localEulerAngles.x < 5 || this.transform.localEulerAngles.x > 355){
								CoM.localPosition.y = normalCoM;
							}
						}
		
				if (CoM.localPosition.y >= -0.1) CoM.localPosition.y = -0.1;
				
				if (CoM.localPosition.z >= 0.2+(GetComponent.<Rigidbody>().mass/1100)) CoM.localPosition.z = 0.2+(GetComponent.<Rigidbody>().mass/1100);
				
				//////////// 
				//this is attenuation for front suspension when forge spring is compressed
				//I've made it to prevent very strange launch to sky when wheelie in new Phys3
				//problem is launch bike to sky when spring must expand from compressed state. In real life front forge can't create such force.
				var maxFrontSuspConstrain : float;//temporary variable to make constrain for attenuation ususpension(need to make it always ~15% of initial force) 
				maxFrontSuspConstrain = CoM.localPosition.z;
				if (maxFrontSuspConstrain >= 0.5) maxFrontSuspConstrain = 0.5;
				var springWeakness : int  = normalFrontSuspSpring-(normalFrontSuspSpring*1.5) * maxFrontSuspConstrain;
				
			}
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
			// debug - wheel is red when braking
			meshFrontWheel.GetComponent.<Renderer>().material.color = Color.red;
			
			//we need to mark suspension as very compressed to make it weaker
			forgeBlocked = true;
		} else FrontSuspensionRestoration(springWeakness);//here is function for weak front spring and return it's force slowly
			
		
//////////////////////////////////// rear brake /////////////////////////////////////////////////////////
		// rear brake - it's all about lose side stiffness more and more till rear brake is pressed
		if (!crashed && outsideControls.rearBrakeOn){
			coll_rearWheel.brakeTorque = frontBrakePower / 2;// rear brake is not so good as front brake
			
			if (this.transform.localEulerAngles.x > 180 && this.transform.localEulerAngles.x < 350){
				CoM.localPosition.z = 0.0 + tmpMassShift;
			}
			
			coll_frontWheel.sidewaysFriction.stiffness = 1.0 - ((stiffPowerGain/2)-tmpMassShift*3);
			
			stiffPowerGain = stiffPowerGain += 0.025 - (bikeSpeed/10000);
				if (stiffPowerGain > 0.9 - bikeSpeed/300){ //orig 0.90
					stiffPowerGain = 0.9 - bikeSpeed/300;
				}
			coll_rearWheel.sidewaysFriction.stiffness = 1.0 - stiffPowerGain;
			meshRearWheel.GetComponent.<Renderer>().material.color = Color.red;
			
		} else{

			coll_rearWheel.brakeTorque = 0;

			stiffPowerGain = stiffPowerGain -= 0.05;
				if (stiffPowerGain < 0){
					stiffPowerGain = 0;
				}
			coll_rearWheel.sidewaysFriction.stiffness = 1.0 - stiffPowerGain;// side stiffness is back to 2
			coll_frontWheel.sidewaysFriction.stiffness = 1.0 - stiffPowerGain;// side stiffness is back to 1
			
		}
		
//////////////////////////////////// reverse /////////////////////////////////////////////////////////
		if (!crashed && outsideControls.reverse && bikeSpeed <=0){
				outsideControls.reverse = false;
				if(isReverseOn == false){
				isReverseOn = true;
				} else isReverseOn = false;
		}
			
		
//////////////////////////////////// turnning /////////////////////////////////////////////////////////////			
			// there is MOST trick in the code
			// the Unity physics isn't like real life. Wheel collider isn't round as real bike tyre.
			// so, face it - you can't reach accurate and physics correct countersteering effect on wheelCollider
			// For that and many other reasons we restrict front wheel turn angle when when speed is growing
			//(honestly, there was a time when MotoGP bikes has restricted wheel bar rotation angle by 1.5 degree ! as we got here :)			
			tempMaxWheelAngle = wheelbarRestrictCurve.Evaluate(bikeSpeed);//associate speed with curve which you've tuned in Editor
		
		if (!crashed && outsideControls.Horizontal !=0){	
		//if (!crashed && Input.GetAxis("Horizontal") !=0){//DEL OLD
			// while speed is high, wheelbar is restricted 
			
			coll_frontWheel.steerAngle = tempMaxWheelAngle * outsideControls.Horizontal;
			//coll_frontWheel.steerAngle = tempMaxWheelAngle * Input.GetAxis("Horizontal");//DEL OLD
			steeringWheel.rotation = coll_frontWheel.transform.rotation * Quaternion.Euler (0, coll_frontWheel.steerAngle, coll_frontWheel.transform.rotation.z);
		} else coll_frontWheel.steerAngle = 0;
		
		
/////////////////////////////////////////////////// PILOT'S MASS //////////////////////////////////////////////////////////
// it's part about moving of pilot's center of mass. It can be used for wheelie or stoppie control and for motocross section in future
		//not polished yet. For mobile version it should back pilot's mass smooth not in one tick
		if (outsideControls.VerticalMassShift >0){
			tmpMassShift = outsideControls.VerticalMassShift/12.5;//12.5 to get 0.08m at final
			CoM.localPosition.z = tmpMassShift;	
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		if (outsideControls.VerticalMassShift <0){
			tmpMassShift = outsideControls.VerticalMassShift/12.5;//12.5 to get 0.08m at final
			CoM.localPosition.z = tmpMassShift;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		if (outsideControls.HorizontalMassShift <0){
			CoM.localPosition.x = outsideControls.HorizontalMassShift/40;//40 to get 0.025m at final
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
			
		}
		if (outsideControls.HorizontalMassShift >0){
			CoM.localPosition.x = outsideControls.HorizontalMassShift/40;//40 to get 0.025m at final
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		
		
		//auto back CoM when any key not pressed
		if (!crashed && outsideControls.Vertical == 0 && !outsideControls.rearBrakeOn || (outsideControls.Vertical < 0 && isFrontWheelInAir)){
			CoM.localPosition.y = normalCoM;
			CoM.localPosition.z = 0.0 + tmpMassShift;
			coll_frontWheel.motorTorque = 0;
			coll_frontWheel.brakeTorque = 0;
			coll_rearWheel.motorTorque = 0;
			coll_rearWheel.brakeTorque = 0;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		//autoback pilot's CoM along
		if (outsideControls.VerticalMassShift == 0){
			CoM.localPosition.z = 0.0;
			tmpMassShift = 0.0;
		}
		//autoback pilot's CoM across
		if (outsideControls.HorizontalMassShift == 0){
			CoM.localPosition.x = 0.0;
		}
		
/////////////////////////////////////////////////////// RESTART KEY ///////////////////////////////////////////////////////////
		// Restart key - recreate bike few meters above current place
		if (outsideControls.restartBike){
			if (outsideControls.fullRestartBike){
				transform.position = Vector3(0,1,-11);
				transform.rotation=Quaternion.Euler( 0.0, 0.0, 0.0 );
			}
			crashed = false;
			transform.position+=Vector3(0,0.1,0);
			transform.rotation=Quaternion.Euler( 0.0, transform.localEulerAngles.y, 0.0 );
			GetComponent.<Rigidbody>().velocity=Vector3.zero;
			GetComponent.<Rigidbody>().angularVelocity=Vector3.zero;
			CoM.localPosition.x = 0.0;
			CoM.localPosition.y = normalCoM;
			CoM.localPosition.z = 0.0;
			//for fix bug when front wheel IN ground after restart(sorry, I really don't understand why it happens);
			coll_frontWheel.motorTorque = 0;
			coll_frontWheel.brakeTorque = 0;
			coll_rearWheel.motorTorque = 0;
			coll_rearWheel.brakeTorque = 0;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		
		
		
///////////////////////////////////////// CRASH happens /////////////////////////////////////////////////////////
	// conditions when crash is happen
	if ((this.transform.localEulerAngles.z >=crashAngle01 && this.transform.localEulerAngles.z <=crashAngle02) || (this.transform.localEulerAngles.x >=crashAngle03 && this.transform.localEulerAngles.x <=crashAngle04)){
		GetComponent.<Rigidbody>().drag = 0.1; // when 250 bike can easy beat 200km/h // ~55 m/s
		GetComponent.<Rigidbody>().angularDrag = 0.01;
		crashed = true;
		CoM.localPosition.x = 0.0;
		CoM.localPosition.y = CoMWhenCrahsed;//move CoM a little bit up for funny bike rotations when fall
		CoM.localPosition.z = 0.0;
		GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
	}
	
	if (crashed) coll_rearWheel.motorTorque = 0;//to prevent some bug when bike crashed but still accelerating
}

//function Update () {
	//not use that because everything here is about physics
//}
///////////////////////////////////////////// FUNCTIONS /////////////////////////////////////////////////////////
function ShiftGears() {
	if ( EngineRPM >= MaxEngineRPM ) {
		var AppropriateGear : int = CurrentGear;
		
		for ( var i = 0; i < GearRatio.length; i ++ ) {
			if (coll_rearWheel.rpm * GearRatio[i] < MaxEngineRPM ) {
				AppropriateGear = i;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
	
	if ( EngineRPM <= MinEngineRPM ) {
		AppropriateGear = CurrentGear;
		
		for ( var j = GearRatio.length-1; j >= 0; j -- ) {
			if (coll_rearWheel.rpm * GearRatio[j] > MinEngineRPM ) {
				AppropriateGear = j;
				break;
			}
		}
		CurrentGear = AppropriateGear;
	}
}
	
function ApplyLocalPositionToVisuals (collider : WheelCollider) {
		if (collider.transform.childCount == 0) {
			return;
		}
		
		var visualWheel : Transform = collider.transform.GetChild (0);
		wheelCCenter = collider.transform.TransformPoint (collider.center);	
		if (Physics.Raycast (wheelCCenter, -collider.transform.up, hit, collider.suspensionDistance + collider.radius)) {
			visualWheel.transform.position = hit.point + (collider.transform.up * collider.radius);
			if (collider.name == "coll_front_wheel") isFrontWheelInAir = false;
			
		} else {
			visualWheel.transform.position = wheelCCenter - (collider.transform.up * collider.suspensionDistance);
			if (collider.name == "coll_front_wheel") isFrontWheelInAir = true;
		}
		var position : Vector3;
		var rotation : Quaternion;
		collider.GetWorldPose (position, rotation);

		visualWheel.localEulerAngles = Vector3(visualWheel.localEulerAngles.x, collider.steerAngle - visualWheel.localEulerAngles.z, visualWheel.localEulerAngles.z);
		visualWheel.Rotate (collider.rpm / 60 * 360 * Time.deltaTime, 0, 0);

}
//need to restore spring power for rear suspension after make it harder for wheelie
function RearSuspensionRestoration (){
	if (coll_rearWheel.suspensionSpring.spring > normalRearSuspSpring){
		coll_rearWheel.suspensionSpring.spring = coll_rearWheel.suspensionSpring.spring -= 500;
	}
}
//need to restore spring power for front suspension after make it weaker for stoppie
function FrontSuspensionRestoration (sprWeakness : int){
	if (forgeBlocked) {//supress front spring power to avoid too much force back
		coll_frontWheel.suspensionSpring.spring = sprWeakness;
		forgeBlocked = false;
	}
	if (coll_frontWheel.suspensionSpring.spring < normalFrontSuspSpring){//slowly returning force to front spring
		coll_frontWheel.suspensionSpring.spring = coll_frontWheel.suspensionSpring.spring += 500;
	}
}