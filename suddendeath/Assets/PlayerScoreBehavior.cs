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
        gm = Globals.Instance.GetComponent<GameManager>();
        controller = (XboxController)playerNum;
    }
    
    // Update is called once per frame
    void Update () {
        if (XCI.GetButtonDown(XboxButton.A, controller))
        {
            gm.StartRound();
        }
    }
}
