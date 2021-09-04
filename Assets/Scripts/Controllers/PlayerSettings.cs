using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSettings
{
    public float soundVolume = 1, musicVolume = 1, ambientVolume = 1;
    public string language = "PT-BR";
    public bool hasBlood = true, hasPP = true, hasShake = true, isWindowed = false;
    public string resolution = "Default";
}
