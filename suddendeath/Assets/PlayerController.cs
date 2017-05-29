using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    bool isDead = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead)
        {
            int playerNum = gameObject.GetComponent<PlayerInput>().PlayerNum;
            Destroy(gameObject);
            Globals.Instance.GameManager.players.Remove(playerNum - 1); //todo: clean this up            
        }
	}

    public void Kill()
    {
        isDead = true;
    }
}
