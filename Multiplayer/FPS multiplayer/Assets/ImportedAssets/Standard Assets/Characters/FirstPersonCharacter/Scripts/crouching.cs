using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class crouching : MonoBehaviour {

	public float crouchSpeed = 2f;
	
	private CharacterController controller;
	private FirstPersonController fpsController;
	
	

	// Use this for initialization
	void Start () {
			controller = GetComponent<CharacterController>();
			fpsController = gameObject.GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
			
			if(Input.GetKeyDown(KeyCode.C)){
				controller.height = 1.8f;
				fpsController.m_WalkSpeed = crouchSpeed;
				fpsController.m_RunSpeed = crouchSpeed;
			}
			if(Input.GetKeyUp (KeyCode.C)) {
				controller.height = 2.5f;
				fpsController.m_WalkSpeed = 5f;
				fpsController.m_RunSpeed = 10f;
				
			}
	}
}
