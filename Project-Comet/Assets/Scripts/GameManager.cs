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

	private Button pauseButton;
	private Button resumeButton;
	
	void Awake () {
		DontDestroyOnLoad(this.gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;
		//PlayerPrefs.GetInt("Highscore", score);
	}
	
	public void StartGame () {
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	public void PauseGame () {
		resumeButton.interactable = true;
		pauseButton.interactable = false;
		StartCoroutine(InitiatePause());
	}

	public void ResumeGame () {
		resumeButton.interactable = false;
		pauseButton.interactable = true;
		Time.timeScale = 1f;
	}

	private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		if (scene.name == "Game") {
			UpdateGameScoreUI(0);

			pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
			resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
			pauseButton.onClick.AddListener(PauseGame);
			resumeButton.onClick.AddListener(ResumeGame);
			Player.ScoreUp += AddScore;
			//Player.FinalizeScore += SaveScore;
		}
	}

	private void UpdateGameScoreUI(int value) {
		score += value;
		foreach (GameObject scoreText in GameObject.FindGameObjectsWithTag("Score")) {
			scoreText.GetComponent<TextMeshProUGUI>().text = " Score: " + score;
		}
	}

	private void AddScore (int value) {
			UpdateGameScoreUI(value);
	}

	private void SaveScore () {
		if (score > highScore) {
			PlayerPrefs.SetInt("Highscore", score);
		} 
	}

	 IEnumerator InitiatePause() {
        yield return new WaitForSeconds(0.15f);
		Time.timeScale = 0f;
    }
}
