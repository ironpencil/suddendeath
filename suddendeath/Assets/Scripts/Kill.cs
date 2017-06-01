﻿using UnityEngine;
using UnityEditor;

public class Kill
{
    public int killerPlayerNum;
    public int victimPlayerNum;
    public Weapon weapon;
    public enum Weapon { Mine, Bomb, Laser }

    public Kill(int killerPlayerNum, int victimPlayerNum, Weapon weapon)
    {
        this.killerPlayerNum = killerPlayerNum;
        this.victimPlayerNum = victimPlayerNum;
        this.weapon = weapon;
    }
}