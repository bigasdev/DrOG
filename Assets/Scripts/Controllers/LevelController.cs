using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    private static LevelController instance;
    public static LevelController Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<LevelController>();
            }
            return instance;
        }
    }
    public string currentLoadedWorld;
    [SerializeField] GameObject loadingCanvas;
    public void LoadWorldMethod(string name, AudioClip sound = null, bool dialogue = true){
        StartCoroutine(LoadWorld(name, dialogue));
        AudioController.Instance.PlayMusic(sound);
    }
    public void LoadWorldMethod(string name, string sound = null, bool dialogue = true){
        StartCoroutine(LoadWorld(name, dialogue));
        AudioController.Instance.PlayMusic(sound);
    }
    public IEnumerator LoadWorld(string worldName, bool dialogue = true) {
        if (!string.IsNullOrEmpty(currentLoadedWorld)) {
            Resources.UnloadUnusedAssets();
            yield return SceneManager.UnloadSceneAsync(currentLoadedWorld);
        }

        currentLoadedWorld = worldName;

        if (!string.IsNullOrEmpty(currentLoadedWorld)) {
            loadingCanvas.SetActive(true);
            AudioController.Instance.OnFadeOut();
            yield return new WaitForSeconds(1f);
            yield return SceneManager.LoadSceneAsync(currentLoadedWorld, LoadSceneMode.Additive);
            loadingCanvas.SetActive(false);
            Scene levelScene = SceneManager.GetSceneByName(currentLoadedWorld);
            SceneManager.SetActiveScene(levelScene);
            var levelManager = FindObjectOfType<LevelManager>();
            if(levelManager != null)levelManager.startDialogue = dialogue;
        }
        else {
            Debug.LogError("Current level is null. Did you forget to add a scene key?");
        }

        yield return new WaitForEndOfFrame();
    }
}
