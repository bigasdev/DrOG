using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool open = false;
    [SerializeField] Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if(player == null || open)return;
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger("Open");
        open = true;
    }
}
