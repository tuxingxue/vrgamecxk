//speedometer
var GUIDashboard : Texture2D;
var dashboardArrow : Texture2D;
private var topSpeed: float;//220 for sport/ 180 for chopper
private var stopAngle: float;//-200 for sport/ ... for chopper
private var topSpeedAngle: float;
var speed: float;

//tachometer
var chronoTex: Texture2D;
private var topRPM: float;// 14000 for sport/ ... for chopper
private var stopRPMAngle: float;//-200 for sport/... ... for chopper
private var topRPMAngle: float;
var RPM: float;

//link to bike script
var linkToBike : pro_bike5;


function Start () {
	linkToBike = GameObject.Find("rigid_bike").GetComponent("pro_bike5");
 	findCurrentBike();
}


function OnGUI() {
	// speedometer
	GUI.DrawTexture(Rect(Screen.width*0.85, Screen.height*0.8, GUIDashboard.width/2, GUIDashboard.height/2), GUIDashboard);
	var centre = Vector2(Screen.width*0.85 + GUIDashboard.width / 4, Screen.height*0.8 + GUIDashboard.height / 4);
	var savedMatrix = GUI.matrix;
	var speedFraction = speed / topSpeed;
	var needleAngle = Mathf.Lerp(stopAngle, topSpeedAngle, speedFraction);
	GUIUtility.RotateAroundPivot(needleAngle, centre);
	GUI.DrawTexture(Rect(centre.x, centre.y - dashboardArrow.height/4, dashboardArrow.width/2, dashboardArrow.height/2), dashboardArrow);
	GUI.matrix = savedMatrix;
	
	//tachometer
	GUI.DrawTexture(Rect(Screen.width*0.70, Screen.height*0.7, chronoTex.width/1.5, chronoTex.height/1.5), chronoTex);
	var centreTacho = Vector2(Screen.width*0.70 + chronoTex.width / 3, Screen.height*0.7 + chronoTex.height / 3);
	var savedTachoMatrix = GUI.matrix;
	var tachoFraction = RPM / topRPM;
	var needleTachoAngle = Mathf.Lerp(stopRPMAngle, topRPMAngle, tachoFraction);
	GUIUtility.RotateAroundPivot(needleTachoAngle, centreTacho);
	GUI.DrawTexture(Rect(centreTacho.x, centreTacho.y - dashboardArrow.height/3, dashboardArrow.width/1.5, dashboardArrow.height/1.5), dashboardArrow);
	GUI.matrix = savedTachoMatrix;
}
function FixedUpdate(){
 	speed = linkToBike.bikeSpeed;
 	RPM = linkToBike.EngineRPM;
}
function findCurrentBike(){
	var curBikeName : GameObject;
	curBikeName = GameObject.Find("rigid_bike");
	if (curBikeName != null){
		SetSpeedometerSettings("sport");
		return;
	}
	
}
function SetSpeedometerSettings(par : String){
	if (par == "sport"){
		topSpeed = 210;
		stopAngle = -215;
		topSpeedAngle = 0;
		topRPM = 12000;
		stopRPMAngle = -200;
		topRPMAngle = 0;
		yield WaitForSeconds(0.5);	
		var linkToBike1 = GameObject.Find("rigid_bike").GetComponent("pro_bike5");
		linkToBike = linkToBike1;
	}
}