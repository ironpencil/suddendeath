using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreenBehavior : MonoBehaviour {
    public GameOptions gameOptions;

    public Toggle spinner;
    public Toggle wallLaser;
    
	// Use this for initialization
	void Start () {
        spinner.onValueChanged.AddListener(ToggleSpinner);
        wallLaser.onValueChanged.AddListener(ToggleWallLaser);
    }

    // Update is called once per frame
    void Update()
    {
        if (XCI.GetButtonDown(XboxButton.Back, controller))
        {
            Globals.Instance.GameManager.DisplayOptions();
        }
    }

    public void ToggleSpinner(bool value)
    {
        gameOptions.IsSpinnerEnabled = value;
    }

    public void ToggleWallLaser(bool value)
    {
        gameOptions.IsWallLaserEnabled = value;
    }
}
