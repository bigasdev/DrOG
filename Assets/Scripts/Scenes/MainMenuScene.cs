using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : MonoBehaviour
{
    public void Play(){
        LevelController.Instance.LoadWorldMethod("FirstLevel", "gameMusic");
    }
}
