using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  GameOptions  {
    // Spinner Options
    public bool isSpinnerEnabled = true;
    public int spinnerCount = 1;
    public int spinnerSpeed = 16;

    // Laser Turret (Wall Laser) Options
    public bool isWallLaserEnabled = true;
    public int wallLaserCount = 1;
    public int wallLaserBounceCount = 4;
    public float wallLaserShotFrequency = 0.5f;
    public float wallLaserChargeTime = 0.5f;

    // Bomb Options
    public bool isBombEnabled = true;
    public float bombFrequency = 1.5f;
    public float bombFallTime = 3.0f;

    // Mine Options
    public bool isMineEnabled = true;
    public bool mineStartsArmed = false;
    public int mineTimeToDetonate = 1;
    public int maxMines = 1;
    public int mineRespawnFrequency = 3;
    public int mineMaxLifetime = 10;
    
    // Floor Options
    public bool isFloorEnabled = true;
    public float floorCollapseDuration = 1.5f;
}
