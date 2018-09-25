using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score;
    private int highScore;

    private Button pauseButton;
    private Button resumeButton;
    private Button replayButton;
    private Button menuButton;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.sceneLoaded += OnSceneLoaded;
        highScore = PlayerPrefs.GetInt("Highscore", 0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        resumeButton.interactable = true;
        pauseButton.interactable = false;
        StartCoroutine(InitiatePause());
    }

    public void ResumeGame()
    {
        resumeButton.interactable = false;
        pauseButton.interactable = true;
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            score = 0;
            UpdateGameScoreUI(0);
            UpdateGameHighScoreUI(highScore, "");
            UpdateGameButtonUI();
            UnsubscribeGameEvents();
            SubscribeGameEvents();
        }
        else if (scene.name == "MainMenu")
        {
            UpdateScoreBoard(highScore);
        }
    }

    private void UpdateScoreBoard(int value)
    {
        GameObject.FindGameObjectWithTag("HighScore").GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    private void UpdateGameScoreUI(int value)
    {
        score += value;
        foreach (GameObject scoreText in GameObject.FindGameObjectsWithTag("Score"))
        {
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score\n" + score;
        }
        GameObject.FindGameObjectWithTag("AScore").GetComponent<TextMeshProUGUI>().text = " Score:" + score;
    }

    private void UpdateGameHighScoreUI(int value, string newScore)
    {
        foreach (GameObject highScoreText in GameObject.FindGameObjectsWithTag("HighScore"))
        {
            string text = "High Score\n" + value;
            if (newScore != "")
                text += "\n" + newScore;
            highScoreText.GetComponent<TextMeshProUGUI>().text = text;
        }
    }

    private void UpdateGameButtonUI()
    {
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        replayButton = GameObject.Find("ReplayButton").GetComponent<Button>();
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        replayButton.onClick.AddListener(StartGame);
        menuButton.onClick.AddListener(ReturnToMenu);
    }

    private void SubscribeGameEvents()
    {
        Player.ScoreUp += AddScore;
        Player.FinalizeScore += SaveScore;
    }

    private void UnsubscribeGameEvents()
    {
        Player.ScoreUp -= AddScore;
        Player.FinalizeScore -= SaveScore;
    }

    private void AddScore(int value)
    {
        Debug.Log(value);
        UpdateGameScoreUI(value);
    }

    private void SaveScore()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            UpdateGameHighScoreUI(score, "(New Record)");
        }
    }

    IEnumerator InitiatePause()
    {
        yield return new WaitForSeconds(0.15f);
        Time.timeScale = 0f;
    }
}