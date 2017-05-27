using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour {
    public float MoveSpeed = 8.0f;
    public float SpinSpeed = 8.0f;
    public float MaxAngularVelocity = 4.0f;

    Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(Random.Range(-1.0f, 1.0f) * MoveSpeed, Random.Range(-1.0f, 1.0f) * MoveSpeed);
    }
	
	// Update is called once per frame
	void Update () {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 direction = rb2d.velocity.normalized;
        rb2d.velocity = direction * MoveSpeed;
        if (rb2d.angularVelocity < MaxAngularVelocity)
        {
            rb2d.AddTorque(SpinSpeed);
        }
    }
}
