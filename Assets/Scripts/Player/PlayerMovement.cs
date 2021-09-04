using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float speed;

    public void Move(float x, float y){
        var xMov = transform.position.x + x;
        var yMov = transform.position.y + y;
        if(y > 0 || y < 0 && CameraManager.Instance.zRotationAngle <= 5)CameraManager.Instance.zRotationAngle += 2.35f * Time.deltaTime;
        if(x < 0 || x > 0 && CameraManager.Instance.zRotationAngle >= 0)CameraManager.Instance.zRotationAngle -= 2.35f * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(xMov, yMov), speed * Time.deltaTime);
    }
}
