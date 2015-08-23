using UnityEngine;
using System.Collections;

public class GameGrid : MonoBehaviour {

	public int numberOfColoumns;
	private static int numberOfPipes=12, NumberOfRowes=8;
	public static Spwaner spwaner;
	private static bool foundSequence=false;
	private static bool checkingForSequence=false;
	private static Ball[,] grid = new Ball[numberOfPipes, NumberOfRowes];
	private static int x,y;
	private static Vector4 color;
	private static Vector2[] checkForSequanceArray= new Vector2[99999];
	private static int currentPosInArray=0;
	
	

	void Start () {
	
	for(int i=0;i<checkForSequanceArray.Length;i++){
			checkForSequanceArray[i]=new Vector2(-1,-1);
	
	}
		spwaner = FindObjectOfType<Spwaner> ();
		GameObject barInstance = GameObject.FindGameObjectWithTag ("Bars");
		numberOfColoumns = barInstance.transform.childCount;
		
	}


	void Update(){
		print (checkForSequanceArray[currentPosInArray].x);
		if(checkingForSequence==false && checkForSequanceArray[currentPosInArray].x>-1){
		
		CheckForSequence();
		}
	
	}

	private static void CheckForSequence (){
		//int x = (int)GridX-1
		
		
		checkingForSequence=true;
		
		x=(int) checkForSequanceArray[currentPosInArray].x;
		y=(int) checkForSequanceArray[currentPosInArray].y;
		CheckForColumn();
		CheckForRow();
		
		checkingForSequence=false;
		currentPosInArray++;
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
		int i=0;
		if(Time.timeSinceLevelLoad>2){
		while(checkForSequanceArray[i].x>-1){
		
		i++;
		
		}
		checkForSequanceArray[i]=new Vector2(x,y);
		print ("x = " + x);
		}
				spwaner.RandomlySpwanBalls();
			
			//ChangeAllBallsLandBool(true);
	
			//print ("ok");	
		
		
	}

	private void NextBall(){


	}

	public Spwaner GetSpwaner(){

		return spwaner;
	}
	
	
	private static void CheckForColumn(){
	
	int count =1;
	
		while (y-count>=0 && grid[x,y-count].GetBallColor()==color){
			count++;
	
		}
		if(count >=4){
			foundSequence=true;
			ChangeAllBallsLandBool(false,y);
			for (int i = 0; i < count; i++)
			{
				print("Destroy " +  (i+1));
				//print ("is Landed " +  grid[x,y-i].GetIsLanded());
				Destroy(grid[x,y-i].gameObject);
			}
		}	
	}
		
	private static void CheckForRow(){
		int MatchOnRight= CountRight();
		int MatchOnLeft = CountLeft ();
			if(MatchOnLeft+MatchOnRight>=3){
				foundSequence=true;
				ChangeAllBallsLandBool(false,y);
				for (int i = 0; i <= MatchOnRight; i++)
				
				{
				print("Destroy " +  (i+1));
					Destroy(grid[x+i,y].gameObject);
				}
				for (int i = 0; i < MatchOnLeft; i++)
				
				{
				print("Destroy " +  (i+1));
					Destroy(grid[x-i-1,y].gameObject);
				}
			}
	
	}
	
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
	public static void ChangeAllBallsLandBool(bool changeTO,int y){
	

		for (int i = 0; i < numberOfPipes; i++)
		{	
			for (int j = 0; j < NumberOfRowes; j++){
				if(grid[i,j] && j>y){
					grid[i,j].UnLandBall(changeTO);
					
				}	
			}	
		}
	}
	
	public static bool GetfoundSequence(){
	
		return foundSequence;
	}
	
	public static void SetfoundSequence(bool set){
	
		foundSequence=set;
	}
	
}
