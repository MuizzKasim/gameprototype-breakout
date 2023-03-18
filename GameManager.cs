using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//Declaring all float variables
	public float BallSpeed = 25f;

	//Declaring all relevant GameObjects
	public GameObject Ball;
	public GameObject BallPrefab;

	public GameObject Music;

	public GameObject LoseGame;//Defeat message
	public GameObject WinGame; //Ordinary victory message
	public GameObject AdviceWin1; //Message that appears during an ordinary Win condition
	public GameObject SpecialWinGame; //Special victory message
	public GameObject AdviceWin2; //Message that appears during a special Win condition
	public GameObject RestartGame; //Appears at the end of the game
	public GameObject DeathNotice; //Appears after ball respawns
	public GameObject BreakOut; //Game title

	public Transform BallOrigin;

	//Declaring all int variables
	public int LivesCount = 3; //Counts the number of lives left
	public int ScoreCount = 0; //Counts the total score achieved

	//Declaring all UI Texts
	public Text GameMsg;
	public Text LoseMsg;
	public Text WinMsg;
	public Text AdviceMsg1;
	public Text SpecialWinMsg;
	public Text AdviceMsg2;
	public Text RestartMsg;
	public Text Lives;
	public Text Tutorial;
	public Text DeathMsg;
	public Text BreakOutMsg;
	public Text Score;

	//Declaring all boolean variables
	public bool GameStart = false;
	public bool GameEnd = false;
	public bool CanRestart = false;
	public bool Death = false;
	public bool DeathMsgActivate = false;

	Rigidbody rb;

	//Declaring all variables for Game Audio
	public AudioClip SoundEffect;
	private AudioSource source;

	//Awake
	void Awake(){
		Screen.SetResolution (1024, 768, false);
	}

	//Initialization
	void Start () {
		rb = Ball.GetComponent<Rigidbody> ();
		source = GetComponent<AudioSource> ();

		Lives.text = "Lives:  "+LivesCount;
		GameMsg.text = "Destroy all blocks to win the game.\nGoodluck and Have fun!\n\n\nPress Space to Start";
		LoseMsg.text = "GAME OVER\nTry again next time!";
		WinMsg.text = "CONGRATULATIONS\nYou've won the game!";
		AdviceMsg1.text = "Try  aiming  for  1000  score!\n(Hint:  Lives  give  extra  score  points!)";
		SpecialWinMsg.text = "CONGRATULATIONS\nYou've truly won the game!";
		AdviceMsg2.text = "Now, no  one  will ever  surpass your  achievement!";
		RestartMsg.text = "Press Space to Try Again";
		Tutorial.text = "Controls:\n←  to  move  Left\n→  to  move  Right\n\nSpacebar\nto Confirm";
		DeathMsg.text = "Press Space to  Launch  Ball";
		BreakOutMsg.text = "BREAKOUT";
		Score.text = "Score: " +ScoreCount;

		DeathNotice.SetActive (false);
		LoseGame.SetActive (false);
		WinGame.SetActive (false);
		AdviceWin1.SetActive (false);
		SpecialWinGame.SetActive (false);
		AdviceWin2.SetActive (false);
		RestartGame.SetActive (false);
		Music.SetActive (false);

		Debug.Log("Abdul Muizz bin Haji Kasim, Digital Media course, B20161128");
	}

	void Update () {
		//Code used to begin the game at the beginning
		if (GameStart == false && Input.GetKey (KeyCode.Space)) {
			GameStart = true;

			Destroy (GameMsg);
			Music.SetActive (true);
			StartGame ();
		}

		//Code used to tell player what to do after death
		if (Death == true && Input.GetKey (KeyCode.Space)) {
			DeathNotice.SetActive (false);
			Death = false;
			DeathMsgActivate = false;
		}

		if (Death == true && DeathMsgActivate == true) {
			DeathNotice.SetActive (true);

			Invoke ("RemoveDeathMsg", 0.5f);
		}

		//Code used to restart the game after it is over
		if (GameEnd == true && Input.GetKey (KeyCode.Space) && CanRestart == true) {
			Reset ();
		}
	}

	//When player press Spacebar, game will start
	void StartGame (){

		//Randomise the initial trajectory of the ball
		float randStart = Random.Range (0, 2);

		Debug.Log (randStart);

		if (randStart == 0) {
				rb.velocity = new Vector3 (BallSpeed, 0, BallSpeed);
		}else {
				rb.velocity = new Vector3 (-BallSpeed , 0, BallSpeed);
		}
		}	


	//Method used to keep track of Player's lives
	public void LoseLife(){

		LivesCount -= 1;

		Lives.text = "Lives:  " + LivesCount;

		if (LivesCount == 0) {
			GameOver ();
		} else {
			Resetup ();
			Death = true;
			DeathMsgActivate = true;
		}
	}

	//Method used to keep track of the Game's status (win/lose)
	void GameOver(){
		//Used as part as scoring system, each existing life gives extra 60 points
		if (LivesCount == 3) {
			ScoreCount += 180;
		} else if (LivesCount == 2) {
			ScoreCount += 120;
		} else if (LivesCount == 1) {
			ScoreCount += 60;
		}

		//Display Game Over message
		if (LivesCount == 3 && ScoreCount >= 820) {
			SpecialWinGame.SetActive (true); //Shows a special Winning screen if player never lost a life and beats the game (destroyed all cubes)
			AdviceWin2.SetActive(true);
		} else if (LivesCount != 3 && ScoreCount >= 820) {
			WinGame.SetActive (true); //Shows an ordinary Winning screen if player beats the game (destroyed all cubes)
			AdviceWin1.SetActive(true);
		}else{
			LoseGame.SetActive (true); //Shows a Losing screen if player doesn't beat the game or lost all lives

		}

		Score.text = "Score: " +ScoreCount; //Updates the score
		rb.isKinematic = true; //Locks the ball in place, to prevent ball from falling again into Death Zone

		GameEnd = true;

		InvokeRepeating ("Restart", 2f, 1f);
	}

	//Method called after collision with a cube
	public void DestroyCube(string type){
		source.PlayOneShot (SoundEffect, 0.2f);

		if (type == "Red") { //Red Cubes give 20 score points
			ScoreCount += 25;
		} else if (type == "Yellow") { //Yellow Cubes give 10 score points
			ScoreCount += 15;
		}

		Score.text = "Score: " + ScoreCount;

		if(ScoreCount == 820)
			GameOver ();
	}

	//Method to create a blinking UI effect for game restart
	void Restart(){
		RestartGame.SetActive (true);
		Invoke ("RemoveRestart", 0.5f);
		CanRestart = true;
	}

	void RemoveRestart(){
		RestartGame.SetActive (false);
	}

	//Method to create blinking UI effect after each respawn
	void RemoveDeathMsg(){
		DeathNotice.SetActive (false);
		DeathMsgActivate = false;
		Invoke ("ReactivateDeathMsg", 0.5f);
	}

	void ReactivateDeathMsg(){
		DeathMsgActivate = true;
	}

	//Method used to respawn the ball after death
	void Resetup(){
		Ball = Instantiate (BallPrefab, BallOrigin.transform) as GameObject;
		rb = Ball.GetComponent<Rigidbody> ();

		GameStart = false;
	}

	//Method used to reset the game fully
	void Reset(){
		SceneManager.LoadScene ("Breakout");
	}
}
