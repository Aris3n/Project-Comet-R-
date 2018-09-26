using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public TextMeshProUGUI ascoreText;
    public TextMeshProUGUI[] scoreTexts;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI fHighScoreText;
    private CanvasManager canvasManager;

    private void Start()
    {
        canvasManager = GetComponent<CanvasManager>();
    }
    public void UpdateScoreUI(int score)
    {
        foreach (TextMeshProUGUI scoreText in scoreTexts)
            scoreText.text = "Score\n" + score;
        ascoreText.text = " Score:" + score;
    }
    public void UpdateHighScoreUI(int value, string newScore)
    {
        string text = "High Score\n" + value;
        highScoreText.text = text;
        fHighScoreText.text = text;
        if (newScore != "")
            text += "\n" + newScore;
        fHighScoreText.text = text;
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void PauseGame()
    {
        StartCoroutine(InitiatePause());
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void ShowGameOver()
    {
        CanvasGroup gameCanvas = transform.GetChild(0).GetComponent<CanvasGroup>();
        CanvasGroup gameOverCanvas = transform.GetChild(2).GetComponent<CanvasGroup>();
		canvasManager.ShowCanvas(gameCanvas, gameOverCanvas);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    IEnumerator InitiatePause()
    {
        yield return new WaitForSeconds(0.15f);
        Time.timeScale = 0f;
    }
}
