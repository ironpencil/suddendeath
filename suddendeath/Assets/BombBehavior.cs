using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {
    public float MoveSpeed;
    public GameObject ExplosionPrefab;
    public GameObject shadow;
    
	// Use this for initialization
	void Start () {

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
            Transform DynamicsParent = Globals.Instance.GetComponent<GameManager>().dynamicsParent;
            GameObject explosion = Instantiate(ExplosionPrefab, DynamicsParent);
            explosion.transform.position = new Vector2(pos.x, pos.y - sr.size.y);

            Destroy(shadow);
            Destroy(gameObject);
        }
    }
}
