using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        soundManager.BgmVolume = joySen.BgmVol;
        soundManager.SoundEffectVolume = joySen.SndEffVol;
    }

    void OnEnable()
    {
        gameObject.transform.GetChild(0).GetComponent<Slider>().value = soundManager.BgmVolume;
        gameObject.transform.GetChild(1).GetComponent<Slider>().value = soundManager.SoundEffectVolume;
        gameObject.transform.GetChild(2).GetComponent<Slider>().value = joySen.joystickSen;
    }

    private void Update()
    {
        soundManager.BgmVolume = gameObject.transform.GetChild(0).GetComponent<Slider>().value;
        soundManager.SoundEffectVolume = gameObject.transform.GetChild(1).GetComponent<Slider>().value;
        joySen.joystickSen = gameObject.transform.GetChild(2).GetComponent<Slider>().value;
        joySen.BgmVol = gameObject.transform.GetChild(0).GetComponent<Slider>().value;
        joySen.SndEffVol = gameObject.transform.GetChild(1).GetComponent<Slider>().value;
        soundManager.SoundVolumeChange();
    }

    public void CloseSetting()
    {
        gameObject.SetActive(false);
    }
}
