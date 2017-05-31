using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreBehavior : MonoBehaviour {
    public int playerNum = 1;
    public Text scoreText;
    private GameManager gm;

    // Use this for initialization
    void Start () {
        gm = Globals.Instance.gameObject.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gm.playerStats.Count >= playerNum)
        {
            PlayerStats ps = gm.playerStats[playerNum];
            scoreText.text = 
                "Player " + playerNum + 
                "\nWins: " + ps.wins + 
                "\nSurvival Time: " + Math.Round(ps.survivalTime, 1) + " seconds" +
                "\nBomb Targets: " + ps.bombTargets;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
