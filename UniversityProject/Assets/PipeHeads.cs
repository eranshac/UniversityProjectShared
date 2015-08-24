using UnityEngine;
using System.Collections;

public class PipeHeads : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for(int i=0; i<GameGrid.GetnumberOfPipes();i++){
			Transform child= this.gameObject.transform.GetChild(i);
			Animator animator = child.gameObject.GetComponent<Animator>();
		animator.SetBool("isWhite",true);
		}
	}

	
	// Update is called once per frame
	void Update () {

		for(int i=0; i<GameGrid.GetnumberOfPipes();i++){
			Transform child= this.gameObject.transform.GetChild(i);
			Animator animator = child.gameObject.GetComponent<Animator>();
			if(GameGrid.grid[i,6] && animator.GetBool("isWhite")){
			print ("ok");
			animator.SetBool("isRed",true);
			animator.SetBool("isBlinking",true);
			animator.SetBool("isWhite",false);
				
			}else if(!GameGrid.grid[i,6] && animator.GetBool("isRed")){
				
				animator.SetBool("isRed",false);
				animator.SetBool("isBlinking",false);
				animator.SetBool("isWhite",true);
						
				}					

		} 
	} 
}
