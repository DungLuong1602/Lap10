using UnityEngine;

public class CameraControllerr : MonoBehaviour
{
    public Transform Target;
    public int smoothvalue = 2;
    public int PosY = 1;

    void Start()
    {

    }

    void Update()
    {
        Vector3 Targetpos = new Vector3(Target.transform.position.x, Target.transform.position.y + PosY);
        transform.position = Vector3.Lerp(transform.position, Targetpos, smoothvalue * Time.deltaTime);
    }
}
