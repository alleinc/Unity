#pragma strict

var grenade : Rigidbody;
var throwPower : float = 10;



function Start () {

}

function Update () {
if (Input.GetButtonDown("Fire2"))
{
var clone: Rigidbody;
clone= Instantiate(grenade, transform.position, transform.rotation);
clone.velocity = transform.TransformDirection(Vector3.forward*throwPower);
}

}