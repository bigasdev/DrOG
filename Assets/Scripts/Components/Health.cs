using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    public int maxHp;
    public int currentHp;
    public Health(int max){
        maxHp = max;
        currentHp = max;
    }
    public int Hp{
        get{
            return currentHp;
        }
        set{
            currentHp = value;
            if(currentHp <= 0){
                Debug.Log($"This {this} is dead.");
                return;
            }
        }
    }
}
