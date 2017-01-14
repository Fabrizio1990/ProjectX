using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager UIInstance;

	public Text p1Score;
	public Text p2Score;
	public Text p1Moves;
	public Text p2Moves;
	public Text upMovementLegend;
	public Text rightMovementLegend;
	public Text downMovementLegend;
	public Text leftMovementLegend;
	public Text timeCount;
	public Text victory;

	private float time;
	public int intTime;

	public int p1ScoreInt;
	public int p2ScoreInt;

	void Awake(){
		UIInstance = this;
	}

	// Use this for initialization
	void Start () {
		TextGeneration ();
	}
	
	// Update is called once per frame
	void Update () {
		TimeManaging ();
	}

	public void TextGeneration(){
		p1Score.text = "0";
		p2Score.text = "0";
		p1Moves.text = "3";
		p2Moves.text = "3";
		upMovementLegend.text = "Up";
		rightMovementLegend.text = "Right";
		downMovementLegend.text = "Down";
		leftMovementLegend.text = "Left";
	}

	public void TimeManaging(){
		time += Time.deltaTime;
		intTime = Mathf.FloorToInt (time);
		timeCount.text = intTime.ToString();
	}

	public void AddScore(int playerID, int amount){
		if (playerID == 0) {
			p1Score.text = amount.ToString();
		} else {
			p2Score.text = amount.ToString();
		}
	}

	public void SetMoves (int playerID, int amount){
		if (playerID == 0) {
			p1Moves.text = amount.ToString();
		} else {
			p2Moves.text = amount.ToString();
		}
	}

	public void Victoring(int player){
		if (player == 1) {
			victory.text = "p1";
		} else if (player == 2) {
			victory.text = "p2";
		}
	}
}
