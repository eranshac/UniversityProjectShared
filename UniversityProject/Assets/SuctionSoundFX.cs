using UnityEngine;
using System.Collections;

public class SuctionSoundFX : MonoBehaviour {

	public AudioSource myAudioSource;
	public float delayInSound;
	void Start () {
		Invoke ("PlaySound", 0.5f+delayInSound);

		
	}
	
	// Update is called once per frame
	void SelfDestruct () {
		Destroy (gameObject);

	}
	void PlaySound(){
		myAudioSource.Play ();

		Invoke ("SelfDestruct", myAudioSource.clip.length);

	}


}
