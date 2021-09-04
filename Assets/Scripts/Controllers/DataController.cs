using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private static DataController instance;
    public static DataController Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<DataController>();
            }
            return instance;
        }
    }
    public PlayerSettings settings;
    private void Start() {
        DontDestroyOnLoad(this.transform);
        Resolution[] resolutions = Screen.resolutions;

        foreach (var res in resolutions)
        {
            Debug.Log(res.width + "x" + res.height + " : " + res.refreshRate);
        }
    }
}
