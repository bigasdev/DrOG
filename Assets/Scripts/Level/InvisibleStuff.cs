using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleStuff : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float distance = .5f;
    void Update()
    {
        if(spriteRenderer == null || Player.Instance == null)return;
        if(Vector2.Distance(this.transform.position, Player.Instance.transform.position) <= distance){
            spriteRenderer.color = new Color(1,1,1,.25f);
        }else{
            spriteRenderer.color = new Color(1,1,1,1);
        }
    }
}
