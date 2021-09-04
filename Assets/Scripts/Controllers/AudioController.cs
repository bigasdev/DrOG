using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController instance;
    private const string audioFolder = "Assets/SFX/";
    public static AudioController Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<AudioController>();
            }
            return instance;
        }
    }
    [SerializeField] AudioSource music, sfx, ambient;
    public void SetMusicVolume(float value) {
        music.volume = value;
    }

    public void SetFxVolume(float value) {
        sfx.volume = value;
    }
    public void PlayMusic(string audioKey) {
        PlayMusic(Resources.Load<AudioClip>(audioFolder + audioKey));
    }

    public void PlayMusic(AudioClip musicClip) {
        if(musicClip == null) {
            return;
        }
        StartCoroutine(MusicRoutine(musicClip));
    }

    private IEnumerator MusicRoutine(AudioClip targetSong) {
        /*if (IsSongSame(targetSong)) {
            yield break;
        }*/

        StopFade();
        float vol = music.volume;
        OnFadeOut();
        yield return StartCoroutine(WaitForFadeToEnd());
        music.clip = targetSong;
        music.Play();
        OnFadeIn();
        yield return StartCoroutine(WaitForFadeToEnd());

        IEnumerator WaitForFadeToEnd() {
            while (Fading) {
                yield return null;
            }
        }
        bool IsSongSame(AudioClip audioClip) => (music.clip != null && music.clip == audioClip);
    }

    public void PlaySound(AudioClip sound) {
        if(sound == null) {
            return;
        }

        sfx.PlayOneShot(sound);
    }
    public void PlaySound(string name){
        var sound = Resources.Load<AudioClip>(audioFolder + name);
        if(sound == null)return;
        sfx.PlayOneShot(sound);
    }

    Coroutine fadeRoutine;
    public void OnFadeOut() {
        if (Fading) {
            return;
        }

        fadeRoutine = StartCoroutine(FadeOut());
    }

    public void OnFadeIn() {
        if (Fading) {
            return;
        }

        fadeRoutine = StartCoroutine(FadeIn());
    }

    public void StopFade() {
        if (!Fading) {
            return;
        }

        StopCoroutine(fadeRoutine);
        fadeRoutine = null;
    }

    bool Fading => fadeRoutine != null;

    public IEnumerator FadeOut() {
        while (music.volume > 0.1) {
            music.volume -= .35f * Time.deltaTime;
            yield return null;
        }
        music.volume = 0f;
        fadeRoutine = null;
    }

    public IEnumerator FadeIn() {
        while (music.volume < DataController.Instance.settings.musicVolume) {
            music.volume += .35f * Time.deltaTime;
            yield return null;
        }
        music.volume = DataController.Instance.settings.musicVolume;
        fadeRoutine = null;
    }
}
