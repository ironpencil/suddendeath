using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLineBehavior : MonoBehaviour {
    public int speed;
    public bool isVertical;
    public Rigidbody2D rb2d;
    
    public Vector2 direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rb2d.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        WallBehavior wb = collider.gameObject.GetComponent<WallBehavior>();
        
        if (wb != null)
        {
            speed = -speed;
        } else
        {
            PlayerController pc = collider.gameObject.GetComponent<PlayerController>();

            if (pc != null)
            {
                pc.Kill(PlayerController.KillType.Dissolve);
            }
        }
    }
}
