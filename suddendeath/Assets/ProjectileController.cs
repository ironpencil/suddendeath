using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    public float MoveSpeed = 10;
    public Vector2 FireDirection;
    public int MaxWallBounceCount = 4;
    private int CurrentWallBounceCount = 0;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.Kill();
            Destroy(gameObject);
        } else if (collision.gameObject.GetComponent<WallBehavior>() != null)
        {
            CurrentWallBounceCount++;
            if (CurrentWallBounceCount >= MaxWallBounceCount)
            {
                Destroy(gameObject);
            }
        }
    }
}
