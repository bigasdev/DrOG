using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerEvent : MonoBehaviour
{
    [SerializeField] bool canBeTriggered = true;
    [SerializeField] GameObject obj;
    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        var entity = other.GetComponent<WalkEntity>();
        if(player == null && entity == null || !canBeTriggered)return;
        obj.SetActive(true);
        canBeTriggered = false;
    }
}
