using UnityEngine;
using System.Collections;

public class GameGrid : MonoBehaviour {

	public int numberOfColoumns;
	private static int numberOfPipes=12, NumberOfRowes=8;
	public static Spwaner spwaner;
	private static Ball[,] grid = new Ball[numberOfPipes, NumberOfRowes];
	private static float timeFromLastCollision,currentTime;

	void Start () {
		spwaner = FindObjectOfType<Spwaner> ();
		GameObject barInstance = GameObject.FindGameObjectWithTag ("Bars");
		numberOfColoumns = barInstance.transform.childCount;
	}


	public static void CheckForSequence (int x, int y,Vector4 color){
		//int x = (int)GridX-1
		CheckForColumn(x,y,color);
		CheckForRow(x,y,color);
	
	
	}
	
	

	
	
	
	public static void UpdatetimeFromLastCollision(float time){
	
		timeFromLastCollision=time;
	}
	
	
	public static void InsertBallToGrid(Ball ball){
		float sum=0;
		int count=0;
		float ballRadius = ball.GetComponent<CircleCollider2D>().radius;
		float ballXPosition = ball.transform.position.x;
		float GridX = Mathf.Round( (ballXPosition+1)/2 );
		float GridY = Mathf.Round(( ball.transform.position.y )/(ballRadius*2));
		grid [(int)GridX-1, (int)GridY-1] = ball;
		
		CheckForSequence((int)GridX-1,(int)GridY-1, ball.GetBallColor());
		if(Time.timeSinceLevelLoad>1){
			while(timeFromLastCollision -Time.timeSinceLevelLoad + sum<10 ){
				print ("count " + count + "  timeFromLastCollision " +timeFromLastCollision);
				sum=sum+Time.deltaTime;
				//print("sum " + sum);
				
			}	
			count++;
			sum=0;					
			spwaner.RandomlySpwanBalls();
			ChangeAllBallsLandBool(true);
		}
			//print ("ok");	
		
		
	}

	private void NextBall(){


	}

	public Spwaner GetSpwaner(){

		return spwaner;
	}
	
	
	private static void CheckForColumn(int x, int y, Vector4 color){
	
	int count =1;
	
		while (y-count>=0 && grid[x,y-count].GetBallColor()==color){
			count++;
	
		}
		if(count >=4){
			ChangeAllBallsLandBool(false);
			for (int i = 0; i < count; i++)
			{
			
				Destroy(grid[x,y-i].gameObject);
			}
		}	
	}
		
	private static void CheckForRow(int x, int y, Vector4 color){
		int MatchOnRight= CountRight(x,y,color);
		int MatchOnLeft = CountLeft (x,y,color);
			if(MatchOnLeft+MatchOnRight>=3){
				ChangeAllBallsLandBool(false);
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
	
	private static int CountRight(int x, int y, Vector4 color){
	int count =0;
		//print(grid.GetLength(0));
		while(x+count+1< grid.GetLength(0)-1 && grid[x+1+count ,y] && grid[x+1+count ,y].GetBallColor()==color){
			count++;
		}
	return count;
	}
	
	
	private static int CountLeft(int x, int y, Vector4 color){
		int count =0;
		
		while(x-count-1>=0 && grid[x-1-count ,y] && grid[x-1-count ,y].GetBallColor()==color){
			count++;
		}
		return count;
	}
	private static void ChangeAllBallsLandBool(bool changeTO){
	

		for (int i = 0; i < numberOfPipes; i++)
		{	
			for (int j = 0; j < NumberOfRowes; j++){
				if(grid[i,j]){
					grid[i,j].UnLandBall(changeTO);
					
				}	
			}	
		}
	}
}
