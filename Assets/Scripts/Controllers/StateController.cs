using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private static StateController instance;
    public static StateController Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<StateController>();
            }
            return instance;
        }
    }
    public StateDefinition currentState;
    public bool CanUpdate => currentState == StateDefinition.GAME_UPDATE;
}
public enum StateDefinition{
    GAME_UPDATE,
    SETTINGS_UPDATE,
    ENDGAME_UPDATE,
    DIALOGUE_UPDATE
}
