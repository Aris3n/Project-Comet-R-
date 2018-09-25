using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text scoreText;
	public int score;
	
	void Start () {
		scoreText.text = " Score: " + score;
		Player.ScoreUp += AddScore;
	}
	
	void Update () {
	
	}
	public void AddScore (int value) {
			score += value;
			scoreText.text = "Score: " + score;
	}
}
