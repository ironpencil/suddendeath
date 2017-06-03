using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class PlayerSelector : MonoBehaviour {

    public int playerNum = 1;
    public bool playerJoined = false;

    public Text playerStatus;

    XboxController controller;

	// Use this for initialization
	void Start () {
        controller = (XboxController)playerNum;
    }
	
	// Update is called once per frame
	void Update () {
        if (XCI.GetButtonDown(XboxButton.A, controller))
        {
            if (!playerJoined)
            {
                playerJoined = true;
                Globals.Instance.GameManager.AddPlayer(playerNum);
                if (playerStatus != null)
                {
                    playerStatus.text = "Joined! Press B to cancel...";
                }
            }
        }

        if (XCI.GetButtonDown(XboxButton.B, controller)) {
            if (playerJoined)
            {
                playerJoined = false;
                Globals.Instance.GameManager.RemovePlayer(playerNum);
                if (playerStatus != null)
                {
                    playerStatus.text = "Waiting...";
                }
            }
        }

        if (XCI.GetButtonDown(XboxButton.Start, controller))
        {
            if (playerJoined)
            {
                //start game!
                Globals.Instance.GameManager.StartRound();
            }
        }
	}
}
