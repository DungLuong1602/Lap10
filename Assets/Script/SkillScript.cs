using UnityEngine;

public class SkillScript : MonoBehaviour
{
    public float speed = 10f;
    public float direction = 1f; // 1 for right, -1 for left
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * Time.deltaTime * speed * direction);
    }
}
