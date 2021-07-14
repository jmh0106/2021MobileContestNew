using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject SettingPanel;
    public float JoystickSen = 0.5f;
    public float BgmVol = 1f;
    public float SndEffVol = 1f;

    private void Update()
    {
        if (!SettingPanel.activeSelf)
            return;

        soundManager.BgmVolume = SettingPanel.transform.GetChild(0).GetComponent<Slider>().value;
        soundManager.SoundEffectVolume = SettingPanel.transform.GetChild(1).GetComponent<Slider>().value;
        JoystickSen = SettingPanel.transform.GetChild(2).GetComponent<Slider>().value;

        BgmVol = SettingPanel.transform.GetChild(0).GetComponent<Slider>().value;
        SndEffVol = SettingPanel.transform.GetChild(1).GetComponent<Slider>().value;
        soundManager.SoundVolumeChange();
    }
}
