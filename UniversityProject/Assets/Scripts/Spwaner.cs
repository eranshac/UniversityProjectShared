using UnityEngine;
using System.Collections;

public class Spwaner : MonoBehaviour {
	public Ball preFarbBall;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void RandomlySpwanBalls(){
		int rand;
		rand = Random.Range (0, 9	);
		Transform origin = this.gameObject.transform.GetChild (rand);
		Ball ball = (Ball) Instantiate (preFarbBall, origin.position, Quaternion.identity);
		ball.transform.parent = origin.transform;
		


	}
}
