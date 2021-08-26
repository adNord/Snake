using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider m_Slider;

    void Start(){
        m_Slider.value = PlayerPrefs.GetFloat("MasterVolume");
    }

    public void SetVolume(Slider slider){
        audioMixer.SetFloat("volume", Mathf.Log10(slider.value) * 20);
        PlayerPrefs.SetFloat("MasterVolume", slider.value);
    }
}
