using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public float MoveSpeed = 8.0f;
    public int playerNum = 1;
    public bool UseForces = false;

    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();    
	}
	
	// Update is called once per frame
	void Update () {

        if (Globals.Instance.acceptPlayerGameInput)
        {
            HandleMovement();
            HandleAttack();
        }
	
	}

    private void HandleAttack()
    {
        
    }

    void HandleMovement()
    {
        float horizontal = 0.0f;
        float vertical = 0.0f;

        horizontal = Input.GetAxisRaw("Horizontal" + playerNum);
        vertical = Input.GetAxisRaw("Vertical" + playerNum);

        Vector2 moveDirection = new Vector2(horizontal * MoveSpeed, vertical * MoveSpeed);

        if (UseForces)
        {
            rb2d.AddForce(moveDirection, ForceMode2D.Force);
        } else
        {
            rb2d.velocity = moveDirection;
        }
        
    }
}
