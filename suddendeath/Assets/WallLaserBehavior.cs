﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLaserBehavior : MonoBehaviour {
    public float MoveSpeed = 10;
    public bool IsVertical = true;
    public bool IsCharging = false;
    public float shotFrequency = 10;
    public float chargeTime = 10;
    public Sprite IdleSprite;
    public Sprite ChargingSprite;
    Rigidbody2D rb2d;
    public SpriteRenderer sprite;
    public float timeLeft;
    public GameObject projectilePrefab;
    public Vector2 facing;
    public Vector2 maxSpawn;
    public Vector2 minSpawn;
    public Transform firingPosition;
    
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        timeLeft = shotFrequency;
        if (Random.value > 0.5f) MoveSpeed = -MoveSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 moveDirection;
        if (IsVertical)
        {
            moveDirection = new Vector2(0, MoveSpeed);
        } else
        {
            moveDirection = new Vector2(MoveSpeed, 0);
        }

        rb2d.velocity = moveDirection;
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            if (IsCharging)
            {
                ToggleCharging();
                FireWeapon();
                timeLeft = shotFrequency;
            } else
            {
                ToggleCharging();
                timeLeft = chargeTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        MoveSpeed = -MoveSpeed;
    }

    private void ToggleCharging()
    {
        if (IsCharging)
        {
            IsCharging = false;
            sprite.sprite = IdleSprite;
        }
        else
        {
            IsCharging = true;
            sprite.sprite = ChargingSprite;
        }
    }

    private void FireWeapon()
    {
        Transform DynamicsParent = Globals.Instance.GetComponent<GameManager>().dynamicsParent;
        GameObject laser = Instantiate(projectilePrefab, DynamicsParent);
        laser.transform.localPosition = firingPosition.localPosition;
        laser.transform.rotation = transform.rotation;
        laser.GetComponent<ProjectileController>().FireDirection = facing;
    }
}
