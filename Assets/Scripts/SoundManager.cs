using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Sounds")]
    public AudioClip jump;
    public AudioClip land;
    public AudioClip swing;
    public AudioClip hit;
    public AudioClip swordPickUp;

    [Header("UI Sounds")]
    public AudioClip confirm;
    public AudioClip gameEnd;
    public AudioClip gameStart;

    [Header("Music")]
    public AudioClip forestMusic;
    public AudioClip gameMusic;
    public AudioClip menuMusic;
    public AudioClip swordMusic;

    [Header("Lists")]
    public AudioClip[] jumpingSounds;
    public AudioClip[] popSounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeMusic(menuMusic);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void PlayRandomFromList(AudioClip[] list)
    {
        if (list.Length > 0)
        {
            int rand = Random.Range(0, list.Length);
            PlaySfx(list[rand]);
        }
    }

    public void ChangeMusic(AudioClip clip)
    {
        if (musicSource.isPlaying)
        {
            if (musicSource.clip == clip) return;
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void FadeMusicOut(float time)
    {
        if (!DOTween.IsTweening(musicSource))
        {
            musicSource.DOFade(0, time);
        }
        else
        {
            musicSource.DOComplete();
        }
    }

    public void FadeMusicIn(float time)
    {
        if (!DOTween.IsTweening(musicSource))
        {
            musicSource.DOFade(1, time);
        }
        else
        {
            musicSource.DOComplete();
        }
    }

}
