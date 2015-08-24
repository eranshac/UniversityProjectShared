using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	private bool isLanded=false;
	public Bar bar;
	private int xPosition=9999;
	private int yPosition=9999;

	public Rigidbody2D rigidbody2d;
	 float speed;
	
	private Vector4 ballColor;
	 void Start () {
		rigidbody2d.velocity=new Vector3 (0,-3,0);
	
		ballColor = GetComponent<SpriteRenderer>().color;
	}

	void Update () {
	
		if (isLanded == false) {
			float currentPitch=Controller.x ;
			speed=0;
			//print ("currentPitch: "+ currentPitch+" speed: "+speed);

			if (currentPitch> 0) {
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


	void OnCollisionEnter2D(Collision2D collision2D){
		rigidbody2d.gravityScale = 3;
		if ((collision2D.gameObject.tag == "Floor" || collision2D.gameObject.tag == "Ball")) {
			print (currentPositionIsDifferentFromPreviousPosition());
			if( currentPositionIsDifferentFromPreviousPosition() && isLanded==true)
			{
				GameGrid.SetNullToPreviousPositionOfBall(xPosition,yPosition);
			}
			this.isLanded=true;

			UpdateTheBallsXandY(collision2D);
			GameGrid.InsertBallToGrid(this);
		}

	}

	bool currentPositionIsDifferentFromPreviousPosition ()
	{
		bool isXValueDifferent= ((int)GameGrid.GetCurrentBallPosition (this).x!=xPosition);
		bool isYValueDifferent= ((int)GameGrid.GetCurrentBallPosition (this).y!=yPosition);

		return isXValueDifferent && isYValueDifferent;
	}

	void UpdateTheBallsXandY (Collision2D collision2D)
	{
		xPosition = (int)GameGrid.GetCurrentBallPosition (this).x;
		yPosition = (int)GameGrid.GetCurrentBallPosition (this).y;

	}
	
	public Vector4 GetBallColor(){
	return ballColor;
	
	}
	


	
}
