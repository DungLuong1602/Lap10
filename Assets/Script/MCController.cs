using UnityEngine;

public class MCController : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public float speed = 2f; // Speed of the character movement
    public float JumpForce = 5f; // Force applied when jumping
    private Rigidbody2D rb; // Reference to the Rigidbody component
    private bool IsJumping = false; // Flag to check if the character is jumping
    private int MaxHp = 100; // Health points of the character
    public int CurrentHp; // Current health points of the character
    public healhBar healthBar; // Reference to the health bar script
    private bool IsTakingDam = false; // Flag to check if the character is taking damage
    //private bool IsHit = false;
    private bool IsDead = false; // Flag to check if the character is dead
    public GameObject GameoverPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to this GameObject
        
    }
    private void Start()
    {
        CurrentHp = MaxHp; // Initialize current health points to maximum health
        healthBar.SetMaxHealth(MaxHp); // Set the maximum health in the health bar
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D keys or Left/Right arrows)
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // hướng phải
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // hướng trái
        }
        var move_x = moveInput * speed;
        rb.linearVelocity = new Vector2(move_x, rb.linearVelocity.y); // Set the horizontal velocity of the Rigidbody
        animator.SetBool("IsRunRight", moveInput != 0); // Set the "IsRunning" parameter in the Animator based on input
        animator.SetBool("IsIdle", moveInput == 0);

        if (Input.GetKeyDown(KeyCode.Space) && !IsJumping) // Check if the space key is pressed and the character is not already jumping
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce); // Apply a vertical force to the Rigidbody for jumping
            animator.SetBool("IsJump", true); // Set the "IsJumping" parameter in the Animator to true
            IsJumping = true; // Set the jumping flag to true
        }

        if(CurrentHp <= 0)
        {
            Die(); // Call the Die method if current health points are less than or equal to zero
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Check if the character collides with an object tagged as "Ground"
        {
            IsJumping = false; // Reset the jumping flag when landing on the ground
            animator.SetBool("IsJump", false); // Set the "IsJumping" parameter in the Animator to false
        }
        if(collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject); // Destroy the coin object when collected
            Debug.Log("Coin collected!"); // Log a message to the console
        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        CurrentHp -= damage; // Decrease current health points by 10
        healthBar.SetHealth(CurrentHp); // Update the health bar with the new health value
        IsTakingDam = true; // Set the taking damage flag to true
        animator.SetTrigger("IsTakingDam"); // Trigger the "IsHit" animation
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsJump", false); // Reset other animation states
    }

    void Die()
    {
        IsDead = true; // Set the dead flag to true
        Debug.Log("Player is dead!"); // Log a message to the console when the character dies
        GameoverPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy") && !IsHit) // Check if the character collides with an object tagged as "Enemy" and is not already hit
    //    {
    //        Hp -= 10; // Decrease health points by 10
    //        IsHit = true; // Set the hit flag to true
    //        Debug.Log("Player HP: " + Hp); // Log the current health points
    //        animator.SetTrigger("IsHit"); // Trigger the "IsHit" animation
    //    }
    //}

}
