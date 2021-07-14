using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillDestroy : MonoBehaviour
{
    float timer = 0;
    public Sprite bossEye;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            GameObject[] bossEyes = GameObject.FindGameObjectsWithTag("BossEye");
            for (int i = 0; i < bossEyes.Length; i++)
            {
                SpriteRenderer bossEyeSpriteRenderer = bossEyes[i].GetComponent<SpriteRenderer>();
                bossEyeSpriteRenderer.sprite = bossEye;

            }
            Destroy(gameObject);
        }
    }
}
