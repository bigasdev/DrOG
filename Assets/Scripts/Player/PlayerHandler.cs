using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerHandler
{
    public static Player GetPlayerInstance(){
        return Player.Instance;
    }
    public static void ChangePlayerSprite(string name){
        Debug.Log("Trying to load + " + name);
        var sprites = Resources.LoadAll<Sprite>("Assets/Sprites/Player/Boneco");
        Sprite sprite = null;
        foreach(var s in sprites){
            if(s.name == name){
                sprite = s;
            }
        }
        Player.Instance.spriteRenderer.sprite = sprite;
    }
    public static void ChangePlayerWeapon(Weapon _weapon){
        if(GetPlayerInstance().weaponItem != null && GetPlayerInstance().weaponItem.weaponName != "knife"){
            var weapon = Resources.Load<WeaponCollectable>("Prefabs/Collectables/WeaponCollectable");
            var w = Object.Instantiate(weapon);
            w.weapon = GetPlayerInstance().weaponItem;
            w.Initialize();
            w.transform.position = GetPlayerInstance().transform.position;
        }
        Object.FindObjectOfType<PlayerHud>().SetGunSprite(_weapon.weaponName);
        GetPlayerInstance().weaponItem = null;
        GetPlayerInstance().weaponItem = _weapon;
    }
    public static void SpawnRestartScreen(){
        var screen = Resources.Load<GameObject>("Prefabs/Views/RestartScreen");
        Object.Instantiate(screen);
    }
}
