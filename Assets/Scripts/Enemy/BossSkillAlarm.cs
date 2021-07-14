using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillAlarm : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    public GameObject bossSkill;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color c = spriteRenderer.material.color;
        c.a = 0;
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        float n = 0;
        for (int i = 0; i < 10; i++)
        {
            Color c = spriteRenderer.material.color;
            if (n == 0)
            {
                n = 0.5f;
                c.a = n;
            }
            else
            {
                n = 0;
                c.a = n;
            }

            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        Instantiate(bossSkill, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
