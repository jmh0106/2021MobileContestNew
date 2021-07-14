using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage1Boss : MonoBehaviour
{
    private float bossSpeed = 3f;
    private float bulletCoolTime = 0.9f;
    private float curX, curY;
    private float timer = 0;
    private int health = 5;
    public GameObject bossBullet;
    public GameObject gameMg;
    GameObject child;
    private Transform playerPos;
    private int currentPhase;
    public Stage1GameManager gameManager;
    SpriteRenderer spriteRenderer;
    public SoundManager soundManager;



    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Stage1GameManager>();

        curX = Random.Range(0.8f, 1.1f) * (int)Mathf.Pow(-1, (int)Random.Range(0, 2));
        curY = curX * (int)Mathf.Pow(-1, (int)Random.Range(0, 2));

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        gameMg = GameObject.FindGameObjectWithTag("GameManager");

        health = gameMg.GetComponent<Stage1GameManager>().GetHealth();
        float scale = gameMg.GetComponent<Stage1GameManager>().GetScale();
        transform.localScale = new Vector3(scale, scale, 1);
        currentPhase = gameMg.GetComponent<Stage1GameManager>().GetPhase();
        Debug.Log("currentPhase:" + currentPhase);

        child = transform.GetChild(0).gameObject;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }


    void Update()
    {
        if (transform.position.x < -7.5)
        {
            curX = 1;
            soundManager.PlayBossBounce();
        }
        else if (transform.position.x > 7.5)
        {
            curX = -1;
            soundManager.PlayBossBounce();
        }

        if (transform.position.y < -4.5 || transform.position.y > 4.5)
        {
            curY = 1;
            soundManager.PlayBossBounce();
        }
        if (transform.position.y > 4.5)
        {
            curY = -1;
            soundManager.PlayBossBounce();
        }

        transform.Translate(new Vector3(curX, curY, 0) * bossSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer > bulletCoolTime)
        {
            Fire();
            timer = 0;
        }


    }

    private void Fire()
    {
        Vector3 dir = transform.position - playerPos.position;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Instantiate(bossBullet, transform.GetChild(0).GetChild(0).transform.position, Quaternion.Euler(0, 0, z + 90));
        gameManager.bossBulletScale = 0.9f - currentPhase * 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            OnHit();
            Destroy(collision.gameObject);
        }

    }
    private void OnHit()
    {
        health--;
        StartCoroutine("ToRed");
        if (health == 0)
        {
            gameMg.GetComponent<Stage1GameManager>().CreateBoss(currentPhase, transform);
            Destroy(gameObject);
        }
    }
    IEnumerator ToRed()
    {
        spriteRenderer.color = new Color(1, 0.4f, 0.4f, 1);
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;
    }
}