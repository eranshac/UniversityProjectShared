using UnityEngine;
using System.Collections;

public class Spwaner : MonoBehaviour {
	public Ball preFarbBall;
	private Vector4[] colorsArray;
	private static int countCallForSpwan=0;
	private int lastRand=0;

	void Start () {
		colorsArray = new Vector4[5];
		colorsArray [0] = new Vector4 (1, 0.92f, 0.016f, 1);
		colorsArray [1] = new Vector4 (1, 0, 0, 1);
		colorsArray [2] = new Vector4 (0, 1, 0, 1);
		colorsArray [3] = new Vector4 (0.5f, 0.5f, 0.5f, 1);
		colorsArray [4] = new Vector4 (1, 0, 1, 1);
		
		SpwanForStart();
		Invoke("Spwan",3.2f);           
	}
	
	private void SpwanForStart(){
		foreach (Transform child in transform) {
		print ("ok");
			if(Random.value<0.75){
				
				SpwanWithYPos(child,1);
				if(Random.value<0.5){
						SpwanWithYPos(child,3);
						if(Random.value<0.25)
						SpwanWithYPos(child,5);
						
					}
			}
		
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/*
	public void RandomlySpwanBalls(){
		if(Time.timeSinceLevelLoad>2){
			
		if(GameGrid.GetfoundSequence()){
		
			countCallForSpwan++;
		
			Invoke("Spwan",0.5f);
			
			
			}else{
			
			Spwan();
			}
		}
		

	}*/
	public void Spwan(){
		Invoke("Spwan",3.2f);   
		
		
			
		
			
			Transform origin = this.gameObject.transform.GetChild (Random.Range (0, 11));
			
			Ball ball = (Ball) Instantiate (preFarbBall, origin.position, Quaternion.identity);
			
			ball.GetComponent<SpriteRenderer> ().color = colorsArray[Random.Range(0,2)];
		
			//ball.transform.parent = origin.transform;
			
			
		
			
	}	
	
	private void SpwanWithYPos(Transform origin, int y){
	
		Ball ball = (Ball) Instantiate (preFarbBall, new Vector2(origin.position.x,y), Quaternion.identity);
		int rand = Random.Range(0,5);
		while(rand==lastRand)
			rand = Random.Range(0,5);
		ball.GetComponent<SpriteRenderer> ().color = colorsArray[rand];
		lastRand=rand;
		ball.transform.parent = origin.transform;
	
	
	}

	
}
