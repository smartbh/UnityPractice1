using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D arrowRigidBody2D;
    private float arrowSpeed = 12f;

    // Start is called before the first frame update
    void Start()
    {
        arrowRigidBody2D = GetComponent<Rigidbody2D>();
        arrowRigidBody2D.AddForce(arrowSpeed * transform.right, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = arrowRigidBody2D.velocity.normalized;
    }
}
