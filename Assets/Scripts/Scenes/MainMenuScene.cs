using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : MonoBehaviour
{
    bool canPlay = true;
    public void Play(){
        if(!canPlay)return;
        LevelController.Instance.LoadWorldMethod("HowToPlay", "gameMusic");
        canPlay = false;
    }
}
