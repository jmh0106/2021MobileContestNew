using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine("FadeIn");
        Color c = spriteRenderer.material.color;
        c.a = 0;
    }

    IEnumerator FadeOut()
    {

        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = spriteRenderer.material.color;
            c.a = f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }


    IEnumerator FadeIn()
    {
        for(int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;
            Color c = spriteRenderer.material.color;
            c.a = f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.tag = "Bomb";

        StartCoroutine("FadeOut");
    }

}
