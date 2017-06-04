using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  GameOptions  {
    // Spinner Options
    public bool isSpinnerEnabled = true;
    public int spinnerCount = 1;
    public int moveSpeed = 16;

    // Laser Turret (Wall Laser) Options
    public bool isWallLaserEnabled = true;
    public int wallLaserCount = 1;
    public int laserBounceCount = 4;
    public float shotFrequency = 0.5f;
    public float chargeTime = 0.5f;

    // Bomb Options
    public bool isBombEnabled = true;
    public float bombFrequency = 1.5f;
    public float fallTime = 3.0f;

    // Mine Options
    public bool isMineEnabled = true;
    public bool startArmed = false;
    public int armedTime = 1;
    public int maxMines = 1;
    public int respawnFrequency = 3;
    public int maxLifetime = 10;
    
    // Floor Options
    public bool isFloorEnabled = true;
    public float collapseDuration = 1.5f;
}
