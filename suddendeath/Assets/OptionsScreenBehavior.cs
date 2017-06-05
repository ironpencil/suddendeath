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

    //public Text wallLaserChargeTimeText;
    //public Slider wallLaserChargeTime;

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
        // Spinner
        //   -- Handlers
        spinner.onValueChanged.AddListener(val => { gameOptions.isSpinnerEnabled = val; Debug.Log("Spinner Enabled: " + val); });
        spinnerCount.onValueChanged.AddListener(val => { gameOptions.spinnerCount = (int)val; spinnerCountText.text = "Spinner Count: " + (int)val; Debug.Log("Spinner Count: " + (int)val); });
        spinnerSpeed.onValueChanged.AddListener(val => { gameOptions.spinnerSpeed = (int)val; spinnerSpeedText.text = "Spinner Speed: " + (int)val; Debug.Log("Spinner Speed: " + (int)val); });

        //   -- Defaults
        spinner.isOn = gameOptions.isSpinnerEnabled;
        spinnerCount.value = gameOptions.spinnerCount;
        spinnerCountText.text = "Spinner Count: " + gameOptions.spinnerCount;
        spinnerSpeed.value = gameOptions.spinnerSpeed;
        spinnerSpeedText.text = "Spinner Speed: " + gameOptions.spinnerSpeed;




        // Wall Laser (Laser Turret) Handlers
        //   -- Handlers
        wallLaser.onValueChanged.AddListener(val => { gameOptions.isWallLaserEnabled = val; Debug.Log("Wall Turret Enabled: " + val); });
        wallLaserCount.onValueChanged.AddListener(val => { gameOptions.wallLaserCount = (int)val; wallLaserCountText.text = "Wall Turret Count: " + (int)val; Debug.Log("Wall Turret Count: " + (int)val); });
        wallLaserShotFrequency.onValueChanged.AddListener(val => { gameOptions.wallLaserShotFrequency = val; wallLaserShotFrequencyText.text = "Turret Shot Freq: " + val; Debug.Log("Wall Turret Shot Freq: " + val); });
        //wallLaserChargeTime.onValueChanged.AddListener(val => { gameOptions.wallLaserChargeTime = val; wallLaserChargeTimeText.text = "Wall Turret Charge Time: " + val; Debug.Log("Wall Turret Charge Time: " + val); });
        wallLaserBounceCount.onValueChanged.AddListener(val => { gameOptions.wallLaserBounceCount = (int)val; wallLaserBounceCountText.text = "Laser Bounces: " + (int)val; Debug.Log("Laser Bounces: " + (int)val); });

        //   -- Defaults
        wallLaser.isOn = gameOptions.isWallLaserEnabled;
        wallLaserCount.value = gameOptions.wallLaserCount;
        wallLaserCountText.text = "Wall Turret Count: " + gameOptions.wallLaserCount;
        wallLaserShotFrequency.value = gameOptions.wallLaserShotFrequency;
        wallLaserShotFrequencyText.text = "Turret Shot Freq: " + gameOptions.wallLaserShotFrequency;
        wallLaserBounceCount.value = gameOptions.wallLaserBounceCount;
        wallLaserBounceCountText.text = "Laser Bounces: " + gameOptions.wallLaserBounceCount;




        // Bomb Handlers
        //   -- Handlers
        bomb.onValueChanged.AddListener(val => { gameOptions.isBombEnabled = val; Debug.Log("Bomb Enabled: " + val); });
        bombFrequency.onValueChanged.AddListener(val => { gameOptions.bombFrequency = val; bombFrequencyText.text = "Bomb Freq: " + val; Debug.Log("Bomb Freq: " + val); });
        bombFallTime.onValueChanged.AddListener(val =>  { gameOptions.bombFallTime = val;  bombFallTimeText.text = "Fall Time: " + val;  Debug.Log("Bomb Fall Time: " + val); });

        //   -- Defaults
        bomb.isOn = gameOptions.isBombEnabled;
        bombFrequency.value = gameOptions.bombFrequency;
        bombFrequencyText.text = "Bomb Freq: " + gameOptions.bombFrequency;
        bombFallTime.value = gameOptions.bombFallTime;
        bombFallTimeText.text = "Fall Time: " + gameOptions.bombFallTime;




        // Mine Handlers
        //   -- Handlers
        mine.onValueChanged.AddListener(val => { gameOptions.isMineEnabled = val; Debug.Log("Mine Enabled: " + val); });
        mineStartsArmed.onValueChanged.AddListener(val => { gameOptions.mineStartsArmed = val; Debug.Log("Mines Start Armed: " + val); });
        mineTimeToDetonate.onValueChanged.AddListener(val => { gameOptions.mineTimeToDetonate = (int)val; mineTimeToDetonateText.text = "Mine Time to Detonate: " + val; Debug.Log("Mine Time to Detonate: " + val); });
        maxMines.onValueChanged.AddListener(val => { gameOptions.maxMines = (int)val; maxMinesText.text = "Max Mines: " + (int)val; Debug.Log("Max Mines: " + (int)val); });
        mineRespawnFrequency.onValueChanged.AddListener(val => { gameOptions.mineRespawnFrequency = val; mineRespawnFrequencyText.text = "Mine Respawn Freq: " + val; Debug.Log("Mine Respawn Freq: " + val); });
        mineMaxLifetime.onValueChanged.AddListener(val => { gameOptions.mineMaxLifetime = val; mineMaxLifetimeText.text = "Mine Max Lifetime: " + val; Debug.Log("Mine Max Lifetime: " + val); });

        //   -- Defaults
        mine.isOn = gameOptions.isMineEnabled;
        mineStartsArmed.isOn = gameOptions.mineStartsArmed;
        mineTimeToDetonate.value = gameOptions.mineTimeToDetonate;
        mineTimeToDetonateText.text = "Mine Time to Detonate: " + gameOptions.mineTimeToDetonate;
        maxMines.value = gameOptions.maxMines;
        maxMinesText.text = "Max Mines: " + gameOptions.maxMines;
        mineRespawnFrequency.value = gameOptions.mineRespawnFrequency;
        mineRespawnFrequencyText.text = "Mine Respawn Freq: " + gameOptions.mineRespawnFrequency;
        mineMaxLifetime.value = gameOptions.mineMaxLifetime;
        mineMaxLifetimeText.text = "Mine max Lifetime: " + gameOptions.mineMaxLifetime;
        




        // Floor Handlers
        floor.onValueChanged.AddListener(val => { gameOptions.isFloorEnabled = val; Debug.Log("Floor Enabled: " + val); });
        //   -- Handlers
        floorCollapseDuration.onValueChanged.AddListener(val => { gameOptions.floorCollapseDuration = val; floorCollapseDurationText.text = "Floor Collapse Dur: " + val; Debug.Log("Floor Collapse Duration: " + val); });

        //   -- Defaults
        floor.isOn = gameOptions.isFloorEnabled;
        floorCollapseDuration.value = gameOptions.floorCollapseDuration;
        floorCollapseDurationText.text = "Floor Collapse Dur: " + gameOptions.floorCollapseDuration;
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
