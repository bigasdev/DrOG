using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollectable : Collectable
{
    private const string weaponFolder = "Assets/Sprites/Player/";
    public Weapon weapon;
    [SerializeField]SpriteRenderer spriteRenderer;
    public bool wasThrown;
    bool canBeCollected;
    private void Start() {
        Initialize();
        Invoke("CanCollect", 1f);
    }
    void CanCollect(){
        canBeCollected = true;
    }
    public void Initialize(){
        var sprites = Resources.LoadAll<Sprite>(weaponFolder + "Arma");
        Sprite sprite = null;
        foreach(var s in sprites){
            if(s.name== weapon.weaponName + "Ground"){
                sprite = s;
            }
        }
        spriteRenderer.sprite = sprite;
    }
    public void ThrowWeapon(){
        StartCoroutine(Throw());
    }
    IEnumerator Throw(){
        wasThrown = true;
        yield return new WaitForSeconds(.35f);
        canBeCollected = true;
        wasThrown = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        var entity = other.GetComponent<Entity>();
        KillEntity(entity);
        var player = other.GetComponent<Player>();
        if(player == null || !canBeCollected)return;
        OnCollect(player);
    }
    public override void OnCollect(Player player)
    {
        PlayerHandler.ChangePlayerWeapon(this.weapon);
        AudioController.Instance.PlaySound("pickup");
        canBeCollected = false;
        //PlayerHandler.ChangePlayerSprite(weapon.weaponName + Player.Instance.copName);
        base.OnCollect(player);
    }
    void KillEntity(Entity entity){
        if(entity == null || !wasThrown)return;
        entity.Hp.Hp -= 1;
        if(entity.Hp.Hp <= 0)entity.ChangeCorpse();
        if(entity.Hp.Hp < 0)return;
        EffectsHandler.SpawnPopup("PointsPopup", entity.transform, Vector3.zero);
        EffectsHandler.SpawnBlood(entity.transform, this.transform);
        EffectsHandler.ThrowStuff(entity, entity.transform, new Vector2(entity.transform.position.x, entity.transform.position.y + 1.75f));
        CameraManager.KillShake();
    }
}
