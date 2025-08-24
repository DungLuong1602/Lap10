using UnityEngine;

public class MovingPlatfrom :MonoBehaviour
{
   public Vector3 moveOffset = new Vector3(3, 0, 0);
    public float speed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingtoTarget = true;
    //private Rigidbody2D rd;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveOffset;
    }

    void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, movingtoTarget ? targetPosition : startPosition, speed * Time.deltaTime);
        //Debug.Log("Moving platform position: " + transform.position);
        // Check if we reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            movingtoTarget = false;
        }
        else if (Vector3.Distance(transform.position, startPosition) < 0.01f)
        {
            movingtoTarget = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // Attach the player to the platform
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // Detach the player from the platform
            collision.transform.SetParent(null);
        }
    }
}
