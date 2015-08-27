using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	private bool isLanded=false ,isCollided=false;
	public Bar bar;
	private int xPosition=9999;
	private int yPosition=9999;
	private bool moveBallWithVoice=false;
	private LevelManager levleManager;
	public Rigidbody2D rigidbody2d;
	public float middlePitch=240;
	public float restrictedSpeedValue=20;
	public bool constantBallSpeed=true;
	private bool test=false;
	private bool hasMadeSuctionSound=false;
	SuctionSoundFX suctionSound;
	GameObject soundToDestroy;
	 float speed;
	
	private Vector4 ballColor;
	 void Start () {
	//	rigidbody2d.rigidbody.freezeRotation;
	
	
		rigidbody2d.velocity=new Vector3 (0,-3 -Time.timeSinceLevelLoad*0.01f ,0);
		ballColor = GetComponent<SpriteRenderer>().color;
		levleManager = FindObjectOfType<LevelManager>();
	}
	void OnDestroy() {
	
		GameGrid.destroyd++;
	}
	void Update () {
		if (isCollided == false) {
			float WidthOfBar = bar.GetComponent<BoxCollider2D> ().size.x;

			if(moveBallWithVoice){
				float currentPitch=Controller.x ;
				
				speed=0;
				if (currentPitch> 0 ) {
					if(constantBallSpeed){
						speed=-1*restrictedSpeedValue*Mathf.Sign((currentPitch - middlePitch));
					}
					else{
						speed = -1*(currentPitch - middlePitch) / 10;
						speed=Mathf.Clamp(speed,0,restrictedSpeedValue);

					}

				}
				transform.position += Vector3.left * speed * Time.deltaTime;
			}else
				if(Input.GetKey(KeyCode.RightArrow)){
					transform.position += Vector3.right * 20 * Time.deltaTime;
				}
				if(Input.GetKey(KeyCode.LeftArrow)){
					
					transform.position += Vector3.left * 20 * Time.deltaTime;
				}
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, 0 + WidthOfBar, 23.5f), transform.position.y, transform.position.z);
		}

	
	}
	
	

	void UpdateArray(){
	
		if(isLanded==true)
		{
			
			GameGrid.SetNullToPreviousPositionOfBall(xPosition,yPosition);
		}else {
		
		GameGrid.AddPoints(10);
		
		}
		this.isLanded=true;
		
		UpdateTheBallsXandY();
		
		
	
		GameGrid.InsertBallToGrid(this);
	
		
	
	}
	void OnCollisionEnter2D(Collision2D collision2D){
		if(isLanded==false){
			GameGrid.pointsNumOfSequanceCoefficient = 0;
		
		}
		
		if (LayerMask.LayerToName (collision2D.gameObject.layer) == "Flask") {
			gameObject.GetComponent<Rigidbody2D>().gravityScale=7;
			collision2D.gameObject.GetComponent<BoxCollider2D>().enabled=false;
		}

		if (gameObject.tag != "menuBall")
			isCollided=true;

		if (LayerMask.LayerToName (collision2D.gameObject.layer) != "Flask" && !hasMadeSuctionSound ) {
			rigidbody2d.gravityScale = 4;

			if( Application.loadedLevelName=="GAME"){

				float delayInSound=0;
				print (Application.loadedLevelName);
				if(transform.position.y>11.93){
					delayInSound=0.55f;
				}
			MakeSuctionSound(delayInSound);

			hasMadeSuctionSound=true;
			}
		}
		
		
		if ((collision2D.gameObject.tag == "Floor" || collision2D.gameObject.tag == "Ball")) {
			
			UpdateArray();
			
			
			int yCurrentPos=	(int)GameGrid.GetCurrentBallPosition (this).y;
			
		
			
			if(yCurrentPos==6){
			LoseTheGame();
			
			}
			
			int i=1;
			int xCurrentPos = (int)GameGrid.GetCurrentBallPosition (this).x;
											
			while( yCurrentPos+i+1<GameGrid.GetNumberOfRows() && GameGrid.grid[xCurrentPos,yCurrentPos+i+1]){
			
				GameGrid.grid[xCurrentPos,yCurrentPos+i+1].UpdateArray();
				i++;
				
			}

			
		}
		if (collision2D.gameObject.tag == "Ball") {
			GameObject suctionSoundGarbage=  GameObject.FindGameObjectWithTag ("SuctionSoundGarbage") as GameObject ;
			if(suctionSoundGarbage.transform.childCount>0){
				soundToDestroy=suctionSoundGarbage.transform.GetChild(0).gameObject;
				soundToDestroy.GetComponent<AudioSource>().pitch=2;

			}
		}

		   
	}
	void MakeSuctionSound(float delayInSound){
		suctionSound = Resources.Load<SuctionSoundFX> ("prefabs/SoundPrefabs/SuctionSound");
		GameObject suctionSoundGarbage=  GameObject.FindGameObjectWithTag ("SuctionSoundGarbage") as GameObject ;
		if (!suctionSoundGarbage) {
			suctionSoundGarbage=new GameObject();
			suctionSoundGarbage.tag="SuctionSoundGarbage";
		}
		suctionSound.delayInSound = delayInSound;
		SuctionSoundFX sound=(SuctionSoundFX)Instantiate (suctionSound, new Vector3 (0, 0, 0), Quaternion.identity);
		sound.transform.parent= suctionSoundGarbage.transform;
	}
	bool currentPositionIsDifferentFromPreviousPosition ()
	{
		bool isXValueDifferent= ((int)GameGrid.GetCurrentBallPosition (this).x!=xPosition);
		bool isYValueDifferent= ((int)GameGrid.GetCurrentBallPosition (this).y!=yPosition);

		return isXValueDifferent && isYValueDifferent;
	}

	void UpdateTheBallsXandY ()
	{
		xPosition = (int)GameGrid.GetCurrentBallPosition (this).x;
		yPosition = (int)GameGrid.GetCurrentBallPosition (this).y;

	}
	
	public Vector4 GetBallColor(){
	return ballColor;
	
	}
	
			
	private void LoseTheGame(){
		levleManager.LoadLevel("MainMenu");

	}
	void Destruct(){
		Destroy (soundToDestroy);
	}
	

	
	
}
