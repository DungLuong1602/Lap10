using UnityEngine;

public class EnemyMoving: MonoBehaviour
{
    public Animator animator; // Reference to the Animator component for animations
    //Patrol points for the enemy to move between
    private Vector3 pointA;
    private Vector3 pointB;
    public Vector3 pointBOffset = new Vector3(3, 0, 0); // Offset for point B from point A
    public float patrolspeed = 2f;
    public float chasingSpeed = 3.5f;

    //Define the target position and Rigidbody2D component
    public float detectionRange = 5f; // Range within which the enemy detects the player
    public float attackRange = 1f; // Range within which the enemy can attack the player
    public LayerMask playerLayer;
    public LayerMask groundLayer;

    //Raycast 
    public Transform groundCheck; // Position to check for ground
    public float groundCheckRadius = 0.2f; // Radius of the ground check

    //attack setting
    public float attackDamage = 1f; // Time between attacks
    public float attackCooldown = 1.5f; // Time between attacks

    private Transform player;
    private Rigidbody2D rb;
    private bool movingtoB = true; // Flag to determine direction of movement
    private float lastAttackTime; // Time when the last attack occurred
    private bool isFacing = true; // Flag to check if the enemy is grounded
    private bool isDead = false; // Flag to check if the enemy is dead

    private void Start()
    {//
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to this GameObject
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player GameObject by tag
        pointA = transform.position;
        pointB = pointA + pointBOffset; // Calculate point B based on point A and the offset
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position); // Calculate distance to the player
        //if(distanceToPlayer <= attackRange && CanSeePlayer())
        //{
        //    Attack(); // If within attack range and can see the player, call the Attack method
        //}

        //else if(distanceToPlayer <= detectionRange && CanSeePlayer())
        //{
        //    ChasePlayer(); // If within detection range and can see the player, call the ChasePlayer method
        //}
        //else
        //{
            patrol(); // If not within detection or attack range, call the patrol method
        //}
    }

    void patrol()
    {
       animator.SetBool("IsRunning", true); // Set the "IsRunning" parameter in the Animator to true
        Vector2 targePosition = movingtoB ? pointB : pointA; // Determine target position based on direction
        //sắp tới mép vự thì quay lại
        if(!GroundAhead())
        {
            movingtoB = !movingtoB; // Switch direction if no ground ahead
            Flip(); // Flip the enemy's direction
        }

        float direction = (targePosition.x - transform.position.x)>0 ? 1f : -1f; // Determine direction based on the target position
        rb.linearVelocity = new Vector2(direction * patrolspeed, rb.linearVelocity.y); // Set the horizontal velocity of the Rigidbody
        if(Vector2.Distance(transform.position, targePosition) < 0.1f) // Check if the enemy has reached the target position
        {
            movingtoB = !movingtoB; // Switch direction
            Flip(); // Flip the enemy's direction
        }
    }
    //void ChasePlayer()
    //{
    //    float direction = (player.position.x - transform.position.x) > 0 ? 1f : -1f; // Determine direction towards the player
    //    rb.linearVelocity = new Vector2(direction * chasingSpeed, rb.linearVelocity.y); // Set the horizontal velocity of the Rigidbody towards the player
    //    if ((direction > 0 && !isFacing) || (direction < 0 && isFacing)) // Check if the enemy is facing the correct direction
    //    {
    //        Flip(); // Flip the enemy's direction
    //    }
    //}
    bool GroundAhead()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, LayerMask.GetMask("Ground")); // Check if there is ground ahead using a raycast
    }
    void Attack()
    { }
    void Flip()
    {
        isFacing = !isFacing; // Toggle the facing direction
        Vector3 scale = transform.localScale; // Get the current local scale
        scale.x *= -1; // Flip the x-axis scale
        transform.localScale = scale; // Apply the flipped scale to the GameObject
    }

    //bool CanSeePlayer()
    //{
       // Vector2 directionToPlayer = player.position - transform.position; // Calculate the direction to the player
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, detectionRange, playerLayer | groundLayer); // Cast a ray towards the player
        //if(hit.collider != null ) // Check if the ray hit the player
        //{
            //return hit.collider.CompareTag("Player"); // Player is detected
       // }
       // return false; // Player is not detected
    //}

    //Debug
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, detectionRange);

    //    if (groundCheck != null)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckRadius);
    //    }
    //}
    public void Die()
    {
        isDead = true; // Set the isDead flag to true
        Destroy(gameObject, 0.5f);
        animator.SetBool("IsDead",true); // Trigger the death animation
    }

}
