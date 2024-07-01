using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [NonSerialized]
    public static AudioManager i;
    [NonSerialized]
    public AudioSource sfxSource;
    [SerializeField]
    public AudioSource musicSource;
    [SerializeField]
    public AudioClip MainMenuMusic;

    [SerializeField]
    AudioMixer audioMixer;
    public float musicVolume = 0.5f;

    void Awake()
    {
        if (i == null) i = this;
        else Destroy(this.gameObject);

        //Make sure this doesn't happen before we've loaded the music volume!
        //AudioManager.i.StartCoroutine(AudioManager.i.CrossFade(AudioManager.i.MainMenuMusic));
    }

    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        
        if (this == null) Debug.Log("???");
        if (MainMenuMusic == null) Debug.Log("Menu music null");
    }

    // Update is called once per frame
    public void PlaySound(AudioClip sound)
    {
        Debug.Log($"Playing: {sound.name}");
        if (sound == null) Debug.LogError("Audio clip is null!");
        else if (sfxSource == null) Debug.LogError($"Playing {sound.name} but source is null!");
        else sfxSource.PlayOneShot(sound, musicVolume);
    }

    public void LoopMusic(AudioClip sound)
    {
        if (sound == null) Debug.LogError("Audio clip is null!");
        else if (musicSource == null) Debug.LogError($"Playing {sound.name} but source is null!");
        musicSource.clip = sound;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void SetMenuMusic()
    {
        Debug.Log("SOUND: Menu music");
        StartCoroutine(CrossFade(MainMenuMusic));
    }

    public IEnumerator CrossFade(AudioClip toSound, float delayBetween = 0f, bool stopLooping = false)
    {
        //if (GameSettings.i is null) Debug.LogError("settings null");
        //if (GameSettings.i.audioMixer is null) Debug.LogError("mixer null");

        float value = musicVolume;
        float toValueNormalized = Mathf.Lerp(-80f, 20f, value);

        Debug.Log($"SOUND: Music volume in settings is {value}, normalized to {toValueNormalized}. Crossfading to {toSound.name}");

        yield return StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "MusicVolume", 1f, 0f));
        Debug.Log("SOUND: Fade out done");
        yield return new WaitForSeconds(delayBetween);
        LoopMusic(toSound);
        if (stopLooping) musicSource.loop = false;
        yield return StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "MusicVolume", 1f, targetVolume: toValueNormalized));
        Debug.Log("SOUND: Fade in done");
        yield break;


    }
}
