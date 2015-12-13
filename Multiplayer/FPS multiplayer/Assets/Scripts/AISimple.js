var Distance;
var Target : Transform;
var lookAtDistance = 25.0;
var attackRange = 15.0;
var moveSpeed = 5.0;
var Damping = 6.0;

function Update ()
{
	Distance = Vector3.Distance(Target.position, transform.position);
	
	if (Distance < lookAtDistance)
	{
		GetComponent.<Renderer>().material.color = Color.yellow;
		lookAt();
	}
	
	if (Distance > lookAtDistance)
	{
		GetComponent.<Renderer>().material.color = Color.green;
	}
	
	if (Distance < attackRange)
	{
		GetComponent.<Renderer>().material.color = Color.red;
		attack ();
	}
}

function lookAt ()
{
	var rotation = Quaternion.LookRotation(Target.position - transform.position);
	transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
}

function attack ()
{
	transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
}