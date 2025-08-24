using UnityEngine;

public class SkillSpawner: MonoBehaviour
{
    public GameObject SkillPrefab;
    public GameObject SkillPoint;
    
    void Start()
    {
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            var skill = Instantiate(SkillPrefab, SkillPoint.transform.position, Quaternion.identity);
            //var skillrb = skill.GetComponent<Rigidbody2D>();
            Debug.Log("Skill spawned at position: " + transform.position);
            skill.GetComponent<SkillScript>().direction = transform.localScale.x > 0 ? 1 : -1; // Set direction based on character's facing direction
        }
    }
}
