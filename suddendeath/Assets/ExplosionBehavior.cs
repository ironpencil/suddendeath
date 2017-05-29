using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour {
    public float lifetime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lifetime <= 0)
        {
            Destroy(gameObject);
        } else
        {
            lifetime -= Time.deltaTime;
        }
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController pc = collider.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.Kill();
        }
    }
}
