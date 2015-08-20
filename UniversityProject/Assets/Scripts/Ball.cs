using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	private bool isLanded=false;
	public Rigidbody2D rigidbody2d;
	 void Start () {
		rigidbody2d.velocity=new Vector3 (0,-3,0);
	}

	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision2D){
		rigidbody2d.gravityScale = 3;
		if ((collision2D.gameObject.tag == "Floor" || collision2D.gameObject.tag == "Ball") && isLanded==false ) {
			this.isLanded=true;
			GameGrid.InsertBallToGrid(this);


		}


	}
}
