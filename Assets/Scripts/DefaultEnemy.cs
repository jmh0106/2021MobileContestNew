using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed=5;
    private int health=2;
    public Sprite[] sprites;

    SpriteRenderer SpriteRenderer;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    private void OnHit()
    {
        health--;
        SpriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health == 0)
            Destroy(gameObject);
    }

    void ReturnSprite()
    {
        SpriteRenderer.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            OnHit();
            Destroy(collision.gameObject);
        }
    }
}
