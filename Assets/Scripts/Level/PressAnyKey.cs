using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] AudioClip music;
    [SerializeField] bool canGo = true;
    private void Update() {
        if(!canGo)return;
        if(Input.anyKeyDown){
            LevelController.Instance.LoadWorldMethod(sceneToLoad, music);
            canGo = false;
        }
    }
}
