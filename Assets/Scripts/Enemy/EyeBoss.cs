using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBoss : MonoBehaviour
{
    public GameObject black;
    private float timer = 0;
    private float bossSkillTimer = 5;
    public Sprite bossSkillEye;

    
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > bossSkillTimer)
        {
            GameObject[] bossEyes = GameObject.FindGameObjectsWithTag("BossEye");
            for (int i = 0; i < bossEyes.Length; i++)
            {
                SpriteRenderer bossEyeSpriteRenderer = bossEyes[i].GetComponent<SpriteRenderer>();
                bossEyeSpriteRenderer.sprite = bossSkillEye;

            }
            black.GetComponent<EyeBossSkill>().EyeSkillOn();
            timer = 0;
        }

    }
}
