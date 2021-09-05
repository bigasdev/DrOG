using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerKillEvent : MonoBehaviour
{
    [SerializeField] bool canBeThrown = true;
    [SerializeField] DialogueObject dialogueObj, dialogueNoObj;
    [SerializeField] Entity[] entitysToKill;
    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if(player == null || !canBeThrown)return;
        if(IsFinished()){
            var dialogue = Resources.Load<DialogueHud>("Prefabs/Views/DialogueHud");
            var d = Instantiate(dialogue);
            d.StartDialogue(dialogueObj);
            canBeThrown =false;
        }else{
            var dialogue = Resources.Load<DialogueHud>("Prefabs/Views/DialogueHud");
            var d = Instantiate(dialogue);
            d.StartDialogue(dialogueNoObj);
            StartCoroutine(AdviceRoutine());
        }
        
    }
    bool IsFinished(){
        foreach(var e in entitysToKill){
            if(e.Hp.Hp >= 1)return false;
        }
        return true;
    }
    IEnumerator AdviceRoutine(){
        canBeThrown = false;
        yield return new WaitForSeconds(.75f);
        canBeThrown = true;
    }
}
