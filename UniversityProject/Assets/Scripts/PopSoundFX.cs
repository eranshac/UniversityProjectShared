using UnityEngine;
using System.Collections;

public class PopSoundFX : MonoBehaviour {

	public AudioSource myAudioSource;
	void Start () {
		myAudioSource.Play ();
		Invoke ("DestroySoundFx", myAudioSource.clip.length);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void DestroySoundFx(){
		Destroy (gameObject);
	}
}
