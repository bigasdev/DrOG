using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private void Start() {
        Invoke("ChangeScene",.1f);
    }
    void ChangeScene(){
        if(DataController.Instance.settings.language == "Default"){
            LevelController.Instance.StartCoroutine(LevelController.Instance.LoadWorld("ChooseLanguage"));
        }else{
            LevelController.Instance.LoadWorldMethod("MainMenu", "logoMusic");
        }
    }
}
