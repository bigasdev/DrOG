using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectsHandler{
    private const string particlesFolder = "Prefabs/Particles/";
    private const string popupsFolder = "Prefabs/Popups/";
    public static void SpawnParticle(Transform pos, string name){
        var particle = Resources.Load<GameObject>(particlesFolder + name);
        var p = Object.Instantiate(particle);
        p.transform.position = pos.position;
    }
    public static void SpawnBlood(Transform pos, Transform rot){
        if(!DataController.Instance.settings.hasBlood)return;
        var blood = Resources.Load<BloodSpatter>(particlesFolder + "BloodSpatter");
        var b = Object.Instantiate(blood);
        b.transform.position = pos.position;
        b.transform.eulerAngles = new Vector3(0, 0, -rot.eulerAngles.z);
    }
    public static void SpawnPopup(string name, Transform pos, Vector3 offset){
        var popup = Resources.Load<GameObject>(popupsFolder + name);
        var p = Object.Instantiate(popup);
        p.transform.position = pos.position + offset;
    }
    public static void ThrowStuff(MonoBehaviour obj,Transform oPos, Vector2 pos, float speed = 15){
        obj.StartCoroutine(Fly(oPos, pos, speed));
    }
    public static IEnumerator Fly(Transform originalPos, Vector2 reach, float speed = 15){
        while(Vector2.Distance(originalPos.position, reach) >= .25f){
            originalPos.position = Vector2.MoveTowards(originalPos.position, reach, speed * Time.deltaTime);
            yield return null;
        }
    }
}
