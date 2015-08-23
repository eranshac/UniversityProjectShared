﻿using UnityEngine;
using System.Collections;

public class Spwaner : MonoBehaviour {
	public Ball preFarbBall;
	private Vector4[] colorsArray;
	private static int countCallForSpwan=0;
	void Start () {
		colorsArray = new Vector4[5];
		colorsArray [0] = new Vector4 (1, 0.92f, 0.016f, 1);
		colorsArray [1] = new Vector4 (1, 0, 0, 1);
		colorsArray [2] = new Vector4 (0, 1, 0, 1);
		colorsArray [3] = new Vector4 (0.5f, 0.5f, 0.5f, 1);
		colorsArray [4] = new Vector4 (0, 1, 1, 0);
		           
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void RandomlySpwanBalls(){
		if(Time.timeSinceLevelLoad>2){
			
		if(GameGrid.GetfoundSequence()){
		
			countCallForSpwan++;
		
			Invoke("Spwan",0.5f);
			
			
			}else{
			
			Spwan();
			}
		}
		

	}
	public void Spwan(){
		//print("countCallForSpwan " + countCallForSpwan);
		if (countCallForSpwan==1 || GameGrid.GetfoundSequence()==false){
			int rand;
			rand = Random.Range (0, 11);
			
			Transform origin = this.gameObject.transform.GetChild (rand);
			
			Ball ball = (Ball) Instantiate (preFarbBall, origin.position, Quaternion.identity);
			ball.GetComponent<SpriteRenderer> ().color = colorsArray[Random.Range(0,4)];
			ball.transform.parent = origin.transform;
			
				GameGrid.SetfoundSequence(false);
				GameGrid.ChangeAllBallsLandBool(true,-1);
			countCallForSpwan=0;
			
		}
		if(GameGrid.GetfoundSequence()==true){
			countCallForSpwan--;
		}
	}
}
