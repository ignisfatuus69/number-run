using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public enum AudioSourceType
    {
        BGM, SFX, UI, Voice, Oneshot
    }
    // For playing background music
    AudioSource bgmSource;

    // SFX Sources
    AudioSource oneShotSource;
    AudioSource sfxSource;
    AudioSource uiSource;
    AudioSource voiceSource;


    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject(typeof(AudioManager).ToString(), typeof(AudioManager));
                _instance = go.GetComponent<AudioManager>();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    static AudioManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);

        InitSources();
    }

    public AudioSource Play(AudioClip clip, AudioSourceType type)
    {
        AudioSource source = GetSource(type);
        source.clip = clip;
        source.Play();
        return source;
    }

    public void PlayOneShot(AudioClip clip)
    {
        oneShotSource.PlayOneShot(clip, GetVolume(AudioSourceType.Oneshot));
    }

    public void Play(AudioClip clip)
    {
        oneShotSource.clip = clip;
        oneShotSource.Play();
    }

    public void UpdateSFXVolume(float vol)
    {
        UpdateVolume(vol, AudioSourceType.SFX);
        UpdateVolume(vol, AudioSourceType.Oneshot);
        UpdateVolume(vol, AudioSourceType.UI);
    }

    public void UpdateVolume(float vol, AudioSourceType type)
    {
        AudioSource source = GetSource(type);
        source.volume = vol;
        PlayerPrefs.SetFloat($"{type}Vol", vol);
    }

    public float GetVolume(AudioSourceType type)
    {
        if (PlayerPrefs.HasKey($"{type}Vol"))
            return PlayerPrefs.GetFloat($"{type}Vol");
        else
            return GetSource(type).volume;
    }

    public IEnumerator PlayListRoutine(List<AudioClip> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            AudioClip clip = list[i];
            var wait = new WaitForSeconds(clip.length);
            PlayOneShot(clip);
            yield return wait;
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    private AudioSource GetSource(AudioSourceType type)
    {
        switch (type)
        {
            case AudioSourceType.BGM:
                return bgmSource;
            case AudioSourceType.SFX:
                return sfxSource;
            case AudioSourceType.UI:
                return uiSource;
            case AudioSourceType.Voice:
                return voiceSource;
            default:
                return sfxSource;
        }
    }

    private void InitSources()
    {
        oneShotSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        uiSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>();

        int length = EnumUtility.GetLength<AudioSourceType>();

        for (int i = 0; i < length; i++)
        {
            if (PlayerPrefs.HasKey($"{(AudioSourceType)i}Vol"))
            {
                GetSource((AudioSourceType)i).volume = PlayerPrefs.GetFloat($"{(AudioSourceType)i}Vol");
            }
        }
    }
}
