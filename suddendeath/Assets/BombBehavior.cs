using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {
    public float MoveSpeed;
    public GameObject ExplosionPrefab;
    public GameObject shadow;
    Rigidbody2D rb2d;
    
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        
        // Once we get to the shadow, blow up
        if (shadow.transform.position.y <= transform.position.y - sr.size.y)
        {
            Vector3 moveDirection = new Vector2(0, -MoveSpeed * Time.deltaTime);
            transform.position += moveDirection;
        } else
        {
            Vector3 pos = gameObject.transform.position;
            GameObject explosion = Instantiate(ExplosionPrefab, new Vector3(pos.x, pos.y - sr.size.y, pos.z), gameObject.transform.rotation);
            Destroy(shadow);
            Destroy(gameObject);
        }
    }
}
