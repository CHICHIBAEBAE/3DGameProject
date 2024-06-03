using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTet : MonoBehaviour
{
    public float amount = 50f;
    public Rigidbody _rigidbody;

    public Vector3 _velocity;

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal") * amount * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * amount * Time.deltaTime;

        _velocity = _rigidbody.velocity;
        // _rigidbody.AddForce(transform.up * h + transform.right * v);
        
        // ForceMode
        // Force : 질량을 사용 & 지속적인 힘
        // Acceleration : 질량을 무시 & 지속적인 힘
        // Impulse : 질량을 사용 & 순간적인 힘
        // VelocityChange : 질량을 무시 & 순간적인 힘

        //_rigidbody.velocity = transform.right * h + transform.forward * v;
    }
}
