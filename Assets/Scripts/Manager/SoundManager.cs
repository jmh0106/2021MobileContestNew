using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public SettingManager settingManager;

    public float BgmVolume = 1;
    public float SoundEffectVolume = 1;

    private void Awake()
    {
        audioSource.loop = true;
        audioSource.volume = settingManager.BgmVol;

        switch (SceneManager.GetActiveScene().name)
        {
            case "LobbyScene":
                PlayLobbyBgm();
                break;

            case "StageSelectScene":
                PlayLobbyBgm();
                break;

            case "Boss":
                PlayStage1Bgm();
                break;

            case "Stage1BossScene":
                PlayBoss1Bgm();
                break;
        }
    }

    public void PlayStage1Bgm()
    {
        audioSource.clip = audioClip[0];
        audioSource.Play();
    }

    public void PlayBoss1Bgm()
    {
        audioSource.clip = audioClip[1];
        audioSource.Play();
    }

    public void PlayLobbyBgm()
    {
        audioSource.clip = audioClip[10];
        audioSource.Play();
    }

    public void SoundVolumeChange()
    {
        audioSource.volume = BgmVolume;
    }

    public void PlayBossBounce()
    {
        audioSource.PlayOneShot(audioClip[2], SoundEffectVolume);
    }

    public void PlayBubbleItem()
    {
        audioSource.PlayOneShot(audioClip[3], SoundEffectVolume);
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(audioClip[4], SoundEffectVolume);
    }

    public void PlayEnemyDeath()
    {
        audioSource.PlayOneShot(audioClip[5], SoundEffectVolume);
    }

    public void PlayFreezeItem()
    {
        audioSource.PlayOneShot(audioClip[6], SoundEffectVolume);
    }

    public void PlaySalvationItem()
    {
        audioSource.PlayOneShot(audioClip[7], SoundEffectVolume);
    }

    public void PlayScoreCount()
    {
        audioSource.PlayOneShot(audioClip[8], SoundEffectVolume);
    }

    public void PlayTimeslowItem()
    {
        audioSource.PlayOneShot(audioClip[9], SoundEffectVolume);

    }
}
