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
		if (flask.gameObject.tag =="SettingFlask") {
			Application.LoadLevel("MainMenu");
		} 
		if(flask.gameObject.tag =="PlayFlask"){
			Application.LoadLevel ("GAME");
		}


	}
}
