using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProjectileTailController : MonoBehaviour {
    public float Lifetime = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Lifetime -= Time.deltaTime;
        if (Lifetime <= 0)
        {
            Destroy(this);
        }
	}
}
