#pragma strict

var theBullet : Rigidbody;
var Speed = 10;
var rotation = Quaternion.Euler(0, -90, 0);

function Start() {
}
function Update () {
	if (Input.GetMouseButtonDown(0))
	{
		var clone = Instantiate(theBullet, transform.position, rotation);
		clone.velocity = transform.TransformDirection(Vector3(0, 2, -2));
		
		Destroy (clone.gameObject, 3);
	}
}