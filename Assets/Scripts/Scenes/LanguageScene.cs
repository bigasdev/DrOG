using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageScene : MonoBehaviour
{
    public void SetLanguage(string language){
        DataController.Instance.settings.language = language;
        LevelController.Instance.LoadWorldMethod("MainMenu", "logoMusic");
    }
}
