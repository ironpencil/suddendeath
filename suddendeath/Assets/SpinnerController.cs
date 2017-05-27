using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour {
    public float MoveSpeed = 8.0f;
    public float SpinSpeed = 8.0f;
    public float MaxAngularVelocity = 4.0f;
    public float SpinDirection = 1;
    private Collider2D lastCollider;

    Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(Random.Range(-1.0f, 1.0f) * MoveSpeed, Random.Range(-1.0f, 1.0f) * MoveSpeed);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 direction = rb2d.velocity.normalized;
        rb2d.velocity = direction * MoveSpeed;

        rb2d.angularVelocity = 1200 * SpinDirection;
        //rb2d.AddTorque(SpinSpeed * SpinDirection);
        //rb2d.rotation += SpinSpeed * SpinDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpinDirection = -SpinDirection;
        //rb2d.angularVelocity *= -1;

        /*
        if (lastCollider != collision.collider) {
            rb2d.rotation = -rb2d.rotation;
            SpinDirection = -SpinDirection;
            lastCollider = collision.collider;
        }
        */
    }
}
