using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] DialogueObject dialogueStart;
    public bool startDialogue = true;
    public string levelName;
    public AudioClip levelMusic;

    private void Start() {
        if(!startDialogue){
            StateController.Instance.currentState = StateDefinition.GAME_UPDATE;
            return;
        }
        var dialogue = Resources.Load<DialogueHud>("Prefabs/Views/DialogueHud");
        var d = Instantiate(dialogue);
        d.StartDialogue(dialogueStart);
    }
}
