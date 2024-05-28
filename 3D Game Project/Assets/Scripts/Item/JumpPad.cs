using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPadPower;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(Vector2.up * jumpPadPower, ForceMode.Impulse);
        }
    }
}
