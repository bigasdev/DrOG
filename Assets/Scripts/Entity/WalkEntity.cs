using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEntity : MonoBehaviour
{
    [SerializeField] int point;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform[] movePoints;
    private void Start() {
        foreach(var m in movePoints){
            m.transform.parent = null;
        }
    }
    private void FixedUpdate() {
        if(!StateController.Instance.CanUpdate){
            return;
        }
        MoveTowards();
    }
    
    void MoveTowards(){
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
}
