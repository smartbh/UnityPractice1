using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullerController : MonoBehaviour
{
    private Rigidbody2D bulletRigidBody2D;
    private float bulletSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody2D = GetComponent<Rigidbody2D>();
        bulletRigidBody2D.velocity = bulletSpeed * transform.right;
    }
}
