using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Health Hp;
    [SerializeField] Transform[] movePoints;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] int point = 0;
    [SerializeField] Sprite charSprite, corpseDead, corpseDeadWithoutBlood;
    [SerializeField] LayerMask playerMask;
    [SerializeField] AudioClip deathSound, seeSound;
    [SerializeField] float runSpeed;
    [SerializeField] bool isChase, isStatic;
    public bool foundPlayer;
    [SerializeField] float playerMinDist;
    [SerializeField] DialogueObject killPlayerDialogue;
    [SerializeField] Transform weaponPos, weaponRot;
    [SerializeField] Weapon weapon;
    SpriteRenderer spriteRenderer;
    public bool isDead = false, killedCop = false, canWalk = true;
    float shootTimer;
    private void OnTriggerEnter2D(Collider2D other) {
        if(isDead || killedCop)return;
        var player = other.GetComponent<Player>();
        if(player != null){
            if(player.isDead)return;
            CameraManager.KillShake();
            player.hp.Hp -= 1;
            if(player.hp.Hp <= 0){
                AudioController.Instance.PlaySound("playerDeath");
                if(player.cop == Cops.Karmel)PlayerHandler.SpawnRestartScreen();
                if(player.cop == Cops.Maximus){
                    if(killPlayerDialogue != null){
                        var dialogue = Resources.Load<DialogueHud>("Prefabs/Views/DialogueHud");
                        var d = Instantiate(dialogue);
                        d.StartDialogue(killPlayerDialogue);
                    }else{
                        PlayerHandler.SpawnRestartScreen();
                    }
                }
                player.isDead = true;
                killedCop = true;
            }
        }
    }
    private void Start() {
        foreach(var m in movePoints){
            m.transform.parent = null;
        }
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if(charSprite != null)spriteRenderer.sprite = charSprite;
    }
    private void Update() {
        if(Player.Instance == null || isDead)return;
        var playerDist = Vector2.Distance(this.transform.position, Player.Instance.transform.position);
        if(playerDist <= playerMinDist)foundPlayer = true;
        CheckForPlayer();
    }
    private void FixedUpdate() {
        if(Hp.Hp <= 0)isDead = true;
        if(Hp.Hp <= 0 || !StateController.Instance.CanUpdate){
            return;
        }
        RotateToPlayer();
        CheckForCollision(transform.right * 1.25f);
        if(!canWalk)return;
        MoveTowards();
        MoveToPlayer();
    }
    void MoveTowards(){
        if(foundPlayer || isStatic)return;
        var currentPoint = movePoints[point];
        transform.right = currentPoint.position - transform.position;
        this.transform.position = Vector2.MoveTowards(this.transform.position, currentPoint.position, moveSpeed * Time.deltaTime);

        var distance = Vector2.Distance(this.transform.position, currentPoint.position);
        if(distance <= .15f){
            point++;
            if(point >= movePoints.Length){
                point = 0;
            }
        }
    }
    void MoveToPlayer(){
        if(!foundPlayer || Player.Instance == null || !isChase)return;
        this.transform.position = Vector2.MoveTowards(this.transform.position, Player.Instance.transform.position, runSpeed * Time.deltaTime);
    }
    void RotateToPlayer(){
        if(!foundPlayer || Player.Instance == null)return;
        transform.right = Player.Instance.transform.position - transform.position;
    }
    void CheckForPlayer(){
        RaycastHit2D hit2D = Physics2D.Raycast(this.transform.position, transform.right, 9, playerMask);

        if(hit2D.collider != null){
            var player = hit2D.collider.GetComponent<Player>();
            if(player !=null){
                Shoot();
                foundPlayer = true;
            }
        }
    }
    public void ChangeCorpse(){
        AudioController.Instance.PlaySound(deathSound);
        if(spriteRenderer != null)spriteRenderer.sprite = DataController.Instance.settings.hasBlood ? corpseDead : corpseDeadWithoutBlood;
    }
    void CheckForCollision(Vector2 direction){
        RaycastHit2D hit2D = Physics2D.Raycast(this.transform.position, direction, .5f, collisionMask);

        if(hit2D.collider != null){
            Debug.Log(hit2D.collider);
            canWalk = false;
            return;
        }
        canWalk = true;
    }
    void Shoot(){
        if(weapon.weaponName == "" && string.IsNullOrEmpty(weapon.weaponName))return;
        shootTimer += Time.deltaTime;
        if(shootTimer >= weapon.fireRate){
            BulletsPool.Instance.GetFromPlayerPool(weaponPos.position, weaponRot.rotation);
            EffectsHandler.SpawnParticle(this.transform, "BulletDrop");
            AudioController.Instance.PlaySound(weapon.weaponName + "Shoot");
            shootTimer = 0;
        }
    }
}
