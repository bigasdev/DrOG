using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Core/Dialogue", order = 1)]
public class DialogueObject : ScriptableObject
{
    public Dialogue[] dialogues;
}
[System.Serializable]
public class Dialogue{
    public string name;
    public Sprite portrait;
    public string dialogueBR;
    public string dialogueEN;
    public Dialogue(string n, Sprite p, string d, string de){
        name = n;
        portrait = p;
        dialogueBR = d;
        dialogueEN = de;
    }
}
