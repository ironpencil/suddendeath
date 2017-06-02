using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour {
    public float MoveSpeed = 8.0f;
    public float SpinSpeed = 8.0f;
    public float PushForce = 10.0f;
    public float SpinDirection = 1;
    public float minPushPercent = 0.5f;
    private Collider2D lastCollider;
    private Dictionary<Rigidbody2D, Vector2> pushes = new Dictionary<Rigidbody2D, Vector2>();

    Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(Random.Range(-1.0f, 1.0f) * MoveSpeed, Random.Range(-1.0f, 1.0f) * MoveSpeed);
    }
	
	void FixedUpdate () {
        HandleMovement();

        foreach (Rigidbody2D rb in pushes.Keys)
        {
            rb.AddForce(pushes[rb], ForceMode2D.Impulse);
        }

        pushes.Clear();
    }

    void HandleMovement()
    {
        Vector2 direction = rb2d.velocity.normalized;
        rb2d.velocity = direction * MoveSpeed;
        rb2d.angularVelocity = 1200 * SpinDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpinDirection = -SpinDirection;
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.Kill();
        } else if (collision.collider.transform.name.Equals("Shield"))
        {
            // Cache push direction so it happens in the next frame, that way
            // forces are applied to the spinner naturally
            // TODO Add li'l ass smacker cheevo for hitting him from behind
            Rigidbody2D playerRigidBody = collision.collider.transform.parent.parent.gameObject.GetComponent<Rigidbody2D>();
            Vector2 pushDirection = playerRigidBody.transform.position - transform.position;
            float pushModifier = Mathf.Max(Vector2.Dot(pushDirection.normalized, rb2d.velocity.normalized), 0);
            pushModifier = minPushPercent + (pushModifier * (1 - minPushPercent));
            pushes[playerRigidBody] = pushDirection.normalized * PushForce * pushModifier;
        }
    }
}
