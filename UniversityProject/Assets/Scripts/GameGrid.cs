using UnityEngine;
using System.Collections;

public class GameGrid : MonoBehaviour {

	public int numberOfColoumns;
	private static int numberOfPipes=10, NumberOfRowes=8;
	public static Spwaner spwaner;
	private static Ball[,] grid = new Ball[numberOfPipes, NumberOfRowes];

	void Start () {
		spwaner = FindObjectOfType<Spwaner> ();
		GameObject barInstance = GameObject.FindGameObjectWithTag ("Bars");
		numberOfColoumns = barInstance.transform.childCount;
	}

	public static void InsertBallToGrid(Ball ball){

		float ballRadius = ball.GetComponent<CircleCollider2D>().radius;
		float ballXPosition = ball.transform.position.x;
		float GridX = Mathf.Round( (ballXPosition+1)/2 );
		float GridY = Mathf.Round(( ball.transform.position.y )/(ballRadius*2));
		print (new Vector2 (GridX,GridY));
		grid [(int)GridX-1, (int)GridY-1] = ball;
		print (grid [(int)GridX-1, (int)GridY-1]);

		spwaner.RandomlySpwanBalls();
		
		
	}

	private void NextBall(){


	}

	public Spwaner GetSpwaner(){

		return spwaner;
	}

}
