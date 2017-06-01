using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWinsBehavior : MonoBehaviour {
    GameManager gm;
	// Use this for initialization
	void Start () {
        gm = Globals.Instance.gameObject.GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {

    }
}
