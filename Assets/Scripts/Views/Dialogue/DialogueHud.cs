using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueName, dialogue;
    [SerializeField] Image dialoguePortrait;
    [SerializeField] Animator anim;
    [SerializeField] DialogueObject dialogueObject;
    [SerializeField] int dialogueIndex;
    [SerializeField] AudioClip typingSound;
    [SerializeField] GameObject skipButton;
    IEnumerator dialogueCoroutine;
    Coroutine leaveCoroutine, fullLeaveCoroutine;
    [SerializeField] bool canGoToNextDialogue = false;
    [SerializeField] bool canCommand = false;
    string realDialogue;
    private void Update()
    {
        if (!canCommand) return;

        if (Input.anyKeyDown && !canGoToNextDialogue)
        {
            ForceEndOfDialogue();

        }
        else if (Input.anyKeyDown && canGoToNextDialogue)
        {
            NextSentence();
        }
    }
    public void StartDialogue(DialogueObject obj){
        canCommand = false;
        dialogueObject = obj;
        StateController.Instance.currentState = StateDefinition.DIALOGUE_UPDATE;
        dialogueCoroutine = Type();
        StartCoroutine(dialogueCoroutine);
    }
    IEnumerator Type(){
        anim.SetTrigger("Enter");
        yield return new WaitForSeconds(.5f);
        dialogue.text = "";
        realDialogue = DataController.Instance.settings.language == "PT-BR" ? dialogueObject.dialogues[dialogueIndex].dialogueBR : dialogueObject.dialogues[dialogueIndex].dialogueEN;
        dialoguePortrait.sprite = dialogueObject.dialogues[dialogueIndex].portrait;
        dialogueName.text = dialogueObject.dialogues[dialogueIndex].name;

        foreach(var c in realDialogue){
            AudioController.Instance.PlaySound(typingSound);
            canCommand = true;
            dialogue.text += c;
            yield return new WaitForSeconds(.05f);
        }
        canGoToNextDialogue = true;
        skipButton.SetActive(true);
    }
    void NextSentence()
    {
        if(dialogueIndex < dialogueObject.dialogues.Length - 1)
        {
           if(leaveCoroutine == null)leaveCoroutine = StartCoroutine(WaitForNext());
        }
        else
        {
            if(fullLeaveCoroutine == null)fullLeaveCoroutine = StartCoroutine(WaitforLeave());
        }
    }
    IEnumerator WaitForNext(){
        anim.SetTrigger("Leave");
        skipButton.SetActive(false);
        canGoToNextDialogue = false;
        yield return new WaitForSeconds(.5f);
        dialogueIndex++;
        dialogueCoroutine = Type();
        dialogue.text = "";
        dialoguePortrait.sprite = dialogueObject.dialogues[dialogueIndex].portrait;
        dialogueName.text = dialogueObject.dialogues[dialogueIndex].name;
        StartCoroutine(dialogueCoroutine);
        leaveCoroutine = null;
    }
    IEnumerator WaitforLeave(){
        anim.SetTrigger("FullLeave");
        yield return new WaitForSeconds(.5f);
        StateController.Instance.currentState = StateDefinition.GAME_UPDATE;
        Destroy(this.gameObject);
    }
    void ForceEndOfDialogue()
    {
        StopCoroutine(dialogueCoroutine);
        dialogue.text = " " + realDialogue;

        canGoToNextDialogue = true;
        skipButton.SetActive(true);
    }
}
