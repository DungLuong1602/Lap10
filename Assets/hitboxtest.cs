using Unity.VisualScripting;
using UnityEngine;

public class hitboxtest : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           Debug.Log("Player has collided with the object.");
           GetComponentInParent<EnemyMoving>().Die();
            // Optionally, you can also destroy the object itself if needed
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX,10f); // Stop the Rigidbody2D's movement
            }
        }
    }

}
