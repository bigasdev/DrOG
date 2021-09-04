using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public int bullets;
    public float fireRate;
    public float accuracy;
    public int weaponNumber;
    public Weapon(string name, int b, float fireR, float acc, int number){
        weaponName = name;
        bullets = b;
        fireRate = fireR;
        accuracy = acc;
        weaponNumber = number;
    }
}
