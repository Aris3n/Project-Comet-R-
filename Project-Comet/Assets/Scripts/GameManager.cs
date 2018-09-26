using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private MainMenu mainMenu;
    private GameMenu gameMenu;
    private int score;
    [SerializeField]
    private int[] scoreMarkers;
    private int highScore;
    public delegate void IncreaseDifficulty();
    public static event IncreaseDifficulty DiffcultyUp;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        highScore = PlayerPrefs.GetInt("Highscore", 0);
        if (scene.name == "MainMenu")
        {
            mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();
            mainMenu.SetHighScoreText(highScore);
        }
        else if (scene.name == "Game")
        {
            score = 0;
            gameMenu = GameObject.FindGameObjectWithTag("GameMenu").GetComponent<GameMenu>();
            gameMenu.UpdateScoreUI(0);
            gameMenu.UpdateHighScoreUI(highScore, "");
            UnsubscribeGameEvents();
            SubscribeGameEvents();
        }
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
        score += value;
        gameMenu.UpdateScoreUI(score);
        for (int i = 0; i < scoreMarkers.Length; i++)
            if (score == scoreMarkers[i])
                DiffcultyUp();
    }
    private void SaveScore()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            gameMenu.UpdateHighScoreUI(score, "(New Record)");
        }
        gameMenu.ShowGameOver();
    }
}