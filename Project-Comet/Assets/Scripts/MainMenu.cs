using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public AudioSource buttonAudio;

    public void SetHighScoreText(float highScore)
    {
        highScoreText.text = highScore.ToString();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonAudio()
    {
        buttonAudio.Play();
    }
}
