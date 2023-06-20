using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

}
