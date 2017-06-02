using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class VictoryScreenBehavior : MonoBehaviour {
    public Text VictoryMessage;

    public void Display()
    {
        GameManager gm = Globals.Instance.GetComponent<GameManager>();
        PlayerStats winner = gm.GetWinner();
        VictoryMessage.text = "Player" + winner.playerNum + " Wins!";
        gameObject.SetActive(true);
    }


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 1; i <= 4; i++)
        {
            if (XCI.GetButtonDown(XboxButton.A, (XboxController)i))
            {
                gameObject.SetActive(false);
                Globals.Instance.GameManager.SetupGame();
            }
        }
    }
}
