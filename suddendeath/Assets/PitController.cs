using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitController : MonoBehaviour {

    public Collider2D pitCollider;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivatePit(bool active)
    {
        pitCollider.enabled = active;
    }
}
