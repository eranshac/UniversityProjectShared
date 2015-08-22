using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	private bool isLanded=false;
	public Bar bar;

	public Rigidbody2D rigidbody2d;
	 float speed;
	private Text BallPitchDisplayToScreen;
	private Vector4 ballColor;
	 void Start () {
		rigidbody2d.velocity=new Vector3 (0,-3,0);
		BallPitchDisplayToScreen = (Text)GameObject.FindGameObjectWithTag ("SpeedText").GetComponent<Text>();
		ballColor = GetComponent<SpriteRenderer>().color;
	}

	void Update () {
	
		if (isLanded == false) {
			float currentPitch=Controller.x ;
			speed=0;
			//print ("currentPitch: "+ currentPitch+" speed: "+speed);

			if (currentPitch> 0) {
				speed = (currentPitch - 220) / 15;
				DisplayOnScreen ( currentPitch);

			
			}
		
			float WidthOfBar = bar.GetComponent<BoxCollider2D> ().size.x;
			transform.position += Vector3.left * speed * Time.deltaTime;
		
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, 0 + WidthOfBar, 23.8f), transform.position.y, transform.position.z);
		}
	
	}
	void DisplayOnScreen ( float x)
	{
		BallPitchDisplayToScreen.text=x.ToString();
	}

	void OnCollisionEnter2D(Collision2D collision2D){
		GameGrid.UpdatetimeFromLastCollision(Time.deltaTime+Time.timeSinceLevelLoad);
		rigidbody2d.gravityScale = 3;
			print(Time.deltaTime);
		if ((collision2D.gameObject.tag == "Floor" || collision2D.gameObject.tag == "Ball") && isLanded==false ) {
			this.isLanded=true;			
			GameGrid.InsertBallToGrid(this);
		}

	}
	
	public Vector4 GetBallColor(){
	
	return ballColor;
	
	}
	
	public void UnLandBall(bool changeTo){
		isLanded=changeTo;
	
	}
	public bool GetIsLanded(){
	return isLanded;
	}

	
}
