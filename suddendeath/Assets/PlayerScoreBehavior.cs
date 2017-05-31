using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class PlayerScoreBehavior : MonoBehaviour {
    public int playerNum = 1;
    public Text scoreText;
    private GameManager gm;
    XboxController controller;

    // Use this for initialization
    void Start () {
        gm = Globals.Instance.gameObject.GetComponent<GameManager>();
        controller = (XboxController)playerNum;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("PlayerScoreBehavior update called");
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

        if (XCI.GetButtonDown(XboxButton.A, controller))
        {
            Globals.Instance.GameManager.StartRound();
        }
    }
}
