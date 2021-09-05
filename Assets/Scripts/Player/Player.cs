using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Cop")]
    public Cops cop = Cops.Karmel;
    private static Player instance;
    public static Player Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    [Header("Components")]
    public Health hp;
    [SerializeField] PlayerMovement movement;
    [SerializeField] Animator anim;
    [SerializeField] GameObject hitbox;
    [SerializeField] Sprite deadCorpse, deadCorpseNoBlood;
    [SerializeField] LayerMask collisionMask;
    public SpriteRenderer spriteRenderer;
    public Weapon weaponItem;
    public Weapon knife;
    public Weapon bestial;
    public Transform weapon;
    float shootTimer;
    SettingsMenu settingsMenu;
    bool canRestart = true, canWalk = true;
    public bool isDead;
    Coroutine cdRestart;
    private void Start() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //PlayerHandler.ChangePlayerSprite(weaponItem.weaponName + copName);
    }
    private void Update() {
        if(Input.GetButtonDown("Restart")){
            if(canRestart) LevelController.Instance.LoadWorldMethod(FindObjectOfType<LevelManager>().levelName, FindObjectOfType<LevelManager>().levelMusic, false);
        }
        if(Input.GetButtonDown("Cancel")){
            OpenSettingsMenu();
        }
        CheckForHp();
        if(!StateController.Instance.CanUpdate)return;
        anim.SetBool("HasWeapon", weaponItem != null);
        UpdateDirection(GetDirectionMouse());
        shootTimer += Time.deltaTime;
        if(weaponItem == null)return;
        anim.SetInteger("WeaponNumber", weaponItem.weaponNumber);
        if(Input.GetButtonDown("Fire2")){
            ShootWeapon();
        }
        if(Input.GetButton("Fire") && shootTimer >= weaponItem.fireRate){
            Shoot();
            shootTimer = 0;
        }
    }
    private void FixedUpdate() {
        if(!StateController.Instance.CanUpdate){
            anim.SetBool("Walking",false);
            return;
        }
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        Vector2 xy = new Vector2(x,y);
        CheckForCollision(xy);
        bool isWalking = x != 0 || y != 0;
        //if(!isWalking)PlayerHandler.ChangePlayerSprite(weaponItem.weaponName + copName);
        anim.SetBool("Walking", isWalking);
        if(!canWalk)return;
        movement.Move(x,y);
    }
    void Shoot(){
        //CameraManager.ShootShake();
        if(weaponItem.weaponName == "knife"){
            CameraManager.ShootShake();
            hitbox.SetActive(true);
            AudioController.Instance.PlaySound("knifeSound");
            anim.SetTrigger("KnifeHit");
            return;
        }
        if(weaponItem.weaponName == "bestial"){
            CameraManager.ShootShake();
            hitbox.SetActive(true);
            hitbox.GetComponent<PlayerHitBox>().timer = .8f;
            EffectsHandler.ThrowStuff(this, this.transform, this.transform.position + transform.up * 3, 25);
            AudioController.Instance.PlaySound("knifeSound");
            anim.SetTrigger("BestialHit");
            return;
        }
        CloseEnemies();
        BulletsPool.Instance.GetFromPlayerPool(new Vector2(weapon.transform.position.x + Random.Range(-weaponItem.accuracy, weaponItem.accuracy), weapon.transform.position.y), weapon.transform.rotation);
        EffectsHandler.SpawnParticle(this.transform, "BulletDrop");
        AudioController.Instance.PlaySound(weaponItem.weaponName + "Shoot");
        weaponItem.bullets--;
        if(weaponItem.bullets <= 0){
            weaponItem = null;
            weaponItem = knife;
        }
    }
    void CloseEnemies(){
        var enemies = FindObjectsOfType<Entity>();
        foreach(var e in enemies){
            if(Vector2.Distance(e.transform.position, this.transform.position) <= 7){
                e.foundPlayer = true;
            }
        }
    }
    private Vector3 GetDirectionMouse(){
        Vector3 from = Camera.main.ViewportToWorldPoint(new Vector3(Input.mousePosition.x/Screen.width,Input.mousePosition.y/Screen.height,10.0f));
        Vector3 to = transform.position;
        Vector3 positionalDifference = to - from;
        return positionalDifference;
    }
    void UpdateDirection(Vector3 dir){
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        this.transform.rotation = rotation;
    }
    void ShootWeapon(){
        if(weaponItem.weaponName == "knife" || weaponItem.weaponName == "bestial")return;
        var weapon = Resources.Load<WeaponCollectable>("Prefabs/Collectables/WeaponCollectable");
        var w = Instantiate(weapon);
        w.weapon = weaponItem;
        w.Initialize();
        w.ThrowWeapon();
        w.transform.position = this.transform.position;
        var position = this.gameObject.transform.up * 6f;
        var p = w.transform.position + position;
        EffectsHandler.ThrowStuff(this, w.transform, p, 50);
        weaponItem = null;
        weaponItem = knife;
    }
    void OpenSettingsMenu(){
        if(settingsMenu != null){
            StateController.Instance.currentState = StateDefinition.GAME_UPDATE;
            Destroy(settingsMenu.gameObject);
            return;
        }
        StateController.Instance.currentState = StateDefinition.SETTINGS_UPDATE;
        var menu = Resources.Load<SettingsMenu>("Prefabs/Views/SettingsMenu");
        var m = Instantiate(menu);
        settingsMenu = m;
    }
    void CheckForHp(){
        if(hp.Hp >= 1)return;
        StateController.Instance.currentState = StateDefinition.ENDGAME_UPDATE;
        anim.enabled = false;
        spriteRenderer.sprite = DataController.Instance.settings.hasBlood ? deadCorpse : deadCorpseNoBlood;
        if(cdRestart == null)cdRestart = StartCoroutine(RestartCooldown());
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
    IEnumerator RestartCooldown(){
        canRestart = false;
        yield return new WaitForSeconds(.25f);
        canRestart = true;
    }
}
public enum Cops{
    Maximus,
    Karmel,
    Felix
}
