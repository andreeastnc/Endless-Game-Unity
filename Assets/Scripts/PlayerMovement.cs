using UnityEngine;
using UnityEngine.SceneManagement;

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

        // Daca player este pe sol si apasa space atunci sare
        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            canJump = true;
        }
    }

    public void Die()
    {
        alive = false;
        //Restart the game
        Invoke("Restart", 2);
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
        speed = newSpeed;
    }

    public void setPlayerJumpHeight(float newJumpHeight = 5)
    {
        jumpHeight = newJumpHeight;
    }
}
