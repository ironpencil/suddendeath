using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool invincible = false;
    bool isDead = false;
    public int gamesWon = 0;
    public NotifyController killNotify;

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
            Globals.Instance.GameManager.livingPlayers.Remove(playerNum);          
        }
	}

    public void NotifyOfKill()
    {
        killNotify.Notify();
    }

    public bool Kill()
    {
        if (!invincible) isDead = true;
        return isDead;
    }
}
