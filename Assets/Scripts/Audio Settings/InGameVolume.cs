using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class InGameVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    //private bool isMute;

    private void Start()
    {
        LoadSettings();

        // Asegurar que los sliders actualicen el volumen en tiempo real
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        myMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        myMixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    //public void ToggleMute()
    //{
    //    isMute = !isMute;
    //    PlayerPrefs.SetInt("isMuted", isMute ? 1 : 0);
    //    PlayerPrefs.Save();
    //    ApplyMute();
    //}

    //private void ApplyMute()
    //{
    //    if (isMute)
    //    {
    //        myMixer.SetFloat("Master Volume", -80f);
    //    }
    //    else
    //    {
    //        float volume = Mathf.Log10(PlayerPrefs.GetFloat("musicVolume", 0.75f)) * 20;
    //        myMixer.SetFloat("Master Volume", volume);
    //    }
    //}

    private void LoadSettings()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        //isMute = PlayerPrefs.GetInt("isMuted", 0) == 1;
        //ApplyMute();
    }
}
