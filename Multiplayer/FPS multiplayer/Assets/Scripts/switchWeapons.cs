using UnityEngine;
using System.Collections;

public class switchWeapons : MonoBehaviour {

	public GameObject Weapon01;
	public GameObject Weapon02;
	public GameObject Weapon03;
	
	
	void Start () {
		Weapon01.SetActive (true);
		Weapon02.SetActive (false);
		Weapon03.SetActive (false);
	}
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			switchingWeapons();
		}
	}
	void switchingWeapons(){
		if (Weapon01.activeSelf) {
			Weapon01.SetActive (false);
			Weapon02.SetActive (true);
			Weapon03.SetActive (false);
			
		} 
		else if (Weapon02.activeSelf) {
			Weapon01.SetActive (false);
			Weapon02.SetActive (false);
			Weapon03.SetActive (true);
			
		} 
		else {
			Weapon01.SetActive (true);
			Weapon02.SetActive (false);
			Weapon03.SetActive (false);
			
		} 

	}
}
