using UnityEngine;

public class Map : MonoBehaviour
{
    public float speed;
    public GameObject map;

    private void Awake()
    {

    }

    void FixedUpdate()
    {
        map.transform.Translate(0, 0, -speed);
    }
}
