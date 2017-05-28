using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProjectileController : MonoBehaviour {
    public float MoveSpeed = 10;
    public Vector2 FireDirection;
    public GameObject tailPrefab;
    Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = FireDirection * MoveSpeed;
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 direction = rb2d.velocity.normalized;
        rb2d.velocity = direction * MoveSpeed;
        //Instantiate(tailPrefab, transform.position, transform.rotation);
    }
}
