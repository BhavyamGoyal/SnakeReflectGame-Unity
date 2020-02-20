using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    Button pauseButton, startButton;
    Text scoreText, playText, playButtonText;
    GameObject startUIObject;
    public int score = 0;
    public UIManager()
    {
        pauseButton = GameObject.FindGameObjectWithTag("pauseButton").GetComponent<Button>();
        startUIObject = GameObject.FindGameObjectWithTag("startButton");
        startButton = startUIObject.GetComponentInChildren<Button>();
        scoreText = GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>();
        playText = startUIObject.transform.GetChild(0).GetComponent<Text>();
        playButtonText = startUIObject.transform.GetChild(1).GetComponentInChildren<Text>();
        startButton.onClick.AddListener(Start);
        pauseButton.onClick.AddListener(Pause);
        SetInstance(this);
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text =score.ToString();
    }

    public void Start()
    {
        startUIObject.SetActive(false);
        Time.timeScale = 1;
        switch (playButtonText.text)
        {
            case "Start":
                pauseButton.gameObject.SetActive(true);
                GameManager.Instance.SpawnPlayer();
                break;

            case "Resume":
                break;
            case "Restart":
                Application.LoadLevel(0);
                break;
        }
    }
    public void ShowGameOver()
    {
        playText.text = "Your score is :" + score;
        playButtonText.text = "Restart";
        startUIObject.SetActive(true);
    }
    

    public void Pause()
    {
        Time.timeScale = 0;
        playText.text = "Your score is :" + score;
        playButtonText.text = "Resume";
        startUIObject.SetActive(true);
    }

}
