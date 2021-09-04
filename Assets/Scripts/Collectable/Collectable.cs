using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public virtual void OnCollect(Player player){
        Destroy(this.gameObject);
    }
}
