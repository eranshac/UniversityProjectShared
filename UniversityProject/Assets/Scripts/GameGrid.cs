using UnityEngine;
using System.Collections;
using System;
	

public class GameGrid : MonoBehaviour {

	public int numberOfColoumns;
	private static int numberOfPipes=12, NumberOfRowes=8;
	public static Spwaner spwaner;
	private static int countCallForCheck=0;
	private static Ball[,] grid = new Ball[numberOfPipes, NumberOfRowes];
	private static int x,y;
	private static Vector4 color;
	

	
	

	void Start () {
	

	
	
		
		GameObject barInstance = GameObject.FindGameObjectWithTag ("Bars");
		numberOfColoumns = barInstance.transform.childCount;
		
	}


	void Update(){
	
	
	}

	private static void CheckForSequence (){
		//int x = (int)GridX-1
		
		

		
		
		CheckForColumn();
		CheckForRow();
		CheckForDiagBottomLeft();
		CheckForDiagBottomRight();
		countCallForCheck--;
	
	}

	public static void InsertBallToGrid(Ball ball){

		float ballRadius = ball.GetComponent<CircleCollider2D>().radius;
		float ballXPosition = ball.transform.position.x;
		float GridX = Mathf.Round( (ballXPosition+1)/2 );
		float GridY = Mathf.Round(( ball.transform.position.y )/(ballRadius*2));
		x=(int)GridX-1;
		y=(int)GridY-1;
		grid[x, y] = ball;
		
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

	public Spwaner GetSpwaner(){

		return spwaner;
	}
	
	
	private static void CheckForColumn(){
			
		int up=CheckUp();
		int down= 	CheckDown();
	
		if(down+up >=4){
			
			print ("ok");
			for (int i = 0; i < down; i++)
			{
				Destroy(grid[x,y-i].gameObject);
			}
			
			for (int i = 1; i <= up; i++)
			{
				Destroy(grid[x,y+i].gameObject);
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
				
					Destroy(grid[x+i,y].gameObject);
				}
				for (int i = 0; i < MatchOnLeft; i++)
				
				{
			
					Destroy(grid[x-i-1,y].gameObject);
				}
		
			
		
			
		}
	
		
		
	
	
	}
	
	private static void CheckForDiagBottomLeft(){
	
		int downLeft= CountDiagDownLeft();
		int upRight = CountDiagUpRight();
		
		if (downLeft+upRight>=3){
		
			for(int i=0;i<=upRight;i++){
				
				Destroy(grid[x+i,y+i].gameObject);
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
		//print(grid.GetLength(0));
		while(x+count+1< grid.GetLength(0)-1 && grid[x+1+count ,y] && grid[x+1+count ,y].GetBallColor()==color){
			count++;
		}
	return count;
	}
	
	
	private static int CountLeft(){
		int count =0;
		
		while(x-count-1>=0 && grid[x-1-count ,y] && grid[x-1-count ,y].GetBallColor()==color){
			count++;
		}
		return count;
	}
	

}
