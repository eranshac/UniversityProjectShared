using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetUserPitch : MonoBehaviour {
	private float time;
	private Text HigestPitchText,LowestPitchText;
	private float highestPitch=0,lowestpitch=1000;
	private bool checkHigest = false,checkLowest=false;
	public Slider highestPitchSlider;
	public Slider lowestPitchSlider;
	private float currentTimeForSlider;
	// Use this for initialization
	
	
	
	public void CheckHighestPitch(){
		highestPitchSlider.value=0;
		currentTimeForSlider=Time.timeSinceLevelLoad;
		checkHigest=true;
		Invoke("Stop",5);
	}
	public void CheckLowestPitch(){
		lowestPitchSlider.value=0;
		currentTimeForSlider=Time.timeSinceLevelLoad;
		checkLowest=true;
		Invoke("Stop",5);
	}
	
	public void Stop(){
	
	PlayerPrefsManager.SetHighestPitch(highestPitch);
		PlayerPrefsManager.SetLowhestPitch(lowestpitch);
	checkHigest=false;
		checkLowest=false;
	
	}
	
	
	void Start () {
		HigestPitchText = (Text) GameObject.Find("HigestPitchText").GetComponent<Text>();
		LowestPitchText = (Text) GameObject.Find("LowestPitchText").GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(checkHigest){
			highestPitchSlider.value=(Time.timeSinceLevelLoad-currentTimeForSlider)/5;
			
		if(Controller.x>highestPitch)
			highestPitch=Controller.x;
			}
			
		if(checkLowest){
			lowestPitchSlider.value=(Time.timeSinceLevelLoad-currentTimeForSlider)/5;
			
			if(Controller.x>0 && Controller.x<lowestpitch)
				lowestpitch=Controller.x;
		}
			
			
			
			
		print("Controller.x " + Controller.x); 
		HigestPitchText.text="Highest Pitch :" + highestPitch.ToString();
		LowestPitchText.text="Lowest Pitch : " + lowestpitch.ToString();
	
	
	}
	

}
