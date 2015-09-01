using UnityEngine;
using System.Collections;

public class FloorTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D flask){
		print (flask.gameObject.tag);
		if (flask.gameObject.tag =="MoreInformationFlask" && Application.loadedLevelName == "MainMenu") {
			Application.LoadLevel("Options");
		} 
		if(flask.gameObject.tag =="PlayFlask"){
			Application.LoadLevel ("GAME");
		}
		if(flask.gameObject.tag =="BackButton" && Application.loadedLevelName == "Options" ){
			Application.LoadLevel ("MainMenu");
		}
		if(flask.gameObject.tag =="BackButton" && Application.loadedLevelName == "Settings" ){
			Application.LoadLevel ("Options");
		}
		if(flask.gameObject.tag =="SettingFlask" && Application.loadedLevelName == "Options" ){
			Application.LoadLevel ("Settings");
		}

	/*	if(flask.gameObject.tag ==""){
			Application.LoadLevel ("");
		}
		if(flask.gameObject.tag ==""){
			Application.LoadLevel ("");
		}
		if(flask.gameObject.tag ==""){
			Application.LoadLevel ("");
		}*/


	}
}
