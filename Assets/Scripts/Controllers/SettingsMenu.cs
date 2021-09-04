using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject generalTab, graphicsTab;
    public void EnableTabs(bool value){
        generalTab.SetActive(!value);
        graphicsTab.SetActive(value);
    }
    public void ChangeResolution(int value){
        if(value == 0){
            Screen.SetResolution(1920,1080,FullScreenMode.FullScreenWindow);
        }else if(value == 1){
            Screen.SetResolution(1024,768,FullScreenMode.FullScreenWindow);
        }else if(value == 2){
            Screen.SetResolution(800,600,FullScreenMode.FullScreenWindow);
        }else if(value == 3){
            Screen.SetResolution(640,480,FullScreenMode.FullScreenWindow);
        }
    }
    public void SetBlood(bool value){
        DataController.Instance.settings.hasBlood = value;
    }
}
