using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupBehavior : MonoBehaviour {
    public List<PlayerSelector> playerSelectors;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Display()
    {
        foreach (PlayerSelector ps in playerSelectors)
        {
            ps.playerJoined = false;
            ps.playerStatus.text = "Waiting...";
        }
        
        gameObject.SetActive(true);
    }
}
