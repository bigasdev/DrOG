using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHud : MonoBehaviour
{
    private const string hudFiles = "Assets/Hud/MainMenu/drogMainMenu";
    [SerializeField] GameObject objects;
    [SerializeField] Image weaponName;

    public void SetGunSprite(string name){
        var sprites = Resources.LoadAll<Sprite>(hudFiles);
        foreach(var s in sprites){
            if(s.name == name){
                weaponName.enabled = true;
                weaponName.sprite = s;
                return;
            }
        }
        weaponName.enabled = false;
    }
    private void Update() {
        objects.SetActive(StateController.Instance.CanUpdate);
    }
}
