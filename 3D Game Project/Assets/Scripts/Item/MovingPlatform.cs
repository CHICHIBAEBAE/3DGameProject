using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float speed;
    [SerializeField] float platformTime;
    Vector3 moveDir;
    Vector3 dir;

    void Start()
    {
        dir = Vector3.right;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MovePlatform(platformTime));
    }

    private void FixedUpdate()
    {
        moveDir = dir * speed * Time.deltaTime;
        rb.MovePosition(transform.position + moveDir);
    }
        
    IEnumerator MovePlatform(float time)
    {
        while (true)
        {
            dir *= -1;
            yield return new WaitForSeconds(time);
        }
    }
}