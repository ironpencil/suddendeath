using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class OptionsScreenBehavior : MonoBehaviour {
    public GameOptions gameOptions;

    public Toggle spinner;
    public Slider spinnerCount;
    public Toggle wallLaser;
    public Toggle bomb;
    public Toggle mine;
    public Toggle floor;
    
	// Use this for initialization
	void Start () {
        spinner.onValueChanged.AddListener(ToggleSpinner);
        spinnerCount.onValueChanged.AddListener(OnChangeSliderCount);
        wallLaser.onValueChanged.AddListener(ToggleWallLaser);
        bomb.onValueChanged.AddListener(ToggleBomb);
        mine.onValueChanged.AddListener(ToggleMine);
        floor.onValueChanged.AddListener(ToggleFloor);
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
        gameOptions.isSpinnerEnabled = value;
    }

    public void OnChangeSliderCount(int value)
    {
        gameOptions.spinnerCount = value;
    }

    public void ToggleWallLaser(bool value)
    {
        gameOptions.isWallLaserEnabled = value;
    }

    public void ToggleBomb(bool value)
    {
        gameOptions.isBombEnabled = value;
    }

    public void ToggleMine(bool value)
    {
        gameOptions.isMineEnabled = value;
    }

    public void ToggleFloor(bool value)
    {
        gameOptions.isFloorEnabled = value;
    }
}
