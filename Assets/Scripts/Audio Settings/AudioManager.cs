using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Menu Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null || s.clips.Length == 0)
        {
            Debug.Log("Sound Not Found");
            return;
        }

        musicSource.clip = s.clips[0]; // Asumiendo que la música tiene un solo clip
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null || s.clips.Length == 0)
        {
            Debug.Log("Sound Not Found");
            return;
        }

        // Seleccionar un sonido aleatorio dentro de la categoría
        AudioClip randomClip = s.clips[UnityEngine.Random.Range(0, s.clips.Length)];
        sfxSource.PlayOneShot(randomClip);
    }
}