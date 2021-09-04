using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    private static BulletsPool instance;
    public static BulletsPool Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<BulletsPool>();
            }
            return instance;
        }
    }
    public GameObject basePlayerBullet;
    private Queue<GameObject> avaliablePlayerBullets = new Queue<GameObject>();
    [SerializeField] int maxPlayerCapacity;
    private void Start() {
        GrowPlayerPool();
    }
    public GameObject GetFromPlayerPool(Vector2 pos, Quaternion rotation){
        if(avaliablePlayerBullets.Count == 0){
            GrowPlayerPool();
        }
        var instance = avaliablePlayerBullets.Dequeue();
        instance.SetActive(true);
        instance.transform.position = pos;
        instance.transform.rotation = rotation;
        return instance;
    }

    private void GrowPlayerPool()
    {
        for(int i = 0; i < maxPlayerCapacity; i++){
            var instanceToAdd = Instantiate(basePlayerBullet);
            instanceToAdd.transform.SetParent(transform);
            AddToPlayerPool(instanceToAdd);
        }
    }
    public void AddToPlayerPool(GameObject instance){
        instance.SetActive(false);
        avaliablePlayerBullets.Enqueue(instance);
    }
}
