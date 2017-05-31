using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ScoreScreenBehavior : MonoBehaviour {
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (XCI.GetButtonDown(XboxButton.A))
        {
            Debug.Log("A button pressed");
            Globals.Instance.gameObject.GetComponent<GameManager>().isRoundReady = true;
        }
    }
}
