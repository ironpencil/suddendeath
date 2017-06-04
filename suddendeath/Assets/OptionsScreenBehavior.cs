using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

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
        for (int player = 1; player < 5; player++)
        {
            if (XCI.GetButtonDown(XboxButton.Back, (XboxController)player))
            {
                Globals.Instance.GameManager.DisplayPlayerSetup();
            }
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
