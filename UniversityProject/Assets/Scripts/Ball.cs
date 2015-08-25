using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	private bool isLanded=false,isCollided=false;
	public Bar bar;
	private int xPosition=9999;
	private int yPosition=9999;

	private LevelManager levleManager;
	private bool VoiceIsOn=false;
	public Rigidbody2D rigidbody2d;
	 float speed;
	
	private Vector4 ballColor;
	 void Start () {
		rigidbody2d.velocity=new Vector3 (0,-3,0);
		ballColor = GetComponent<SpriteRenderer>().color;
		levleManager = FindObjectOfType<LevelManager>();
	}

	void Update () {
	
		if (isCollided == false) {
			float currentPitch=Controller.x ;
			speed=0;
	

			if (currentPitch> 0 && VoiceIsOn==true) {
				speed = (currentPitch - 220) / 15;
				

			
			}
		
			float WidthOfBar = bar.GetComponent<BoxCollider2D> ().size.x;
			transform.position += Vector3.left * speed * Time.deltaTime;
			
			if(Input.GetKey(KeyCode.RightArrow)){
				
				transform.position += Vector3.right * 20 * Time.deltaTime;
				
			}
			
			if(Input.GetKey(KeyCode.LeftArrow)){
				
				transform.position += Vector3.left * 20 * Time.deltaTime;
				
			}
		
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, 0 + WidthOfBar, 23.8f), transform.position.y, transform.position.z);
		}
	
	}
	
	

	void UpdateArray(){
	
		if(isLanded==true)
		{
			
			GameGrid.SetNullToPreviousPositionOfBall(xPosition,yPosition);
		}
		this.isLanded=true;
		
		UpdateTheBallsXandY();
		
		
	
		GameGrid.InsertBallToGrid(this);
	
		
	
	}
	void OnCollisionEnter2D(Collision2D collision2D){
	isCollided=true;
		rigidbody2d.gravityScale = 3;
		
		if ((collision2D.gameObject.tag == "Floor" || collision2D.gameObject.tag == "Ball")) {
			print ("collision");
			UpdateArray();
			
			
			int yCurrentPos=	(int)GameGrid.GetCurrentBallPosition (this).y;
			print ("yCurrentPos  " + yCurrentPos);
		
			
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
	
	
}
