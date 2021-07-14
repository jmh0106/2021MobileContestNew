using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossSkill : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color c;
    public Sprite bossEye;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color c = spriteRenderer.material.color;
        c.a = 0;
        StartCoroutine("FadeOut");
    }



    public void EyeSkillOn()
    {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeOut()
    {

        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            c.a = f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        GameObject[] bossEyes = GameObject.FindGameObjectsWithTag("BossEye");

        for (int i = 0; i < bossEyes.Length; i++)
        {
            SpriteRenderer bossEyeSpriteRenderer = bossEyes[i].GetComponent<SpriteRenderer>();
            bossEyeSpriteRenderer.sprite = bossEye;

        }
    }


    IEnumerator FadeIn()
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;
            Color c = spriteRenderer.material.color;
            c.a = f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine("FadeOut");
    }
}
