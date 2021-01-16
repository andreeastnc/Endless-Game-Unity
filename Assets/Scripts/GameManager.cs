using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int score;
    public static GameManager inst;
    
    // Initializat cu null ca sa nu mai primim warning in unity
    [SerializeField] Text scoreText = null;
    [SerializeField] Text highScoreText = null;

    [SerializeField] PlayerMovement playerMovement = null;
    GroundTIle groundTIle;

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score;

        // Increase the player's speed
        playerMovement.speed += playerMovement.speedIncreasePerPoint;

        // Nu stiu unde sa  pun :)))
        checkHighScore();
    }

    public int GetScore()
    {
        return score;
    }

    private void Awake()
    {
        inst = this;
    }


    void Start()
    {
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void checkHighScore()
    {
        //PlayerPrefs.SetInt("Highscore", 0);

        int highScore = PlayerPrefs.GetInt("Highscore", 0);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
