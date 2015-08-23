using UnityEngine;
using System.Collections;

public class FloorTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D flask){
		if (flask.gameObject.tag =="SettingFlask") {
			Application.LoadLevel("MainMenu");


		} else {
			Application.LoadLevel ("Game");

		}


	}
}
