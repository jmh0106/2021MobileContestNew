using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BossSkill : MonoBehaviour
{
    public GameObject bossSkill;
    public GameObject bossSkillAlarm;
    public Sprite bossEyeSprite;
    float timer = 0;
    float regenTime = 5;
    int skillPos;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > regenTime)
        {
            GameObject[] bossEyes = GameObject.FindGameObjectsWithTag("BossEye");
            for (int i = 0; i < bossEyes.Length; i++)
            {
                SpriteRenderer bossEyeSpriteRenderer = bossEyes[i].GetComponent<SpriteRenderer>();
                bossEyeSpriteRenderer.sprite = bossEyeSprite;
                
            }
            skillPos = Random.Range(-8, 9);
            Instantiate(bossSkillAlarm, new Vector3(skillPos,0,0),transform.rotation);
            timer = 0;
        }
    }
}
