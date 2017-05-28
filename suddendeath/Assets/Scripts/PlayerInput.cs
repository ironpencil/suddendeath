﻿using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public float MoveSpeed = 8.0f;
    public int playerNum = 1;
    public bool UseForces = false;
    
    public GameObject hand;
    
    public bool mouseInput = false;

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

        Vector2 rinput;
        if (mouseInput)
        {
            rinput = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rinput.Normalize();
        }
        else {
            // We are going to read the input every frame
            rinput = new Vector2(Input.GetAxisRaw("RHorizontal" + playerNum), Input.GetAxisRaw("RVertical" + playerNum));

        }
        Debug.Log("RHorizontal" + playerNum + ": " + rinput.x);
        Debug.Log("RVertical" + playerNum + ": " + rinput.y);


        //shield.position = (Vector2)transform.position + (rinput * shieldDistance);

        
        // Apply the transform to the object  
        var angle = Mathf.Atan2(rinput.y, rinput.x) * Mathf.Rad2Deg;
        //hand.transform.Rotate(new Vector3(0, 0, angle));
        hand.transform.eulerAngles = new Vector3(0, 0, angle);
        //shield.rotation = angle;
        //shield.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
