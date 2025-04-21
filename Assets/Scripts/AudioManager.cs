using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM Clips")]
    public AudioClip game;

    [Header("SFX Clips")]
    public AudioClip pew;

    private string currentBGM = "";
    private bool canPlaySFX = true;
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        string scene = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        bgmSource.loop = true;
        PlayBGM("game");
    }

    private void Update()
    {
        string scene = SceneManager.GetActiveScene().name;
    }

    public void PlayBGM(string clip)
    {
        switch (clip)
        {
            case "game": bgmSource.clip = game; bgmSource.volume = 0.5f; break;
            default: return;
        }

        bgmSource.Play();
    }

    public void PlaySFX(string clip)
    {
        if (!canPlaySFX) return;

        AudioClip selectedClip = null;
        float volume = 1f;

        switch (clip)
        {
            case "pew": selectedClip = pew; volume = 0.2f; break;
            default: return;
        }

        sfxSource.PlayOneShot(selectedClip, volume);
    }

    private IEnumerator EnableSFXAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canPlaySFX = true;
    }
}
