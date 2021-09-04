using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] float timer;
    private void Start() {
        Destroy(this.gameObject, timer);
    }
}
