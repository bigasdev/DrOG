using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKillEvent : MonoBehaviour
{
    [SerializeField] bool canBeThrown = true;
    [SerializeField] DialogueObject dialogueObject;
    [SerializeField] Entity[] entitysToKill;

    private void Update() {
        if(!canBeThrown)return;
        foreach(var e in entitysToKill){
            if(e.Hp.Hp >= 1)return;
        }
        var dialogue = Resources.Load<DialogueHud>("Prefabs/Views/DialogueHud");
        var d = Instantiate(dialogue);
        d.StartDialogue(dialogueObject);
        canBeThrown = false;
    }
}
