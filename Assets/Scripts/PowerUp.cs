using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;
    public int typeOfPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        // Decidem random ce tip de power up va fi si modificam obiectul
        typeOfPowerUp = Random.Range(1, 2);
        Renderer rend = gameObject.GetComponent<Renderer>();

        switch (typeOfPowerUp)
        {
            case 1:
                rend.material.color = Color.red;
                break;

            case 2:
                rend.material.color = Color.blue;
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

        switch (typeOfPowerUp)
        {
            case 1:
                // Speed Boost
                break;
            case 2:
                // Jump Boost
                break;
        }

        // Destroy this object
        Destroy(gameObject);
    }
}
