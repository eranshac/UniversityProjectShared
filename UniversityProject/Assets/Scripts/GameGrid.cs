using UnityEngine;
using System.Collections;
using System;
	

public class GameGrid : MonoBehaviour {

	public int numberOfColoumns;
	private static int numberOfPipes=12, NumberOfRowes=8;
	public static Spwaner spwaner;
	private static int countCallForCheck=0;

	public static Ball[,] grid = new Ball[numberOfPipes, NumberOfRowes];
	private static int x,y;
	private static Vector4 color;
	

	void Update(){
	

	}
	

	void Start () {
		GameObject barInstance = GameObject.FindGameObjectWithTag ("Bars");
		numberOfColoumns = barInstance.transform.childCount;
	}

	private static void CheckForSequence (){
		CheckForColumn();
		CheckForRow();
		CheckForDiagBottomLeft();
		CheckForDiagBottomRight();
		countCallForCheck--;
	}

	public static void InsertBallToGrid(Ball ball){
		Vector2 ballPosition= GetCurrentBallPosition(ball);
		insertBallIntoGrid (ballPosition,ball);
		color=ball.GetBallColor();
		CanCheck();
		
		
	}
	private static void CanCheck(){
		if(countCallForCheck>0){
			
			CanCheck();
		}else{
			countCallForCheck++;
			CheckForSequence();
		}
	}
	public static Vector2 GetCurrentBallPosition (Ball ball)
	{
		float ballRadius = ball.GetComponent<CircleCollider2D>().radius;
		float ballXPosition = ball.transform.position.x;
		float GridX = Mathf.Round( (ballXPosition+1)/2 );
		float GridY = Mathf.Round(( ball.transform.position.y )/(ballRadius*2));
		x=(int)GridX-1;
		y=(int)GridY-1;
		Vector2 ballPosition = new Vector2 (x,y);
		return ballPosition;
	}

	public static void insertBallIntoGrid (Vector2 ballPosition,Ball ball)
	{
		grid[(int)ballPosition.x, (int)ballPosition.y] = ball;
	}



	public Spwaner GetSpwaner(){

		return spwaner;
	}
	
	private static void CheckForColumn(){
		int up=CheckUp();
		int down= 	CheckDown();
		if(down+up >=4){
			for (int i = 0; i < down; i++)
			{
				DestroyBallInGrid(x,y-i);
			}
			
			for (int i = 1; i <= up; i++)
			{
				DestroyBallInGrid(x,y+i);
			}
		}	
		
	}
	
	private static int CheckDown(){
	
		int count =1;
		
		while (y-count>=0 && grid[x,y-count].GetBallColor()==color){
			count++;
			
		}
	return count;
	}
	
	private static int CheckUp(){
		
		int count =0;
		
		while (y+count+1<NumberOfRowes && grid[x,y+1+count]	 && grid[x,y+1+count].GetBallColor()==color){
			count++;
			
		}
		return count;
	}
	
		
	private static void CheckForRow(){
		int MatchOnRight= CountRight();
		int MatchOnLeft = CountLeft ();
			if(MatchOnLeft+MatchOnRight>=3){
				for (int i = 0; i <= MatchOnRight; i++)
				{
					DestroyBallInGrid(x+i,y);
				}
				for (int i = 0; i < MatchOnLeft; i++)
				{
					DestroyBallInGrid(x-i-1,y);
				}
		}
	}
	
	private static void CheckForDiagBottomLeft(){
		int downLeft= CountDiagDownLeft();
		int upRight = CountDiagUpRight();
		if (downLeft+upRight>=3){
			for(int i=0;i<=upRight;i++){
				DestroyBallInGrid(x+i,y+i);
			}
			
			for(int i=1;i<=downLeft;i++){
				
				Destroy(grid[x-i,y-i].gameObject);
			}
		}
	
	}
	
	
	private static void	CheckForDiagBottomRight(){
	
		int downRight= CountDiagDownRight();
		int upLeft = CountDiagUpLeft();
		
		if (downRight+upLeft>=3){
			
			for(int i=0;i<=downRight;i++){
				
				Destroy(grid[x+i,y-i].gameObject);
			}
			
			for(int i=1;i<=upLeft;i++){
				
				Destroy(grid[x-i,y+i].gameObject);
			}
		}
	
	
	}
	
	
	private static int CountDiagUpLeft(){
		int count=0;
		while(x-count-1>=0 && y+count+1<NumberOfRowes && grid[x-count-1,y+count+1] && grid[x-count-1,y+count+1].GetBallColor()==color){
			count++;
			
		}
		return count;
	}
	
	
	private static int CountDiagDownRight(){
		int count=0;
		while(x+count+1< grid.GetLength(0)-1 && y-count-1>=0 && grid[x+count+1,y-count-1] && grid[x+count+1,y-count-1].GetBallColor()==color){
			count++;
			
		}
		return count;
	}
	
	private static int CountDiagDownLeft(){
	int count=0;
		while(x-count-1>=0 && y-count-1>=0 && grid[x-count-1,y-count-1] && grid[x-count-1,y-count-1].GetBallColor()==color){
		count++;
		
		}
		return count;
	}
	private static int CountDiagUpRight(){
		int count=0;
		while(x+count+1< grid.GetLength(0)-1 && y+count+1<NumberOfRowes && grid[x+count+1,y+count+1] && grid[x+count+1,y+count+1].GetBallColor()==color){
			count++;
			
		}
		return count;
	}

//
	

	private static int CountRight(){
	int count =0;
		
		while(x+count+1< grid.GetLength(0)-1 && grid[x+1+count ,y] && grid[x+1+count ,y].GetBallColor()==color){
			count++;
		}
	return count;
	}


	public static void SetNullToPreviousPositionOfBall(int xPosition,int yPosition){
		grid [xPosition,yPosition] = null;
	}
	
	
	private static int CountLeft(){
		int count =0;
		
		while(x-count-1>=0 && grid[x-1-count ,y] && grid[x-1-count ,y].GetBallColor()==color){
			count++;
		}
		return count;
	}
	

	public static void DestroyBallInGrid (int x, int y)
	{
		Destroy(grid[x,y].gameObject);

	}
	public static int GetNumberOfRows(){
	
		return NumberOfRowes;
	}
}
