using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerEvent : MonoBehaviour
{
    [SerializeField] bool canBeTriggered = true;
    [SerializeField] DialogueObject dialogueObj;
    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if(player == null || !canBeTriggered)return;
        var dialogue = Resources.Load<DialogueHud>("Prefabs/Views/DialogueHud");
        var d = Instantiate(dialogue);
        d.StartDialogue(dialogueObj);
        canBeTriggered = false;
    }
}
