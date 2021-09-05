using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    Vector2 toMove, toThrow;
    private void FixedUpdate() {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        var entity = other.GetComponent<Entity>();
        if(entity == null && other.CompareTag("Structure"))BulletsPool.Instance.AddToPlayerPool(this.gameObject);
        KillEntity(entity);
    }
    public void Move(){
        var t = transform.up * bulletSpeed;
        toMove = transform.position + t;
        toThrow = this.transform.up * 7;
        this.transform.position = Vector2.MoveTowards(this.transform.position, toMove, bulletSpeed * Time.deltaTime);
        DestroyOnTime(toMove);
    }
    void DestroyOnTime(Vector2 pos){
        var distance = Vector2.Distance(this.transform.position, pos);
        if(distance <= .05f){
            BulletsPool.Instance.AddToPlayerPool(this.gameObject);
        }
    }
    void KillEntity(Entity entity){
        if(entity == null)return;
        entity.Hp.Hp -= 1;
        entity.foundPlayer = true;
        if(entity.Hp.Hp <= 0)entity.ChangeCorpse();
        if(entity.Hp.Hp < 0)return;
        EffectsHandler.SpawnPopup("PointsPopup", entity.transform, Vector3.zero);
        EffectsHandler.SpawnBlood(entity.transform, this.transform);
        EffectsHandler.ThrowStuff(entity, entity.transform, new Vector2(entity.transform.position.x, entity.transform.position.y + 1.75f));
        CameraManager.KillShake();
        BulletsPool.Instance.AddToPlayerPool(this.gameObject);
    }
}
