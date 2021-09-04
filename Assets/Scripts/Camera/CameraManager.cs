using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    public static CameraManager Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<CameraManager>();
            }
            return instance;
        }
    }
    [SerializeField] Camera mainCamera;
    [SerializeField] float followSpeed;
    public BoxCollider2D bounds;
    public float zRotationAngle;
    private void Update() {
        CameraClamp();
    }
    private void FixedUpdate() {
        Movement();
    }
    void Movement(){
        if(Player.Instance == null || shakeRoutine != null)return;
        mainCamera.transform.position = new Vector3(Mathf.MoveTowards(mainCamera.transform.position.x, Player.Instance.transform.position.x, followSpeed * Time.deltaTime),
                                  Mathf.MoveTowards(mainCamera.transform.position.y, Player.Instance.transform.position.y, followSpeed * Time.deltaTime),
                                  -10);
        if(zRotationAngle <= 0)return;
        mainCamera.transform.eulerAngles = Vector3.MoveTowards(mainCamera.transform.eulerAngles, new Vector3(0,0,zRotationAngle), 10 * Time.deltaTime);
    }
    void CameraClamp(){
        if(bounds == null)return;
        float vertExtent = mainCamera.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;

        Vector3 linkedCameraPos = mainCamera.transform.position;
        Bounds areaBounds = bounds.bounds;

        mainCamera.transform.position = new Vector3(
            Mathf.Clamp(linkedCameraPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
            Mathf.Clamp(linkedCameraPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
            linkedCameraPos.z);
    }


    public void killShake() => KillShakePrivate();
    public void shootShake() => OnCameraShake(.010f, .05f);
    public static void KillShake() => CameraManager.Instance.killShake();
    public static void ShootShake() => CameraManager.Instance.shootShake();
    Coroutine shakeRoutine;
    public void OnCameraShake(float duration, float magnitude) {
        if (shakeRoutine != null) {
            return;
        }
        shakeRoutine = StartCoroutine(Shake(duration, magnitude));
    }
    public void KillShakePrivate(){
        StartCoroutine(SlowCamera());
        shakeRoutine = null;
        OnCameraShake(.015f, .5f);
    }
    private IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPos3 = mainCamera.transform.localPosition;
        Vector2 originalPos = mainCamera.transform.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < duration) {
            mainCamera.transform.localPosition = new Vector3(originalPos.x + Random.insideUnitCircle.x * magnitude, originalPos.y + Random.insideUnitCircle.y * magnitude, -10f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPos3;
        //DefaultPosition();
        shakeRoutine = null;
    }
    private IEnumerator SlowCamera(){
        Time.timeScale = .15f;
        yield return new WaitForSeconds(.015f);
        Time.timeScale = 1f;
    }
}
