using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	private TextMeshProUGUI scoreText;
	private int score;
	private int highScore;
	
	void Awake () {
		DontDestroyOnLoad(this.gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;
		PlayerPrefs.GetInt("Highscore", score);
	}
	
	void Update () {
		
	}

	public void StartGame () {
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	public void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		if (scene.name == "Game") {
			Debug.Log("In game");
			scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
			scoreText.text = " Score: " + score;
			Player.ScoreUp += AddScore;
			Player.FinalizeScore += SaveScore;
		}
	}

	private void AddScore (int value) {
			score += value;
			scoreText.text = "Score: " + score;
	}

	private void SaveScore () {
		if (score > highScore) {
			PlayerPrefs.SetInt("Highscore", score);
		} 
	} 
}
