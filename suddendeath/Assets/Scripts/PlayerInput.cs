﻿using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    private float CurrentSpeed;
    public float MoveSpeed = 8.0f;
    public int PlayerNum = 1;
    public bool UseForces = false;
    public Sprite PlayerSprite;

    //Dashing vars
    public float DashRechargeTime = 5.0f;
    public float DashTime = 1.0f;
    public float DashSpeed = 1.0f;
    public Sprite DashingSprite;
    public bool IsDashing = false;
    public bool LockDashDirection = true;
    private float DashRechardTimeLeft = 0.0f;
    private float DashTimeLeft = 0.0f;
    private float LastHorizontal = 0.0f;
    private float LastVertical = 0.0f;

    public GameObject hand;
    public GameObject sprite;
    
    public bool mouseInput = false;

    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        CurrentSpeed = MoveSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (Globals.Instance.acceptPlayerGameInput)
        {
            HandleDash();
            HandleMovement();
            HandleShieldMovement();
            HandleAttack();
        }
	}

    private void HandleAttack()
    {
        
    }

    void HandleDash()
    {
        // TODO Move the character quickly one square, ignoring hazards and other characters
        // Recharges over time
        DashRechardTimeLeft -= Time.deltaTime;

        if (Input.GetButton("Dash" + PlayerNum))
        {
            if (DashRechardTimeLeft <= 0)
            {
                BeginDash();
            } else
            {
                Debug.Log("Can't dash, let player know here");
            }
        } else if (IsDashing)
        {
            EndDash();
        }
    }

    void BeginDash()
    {
        IsDashing = true;
        DashTimeLeft = DashTime;
        SpriteRenderer sr = sprite.GetComponent<SpriteRenderer>();
        sr.sprite = DashingSprite;
        CurrentSpeed = DashSpeed;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    void EndDash()
    {
        IsDashing = false;
        DashTimeLeft = 0.0f;
        DashRechardTimeLeft = DashRechargeTime;
        SpriteRenderer sr = sprite.GetComponent<SpriteRenderer>();
        sr.sprite = PlayerSprite;
        CurrentSpeed = MoveSpeed;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    void HandleMovement()
    {
        if (IsDashing)
        {
            DashTimeLeft -= Time.deltaTime;
            if (DashTimeLeft <= 0)
            {
                EndDash();
            }
        }

        float horizontal = 0.0f;
        float vertical = 0.0f;

        if (LockDashDirection && IsDashing)
        {
            horizontal = LastHorizontal;
            vertical = LastVertical;
        } else
        {
            horizontal = Input.GetAxisRaw("Horizontal" + PlayerNum);
            vertical = Input.GetAxisRaw("Vertical" + PlayerNum);
            LastHorizontal = horizontal;
            LastVertical = vertical;
        }

        Vector2 moveDirection = new Vector2(horizontal * CurrentSpeed, vertical * CurrentSpeed);

        if (UseForces)
        {
            rb2d.AddForce(moveDirection, ForceMode2D.Force);
        }
        else
        {
            rb2d.velocity = moveDirection;
        }
    }

    void HandleShieldMovement()
    {
        if (!IsDashing)
        {
            Vector2 rinput;
            if (mouseInput)
            {
                rinput = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rinput.Normalize();
            }
            else
            {
                // We are going to read the input every frame
                rinput = new Vector2(Input.GetAxisRaw("RHorizontal" + PlayerNum), Input.GetAxisRaw("RVertical" + PlayerNum));
            }

            // Apply the transform to the object  
            var angle = Mathf.Atan2(rinput.y, rinput.x) * Mathf.Rad2Deg;
            hand.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
