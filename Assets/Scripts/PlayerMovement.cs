﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;
            
    public float speed = 5;
    // Initializat cu null ca sa nu mai primim warning in unity
    [SerializeField] Rigidbody rb = null;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;
    public float jumpHeight = 5;
    bool isOnGround;
    bool canJump = false;
    bool hasDoubleJump = false;
    bool coroutineRunning = false;
    int buffDurationInSeconds = 10;

    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);

        if (canJump) 
        {
            canJump = false;
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        }

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < -5)
        {
            Die();
        }

        bool spacePressed = Input.GetKeyDown(KeyCode.Space);
        // Daca player este pe sol (sau are double jump) si apasa space atunci sare
        if (isOnGround && spacePressed)
        {
            canJump = true;
        }

        if (!isOnGround && spacePressed && hasDoubleJump)
        {
            canJump = true;
            hasDoubleJump = false;
        }

        if (isOnGround && coroutineRunning)
        {
            hasDoubleJump = true;
        }
    }

    public void Die()
    {
        alive = false;
        // Go back to start menu
        Invoke("GoToStartScreen", 2);
    }

    void GoToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionStay (Collision collisionInfo)
    {
        // Daca player este la sol
        isOnGround = true;
    }
    
    void OnCollisionExit (Collision collisionInfo)
    {
        // Daca player este in aer
        isOnGround = false;
    }

    public void setPlayerSpeed(float newSpeed = 5.0f)
    {
        var oldSpeed = speed;
        speed = newSpeed;
        StartCoroutine(startSpeedBoost(oldSpeed));
    }

    IEnumerator startSpeedBoost(float oldSpeed)
    {
        // Asteptam buffDuration secunde dupa care revenim 
        // la viteza pe care o avea player-ul cand a luat boost-ul,
        // astfel incat monedele luate pe durata buff-ului conteaza doar la scor.
        yield return new WaitForSeconds(buffDurationInSeconds);
        undoPlayerSpeedBuff(oldSpeed);
    }

    void undoPlayerSpeedBuff(float oldSpeed)
    {
        // Setam viteza player-ului 
        setPlayerSpeed(5);
    }

    public void setPlayerJumpHeight(float newJumpHeight = 5)
    {
        jumpHeight = newJumpHeight;
        StartCoroutine(startJumpBoost(buffDurationInSeconds));
    }

    void undoPlayerJumpBoost()
    {
        // Functia apelata fara parmetrii pune val. default
        setPlayerJumpHeight();
    }

    IEnumerator startJumpBoost(int buffDurationInSeconds)
    {
        yield return new WaitForSeconds(buffDurationInSeconds);
        undoPlayerJumpBoost();
    }

    public void setDoubleJump(bool doubleJump)
    {
        hasDoubleJump = doubleJump;
        StartCoroutine(startDoubleJumpBoost());
    }

    IEnumerator startDoubleJumpBoost()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(buffDurationInSeconds);
        undoPlayerDoubleJumpBoost();
        coroutineRunning = false;
    }

    void undoPlayerDoubleJumpBoost()
    {
        setDoubleJump(false);
    }
}
