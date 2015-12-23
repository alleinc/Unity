using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class countDownTimer : MonoBehaviour {

	public float timeRemaining = 5;
	public GameObject canvasText;
	public Text text;

	// Use this for initialization
	void Awake(){
	}
	
	// Update is called once per frame
	void Update () {

		timeRemaining -= Time.deltaTime;
	}
	void OnGUI(){
		int xPos = Screen.width/2;
		if (timeRemaining > 0) {
			GUI.Label (new Rect (xPos, 20, 200, 100), "Time Remaining: " + (int)timeRemaining);
		} 
		else {
			GUI.Label(new Rect(xPos,20,100,100), "Time's up");
		}
	}
}
