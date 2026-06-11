using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class VolumeControllerCS : MonoBehaviour
{
    [Header("Audio Configuration")]
    public AudioMixer audioMixer;
    public string volumeParameterName = "MasterVol";

    private Slider volumeSlider;

    private string saveKey;

    void Awake()
    {
        volumeSlider = GetComponent<Slider>();

        saveKey = "SavedVolume_" + volumeParameterName;
        
        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0.0000001f; 
            volumeSlider.maxValue = 1f;

            float savedValue = PlayerPrefs.GetFloat(saveKey, 0.75f);


            volumeSlider.value = savedValue;


            SetVolume(savedValue);


            volumeSlider.onValueChanged.AddListener(SetVolume);

            // volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float sliderValue)
    {
        if (audioMixer == null) return;

        float dbVolume = Mathf.Log10(sliderValue) * 20f;
        audioMixer.SetFloat(volumeParameterName, dbVolume);

        PlayerPrefs.SetFloat(saveKey, sliderValue);
        PlayerPrefs.Save(); // บันทึกข้อมูลลงดิสก์
    }
}
