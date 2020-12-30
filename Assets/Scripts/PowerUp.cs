using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;
    string[] PowerUps = new string[] { "Speed+", "Jump+" };

    // Decidem random ce tip de power up va fi acesta
    void Start()
    {
        var typeOfPowerUp = Random.Range(0, 2);
        Debug.Log(PowerUps[typeOfPowerUp]);

        switch (typeOfPowerUp)
        {
            // Speed+
            case 0:
                break;
            // Jump+
            case 1:
                break;
                
        }

    }

    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if powerup spawned inside an obstacle
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

        // Destroy this object
        Destroy(gameObject);
    }
}
