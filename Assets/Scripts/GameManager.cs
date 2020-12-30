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

    [SerializeField] PlayerMovement playerMovement = null;
    GroundTIle groundTIle;

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score;

        // Increase the player's speed
        playerMovement.speed += playerMovement.speedIncreasePerPoint;
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
    }

    void Update()
    {
    }
}
