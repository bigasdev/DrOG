using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LanguageImage : MonoBehaviour
{
    [SerializeField] Sprite ptBr, english;
    [SerializeField] Image buttonImage;
    private void Start() {
        if(DataController.Instance.settings.language == "PT-BR"){
            buttonImage.sprite = ptBr;
        }else{
            buttonImage.sprite = english;
        }
    }
}
