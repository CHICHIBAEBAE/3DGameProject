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
        // Force : ������ ��� & �������� ��
        // Acceleration : ������ ���� & �������� ��
        // Impulse : ������ ��� & �������� ��
        // VelocityChange : ������ ���� & �������� ��

        //_rigidbody.velocity = transform.right * h + transform.forward * v;
    }
}
