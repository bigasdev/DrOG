using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    bool CanHitAgain = true;
    public float timer = .3f;
    Coroutine cd;
    private void OnEnable() {
        StartCoroutine(DisableAgain());
        CanHitAgain = true;
        cd = null;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        var entity = other.GetComponent<Entity>();
        if(entity == null)return;
        KillEntity(entity);
        if(cd == null)cd = StartCoroutine(Cooldown());
    }
    void KillEntity(Entity entity){
        if(entity == null || !CanHitAgain)return;
        if(entity.isDead)return;
        entity.Hp.Hp -= 2;
        if(entity.Hp.Hp <= 0){
            CloseEnemies();
            entity.ChangeCorpse();
        }
        if(entity.Hp.Hp < 0)return;
        EffectsHandler.SpawnPopup("PointsPopup", entity.transform, Vector3.zero);
        EffectsHandler.SpawnBlood(entity.transform, this.transform);
        EffectsHandler.ThrowStuff(entity, entity.transform, new Vector2(entity.transform.position.x, entity.transform.position.y + 1.75f));
        CameraManager.KillShake();
    }
    IEnumerator Cooldown(){
        CanHitAgain = false;
        yield return new WaitForSeconds(.5f);
        CanHitAgain = true;
        cd = null;
    }
    IEnumerator DisableAgain(){
        yield return new WaitForSeconds(timer);
        this.gameObject.SetActive(false);
    }
    void CloseEnemies(){
        var enemies = FindObjectsOfType<Entity>();
        foreach(var e in enemies){
            if(Vector2.Distance(e.transform.position, this.transform.position) <= 10){
                e.foundPlayer = true;
            }
        }
    }
}
