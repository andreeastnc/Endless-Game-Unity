using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;
    // TypeOfPowerUp va primi o valoare random de la 1 la nr. de power ups - 1
    public int typeOfPowerUp;
    private PlayerMovement player;
    private int buffDurationInSeconds = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        // Decidem random ce tip de power up va fi si modificam obiectul
        typeOfPowerUp = Random.Range(1, 4);
        typeOfPowerUp = 1;
        Renderer rend = gameObject.GetComponent<Renderer>();

        // Decidem cum va arata power up il inainte de instantiere
        switch (typeOfPowerUp)
        {
            case 1:
                // Jump boost
                rend.material.color = Color.magenta;
                break;

            case 2:
                // Speed boost
                rend.material.color = Color.blue;
                break;
            case 3:
                // Double Jump
                rend.material.color = Color.yellow;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if object spawned inside an obstacle
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check that the object we collided with is the player
        if (other.gameObject.name != "Player")
        {
            return;
        }

        // pt. a ascunde obiectul, deoarece se distruge dupa buffDuration secunde
        gameObject.transform.localScale = new Vector3(0, 0, 0);

        switch (typeOfPowerUp)
        {
            case 1:
                // Jump boost
                player.setPlayerJumpHeight(8);
                StartCoroutine(startJumpBoost());
                break;
            case 2:
                // Speed boost
                // Memoram viteza actuala in oldSpeed
                var oldSpeed = player.speed;
                // Marim viteza player-ului cu 50%
                player.setPlayerSpeed(oldSpeed * 1.50f);
                StartCoroutine(startSpeedBoost(oldSpeed));
                break;
            case 3:
                player.setDoubleJump(true);
                StartCoroutine(startDoubleJumpBoost());
                break;

            default:
                // Buff urile distrug obiectul dupa ce termina 
                // de rulat, altfel efectul nu va fi reverted
                Destroy(gameObject);
                break;
        }
    }

    void undoPlayerSpeedBuff(float oldSpeed)
    {
        // Setam viteza player-ului 
        player.setPlayerSpeed(5);
        // Destroy this object
        Destroy(gameObject);
    }

    IEnumerator startSpeedBoost(float oldSpeed)
    {
        // Asteptam buffDuration secunde dupa care revenim 
        // la viteza pe care o avea player-ul cand a luat boost-ul,
        // astfel incat monedele luate pe durata buff-ului conteaza doar la scor.
        yield return new WaitForSeconds(this.buffDurationInSeconds);
        undoPlayerSpeedBuff(oldSpeed);
    }

    void undoPlayerJumpBoost()
    {
        // Functia apelata fara parmetrii pune val. default
        player.setPlayerJumpHeight();
        // Destroy this object
        Destroy(gameObject);

    }

    IEnumerator startJumpBoost()
    {
        yield return new WaitForSeconds(this.buffDurationInSeconds);
        undoPlayerJumpBoost();
    }

    void undoPlayerDoubleJumpBoost()
    {
        player.setDoubleJump(false);
        Destroy(gameObject);
    }

    IEnumerator startDoubleJumpBoost()
    {
        yield return new WaitForSeconds(this.buffDurationInSeconds);
        undoPlayerDoubleJumpBoost();
    }
}
