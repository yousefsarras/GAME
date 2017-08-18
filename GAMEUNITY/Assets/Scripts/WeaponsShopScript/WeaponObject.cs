using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponObject : ScriptableObject {

    public string weaponName = "Weapon Name Here";
    public int cost = 0;
    public string description;

    public int aD = 0;
    public int health = 0;
    public int mana = 0;
    public int moveSpeed = 0;
    public int defense = 0;
    public Sprite icon;
}
