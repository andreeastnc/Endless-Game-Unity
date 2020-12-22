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

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score;

        // Increase the player's speed
        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
