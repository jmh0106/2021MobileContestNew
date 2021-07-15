using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPlayer;
    GameObject step2;
    GameObject step3;
    GameObject step4;
    GameObject step5;
    GameObject step3_4;
    GameObject tutorialEnemy;
    GameObject enemy;
    void Start()
    {
        step2 = GameObject.FindGameObjectWithTag("Step2");
        step2.SetActive(false);
        step3 = GameObject.FindGameObjectWithTag("Step3");
        step3.SetActive(false);
        step4 = GameObject.FindGameObjectWithTag("Step4");
        step4.SetActive(false);
        step3_4 = GameObject.FindGameObjectWithTag("Step3_4");
        step3_4.SetActive(false);
        step5 = GameObject.FindGameObjectWithTag("Step5");
        step5.SetActive(false);
        tutorialEnemy = GameObject.FindGameObjectWithTag("TutorialMonster");
        tutorialEnemy.SetActive(false);
        enemy = GameObject.FindGameObjectWithTag("TutorialEnemy");
        enemy.SetActive(false);
    }

    public void Step1OnClick()
    {
        Destroy(GameObject.FindGameObjectWithTag("Step1"));
        tutorialPlayer.GetComponent<TutorialPlayer>().isMoveOn = true;
        step2.SetActive(true);
    }

    public void OnStep3()
    {
        Destroy(step2);
        step3.SetActive(true);
        step3_4.SetActive(true);
        tutorialPlayer.GetComponent<TutorialPlayer>().isBulletOn = true;
        tutorialEnemy.SetActive(true);
        
    }
    public void OnStep4()
    {
        tutorialPlayer.GetComponent<TutorialPlayer>().isMoveOn = false;
        tutorialPlayer.GetComponent<TutorialPlayer>().isBulletOn = false;
        tutorialPlayer.GetComponent<TutorialPlayer>().isDie = true;
        Destroy(step3);
        Destroy(step3_4);
        step4.SetActive(true);
        enemy.SetActive(true);
    }
    public void OnStep5()
    {
        Destroy(step4);
        step5.SetActive(true);
    }
}
