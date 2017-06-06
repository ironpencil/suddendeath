using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  GameOptions : MonoBehaviour {
    // Not Yet Implemented
    [Header("General")]
    public bool displayFrameRate = true;
    public int roundsToWin = 10;

    [Header("Player")]
    public float playerMoveSpeed = 5000f;
    public float playerDashRechargeTime = 0.8f;
    public float playerDashTime = 0.15f;
    public float playerDashSpeed = 20000;
    public bool playerLockDashDirection = true;

    // Spinner Options
    [Header("Spinner")]
    public bool isSpinnerEnabled = true;
    public int spinnerCount = 1;
    public int spinnerSpeed = 16;

    // Laser Turret (Wall Laser) Options
    [Header("Wall Turret")]
    public bool isWallLaserEnabled = true;
    public int wallLaserCount = 1;
    public float wallLaserShotFrequency = 1.0f;
    public float wallLaserChargeTime = 0.5f;
    public int wallLaserBounceCount = 3;
    public float wallLaserSpeed = 15;

    [Header("Laser Line")]
    public bool isLaserLineEnabled = true;
    public int laserLineCount = 3;
    public float laserLineMaxSpeed = 4;
    public float laserLineMinSpeed = 2;

    // Bomb Options
    [Header("Bomb")]
    public bool isBombEnabled = true;
    public float bombFrequency = 1.5f;
    public float bombFallTime = 3.0f;

    // Mine Options
    [Header("Mine")]
    public bool isMineEnabled = true;
    public bool mineStartsArmed = false;
    public int mineTimeToDetonate = 3;
    public int maxMines = 2;
    public float mineRespawnFrequency = 3.0f;
    public float mineMaxLifetime = 10.0f;
    
    // Floor Options
    [Header("Floor")]
    public bool isFloorEnabled = true;
    public float floorCollapseDuration = 1.5f;
}
