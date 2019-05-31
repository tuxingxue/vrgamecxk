#pragma strict
var destoryThisTime = 30;//in seconds
var alphaFade = 0.001;//abstarct -0.001 means slow fade out in ~20 secs
private var skidMatGO : Transform;

function Start () {
	skidMatGO = transform.FindChild ("skidPrefabMesh");
	Destroy (gameObject, destoryThisTime);
}
function FixedUpdate(){
	skidMatGO.gameObject.GetComponent.<Renderer>().material.color.a = skidMatGO.gameObject.GetComponent.<Renderer>().material.color.a -= alphaFade;
}