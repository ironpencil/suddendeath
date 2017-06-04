using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class OptionsScreenBehavior : MonoBehaviour {
    public GameOptions gameOptions;

    // Spinner Menu
    public Toggle spinner;

    public Text spinnerCountText;
    public Slider spinnerCount;

    public Text spinnerSpeedText;
    public Slider spinnerSpeed;

    // Wall Laser (Laser Turret) Menu
    public Toggle wallLaser;

    public Text wallLaserCountText;
    public Slider wallLaserCount;

    public Text wallLaserBounceCountText;
    public Slider wallLaserBounceCount;

    public Text wallLaserShotFrequencyText;
    public Slider wallLaserShotFrequency;

    public Text wallLaserChargeTimeText;
    public Slider wallLaserChargeTime;

    // Bomb Menu
    public Toggle bomb;

    public Text bombFrequencyText;
    public Slider bombFrequency;

    public Text bombFallTimeText;
    public Slider bombFallTime;

    // Mine Menu
    public Toggle mine;
    public Toggle mineStartsArmed;

    public Text mineTimeToDetonateText;
    public Slider mineTimeToDetonate;

    public Text maxMinesText;
    public Slider maxMines;

    public Text mineRespawnFrequencyText;
    public Slider mineRespawnFrequency;

    public Text mineMaxLifetimeText;
    public Slider mineMaxLifetime;

    // Floor Menu
    public Toggle floor;

    public Text floorCollapseDurationText;
    public Slider floorCollapseDuration;
    
	// Use this for initialization
	void Start () {
        // Spinner Handlers
        spinner.onValueChanged.AddListener(val => { gameOptions.isSpinnerEnabled = val; Debug.Log("") });
        spinnerCount.onValueChanged.AddListener(val => { gameOptions.spinnerCount = (int)val; Debug.Log("") });
        spinnerSpeed.onValueChanged.AddListener(val => { gameOptions.spinnerSpeed = (int)val; Debug.Log("") });

        // Wall Laser (Laser Turret) Handlers
        wallLaser.onValueChanged.AddListener(val => { gameOptions.isWallLaserEnabled = val; Debug.Log(""));
        wallLaserCount.onValueChanged.AddListener(val => { gameOptions.wallLaserCount = (int)val; wallLaserCountText.text = "Wall Laser Count: " + (int)val; Debug.Log("") });
        wallLaserBounceCount.onValueChanged.AddListener(val => { gameOptions.wallLaserBounceCount = (int)val; wallLaserBounceCountText.text = "Laser Bounce Count: " + (int)val; Debug.Log("") });
        wallLaserShotFrequency.onValueChanged.AddListener(val => { gameOptions.wallLaserShotFrequency = val; wallLaserShotFrequencyText.text = "Wall Laser Shot Freq: " + val; Debug.Log("") });
        wallLaserChargeTime.onValueChanged.AddListener(val => { gameOptions.wallLaserChargeTime = val; wallLaserChargeTimeText.text = "Wall Laser Charge Time: " + val; Debug.Log("") });

        // Bomb Handlers
        bomb.onValueChanged.AddListener(val => { gameOptions.isBombEnabled = val; Debug.Log("") });
        bombFrequency.onValueChanged.AddListener(val => { gameOptions.bombFrequency = val; bombFrequencyText.text = "Bomb Frequency: " + val; Debug.Log("") });
        bombFallTime.onValueChanged.AddListener(val => { gameOptions.bombFallTime = val; bombFallTimeText.text = "Bomb Fall Time: " + val; Debug.Log("") });

        // Mine Handlers
        mine.onValueChanged.AddListener(val => { gameOptions.isMineEnabled = val; bombFallTimeText.text = "Bomb Fall Time: " + val; Debug.Log("") });
        mineStartsArmed.onValueChanged.AddListener(val => { gameOptions.mineStartsArmed = val; bombFallTimeText.text = "Bomb Fall Time: " + val; Debug.Log("") });
        mineTimeToDetonate.onValueChanged.AddListener(val => { gameOptions.mineTimeToDetonate = (int)val; bombFallTimeText.text = "Bomb Fall Time: " + val; Debug.Log("") });
        maxMines.onValueChanged.AddListener(val => { gameOptions.maxMines = (int)val; bombFallTimeText.text = "Bomb Fall Time: " + (int)val; Debug.Log(""));
        mineRespawnFrequency.onValueChanged.AddListener(val => { gameOptions.mineRespawnFrequency = (int)val; bombFallTimeText.text = "Bomb Fall Time: " + (int)val; Debug.Log("") });
        mineMaxLifetime.onValueChanged.AddListener(val => { gameOptions.mineMaxLifetime = (int)val; bombFallTimeText.text = "Bomb Fall Time: " + (int)val; Debug.Log("") });

        // Floor Handlers
        floor.onValueChanged.AddListener(val => { gameOptions.isFloorEnabled = val; Debug.Log("") });
        floorCollapseDuration.onValueChanged.AddListener(val => { gameOptions.floorCollapseDuration = (int)val; Debug.Log("") });
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
}
