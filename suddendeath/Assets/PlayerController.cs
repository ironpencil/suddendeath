using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    bool isDead = false;
    public int gamesWon = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead)
        {
            int playerNum = gameObject.GetComponent<PlayerInput>().PlayerNum;
            Globals.Instance.GameManager.playerStats[playerNum].survivalTime += Time.time - Globals.Instance.GameManager.roundStartTime;
            Destroy(gameObject);
            Globals.Instance.GameManager.players.Remove(playerNum);          
        }
	}

    public void Kill()
    {
        isDead = true;
    }
}
