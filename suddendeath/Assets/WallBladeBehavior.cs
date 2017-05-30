using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBladeBehavior : MonoBehaviour {
    public float WallSpeed = .001f;
    public float MoveSpeed = 1f;
    public float SpinSpeed = 8.0f;
    public float WallOffset = 2.0f;
    public float TriggerRange = 0.5f;
    public Vector2 Direction = Vector2.zero;
    public GameObject currentWall = null;
    Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        // Move along the wall
        // Check for a player
		if (currentWall != null)
        {
            gameObject.transform.position += (Vector3)Direction * WallSpeed;
            foreach (PlayerController pc in Globals.Instance.gameObject.GetComponent<GameManager>().players.Values)
            {
                float pcx = pc.transform.position.x;
                float wbx = gameObject.transform.position.x;
                float pcy = pc.transform.position.y;
                float wby = gameObject.transform.position.y;
                
                //Debug.Log("pcx (" + pcx + ") tween: " + (wbx - TriggerRange) + " and " + (wbx + TriggerRange));
                //Debug.Log("pcy (" + pcy + ") tween: " + (wby - TriggerRange) + " and " + (wby + TriggerRange));

                if ((wbx + TriggerRange >= pcx && wbx - TriggerRange <= pcx)
                    || (wby + TriggerRange >= pcy && wby - TriggerRange <= pcy))
                {
                    EjectFromWall();
                }
            }
            // Move that direction
        }
	}

    private void FixedUpdate()
    {
        if (currentWall == null)
        {
            
            rb2d.velocity = rb2d.velocity.normalized * MoveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<WallBehavior>() != null)
        {
            currentWall = collision.collider.gameObject;
            LockOntoWall(collision.collider.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<WallBehavior>() != null && collider.gameObject != currentWall)
        {
            LockOntoWall(collider.gameObject);
        }
    }

    private void EjectFromWall()
    {
        // Begin ignoring physics
        rb2d.isKinematic = false;
        gameObject.GetComponent<CircleCollider2D>().isTrigger = false;

        // On a wide wall
        if (currentWall.transform.localScale.x > currentWall.transform.localScale.y)
        {
            if (currentWall.transform.position.y > 0)
            {
                gameObject.transform.position += new Vector3(0, -1, 0);
                Direction = new Vector2(0, -1);
            } else
            {
                gameObject.transform.position += new Vector3(0, 1, 0);
                Direction = new Vector2(0, 1);
            }
        }
        // Tall Wall
        else
        {
            if (currentWall.transform.position.x > 0)
            {
                gameObject.transform.position += new Vector3(-1, 0, 0);
                Direction = new Vector2(-1, 0);
            }
            else
            {
                gameObject.transform.position += new Vector3(1, 0, 0);
                Direction = new Vector2(1, 0);
            }
        }
        
        currentWall = null;
        rb2d.velocity = Direction * MoveSpeed;
    }

    private void LockOntoWall(GameObject targetWall)
    {
        // Begin ignoring physics
        rb2d.isKinematic = true;
        rb2d.velocity = Vector2.zero;
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        
        float currentPos = 0.0f;
        float targetPos = 0.0f;
        bool wideWall = true;

        // Collided with a Wide wall
        if (targetWall.transform.localScale.x > targetWall.transform.localScale.y)
        {
            currentPos = currentWall.transform.position.x;
            targetPos = targetWall.transform.position.x;
        }
        // Tall Wall
        else
        {
            wideWall = false;
            currentPos = currentWall.transform.position.y;
            targetPos = targetWall.transform.position.y;
        }

        // We know we're on a Wide Wall, so we must have run into a tall wall
        // If we're on the low end of the wall, go up, otherwise, go down
        int dir = Random.Range(-1, 1);
        if (currentWall != null)
        {
            dir = 1;
            if (currentPos > targetPos)
            {
                dir = -1;
            }
            // We're not on a wall already so pick any random direction
        }

        if (wideWall)
        {
            Direction = new Vector2(dir, 0);
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, targetWall.transform.position.y);
        } else
        {
            Direction = new Vector2(0, dir);
            gameObject.transform.position = new Vector2(targetWall.transform.position.x, gameObject.transform.position.y);
        }

        currentWall = targetWall;
    }
}
