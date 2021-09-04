using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Language : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    [SerializeField] string ptBr, english;

    private void Start() {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        if(DataController.Instance.settings.language == "PT-BR"){
            text.text = ptBr;
        }else{
            text.text = english;
        }
    }
    private void OnEnable() {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        if(DataController.Instance.settings.language == "PT-BR"){
            text.text = ptBr;
        }else{
            text.text = english;
        }
    }
}
