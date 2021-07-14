using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public SoundManager soundManager;
    public float JoystickSen = 0.5f;
    public float BgmVol = 1f;
    public float SndEffVol = 1f;

    private void Awake()
    {
        soundManager.BgmVolume = BgmVol;
        soundManager.SoundEffectVolume = SndEffVol;
    }

    void OnEnable()
    {
        gameObject.transform.GetChild(0).GetComponent<Slider>().value = soundManager.BgmVolume;
        gameObject.transform.GetChild(1).GetComponent<Slider>().value = soundManager.SoundEffectVolume;
        gameObject.transform.GetChild(2).GetComponent<Slider>().value = JoystickSen;
    }

    private void Update()
    {
        soundManager.BgmVolume = gameObject.transform.GetChild(0).GetComponent<Slider>().value;
        soundManager.SoundEffectVolume = gameObject.transform.GetChild(1).GetComponent<Slider>().value;
        JoystickSen = gameObject.transform.GetChild(2).GetComponent<Slider>().value;
        BgmVol = gameObject.transform.GetChild(0).GetComponent<Slider>().value;
        SndEffVol = gameObject.transform.GetChild(1).GetComponent<Slider>().value;
        soundManager.SoundVolumeChange();
    }
}
