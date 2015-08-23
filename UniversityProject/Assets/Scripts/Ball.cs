using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	private bool isLanded=false;
	public Bar bar;

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
			this.isLanded=true;			
			GameGrid.InsertBallToGrid(this);
		}

	}
	
	public Vector4 GetBallColor(){
	
	return ballColor;
	
	}
	


	
}
