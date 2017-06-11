using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class PlayerSelector : MonoBehaviour {

    public int playerNum = 1;
    public bool playerJoined = false;
    public Image AButton;
    public Image BButton;

    public SoundEffectHandler confirmSound;
    public SoundEffectHandler cancelSound;
    
    public Text playerStatus;

    XboxController controller;

    public void ResetSelector()
    {
        AButton.enabled = true;
        BButton.enabled = false;
        playerStatus.text = "Press       to join!";
        playerJoined = false;
        Globals.Instance.GameManager.RemovePlayer(playerNum);
    }

    // Use this for initialization
    void Start () {
        BButton.enabled = false;
        controller = (XboxController)playerNum;
    }
	
	// Update is called once per frame
	void Update () {
        if (playerJoined && !Globals.Instance.GameManager.joinedPlayers.Contains(playerNum))
        {
            AButton.enabled = true;
            BButton.enabled = false;
            playerStatus.text = "Press       to join!";
        }

        if (XCI.GetButtonDown(XboxButton.A, controller))
        {
            if (!playerJoined)
            {
                confirmSound.PlayEffect();
                playerJoined = true;
                Globals.Instance.GameManager.AddPlayer(playerNum);
                if (playerStatus != null)
                {
                    AButton.enabled = false;
                    BButton.enabled = true;
                    playerStatus.text = "Joined! Press       to cancel...";
                }
            }
        }

        if (XCI.GetButtonDown(XboxButton.B, controller)) {
            if (playerJoined)
            {
                cancelSound.PlayEffect();
                ResetSelector();
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

        if (XCI.GetButtonDown(XboxButton.Back, controller))
        {
            Globals.Instance.GameManager.DisplayOptions();
        }
	}
}
