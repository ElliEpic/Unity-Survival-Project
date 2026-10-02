using DG.Tweening;
using UnityEngine;

public class Tweening : MonoBehaviour
{
    Vector3 startPosition = new Vector3(-1.5f,0,0);
    Vector3 goalPosition = new Vector3(5f,0,0);
    float t = 0f;
    [SerializeField] float speed = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, goalPosition, t);
    }
}
