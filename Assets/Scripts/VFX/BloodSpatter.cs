using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpatter : MonoBehaviour
{
    [SerializeField]ParticleSystem blood;

    private void Start() {
        Invoke("Pause", Random.Range(0.05f, 0.15f));
    }
    void Pause(){
        blood.Pause();
    }
}
