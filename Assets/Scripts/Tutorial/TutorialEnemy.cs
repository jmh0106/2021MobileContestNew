using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnemy : MonoBehaviour
{
    public ScoreManager ScoreManager;
    public float speed;
    public int health;
    private Transform playerPos;
    public SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    public GameObject item;
    public float ChaseTime = 999;
    private float t = 0;
    private float t2 = 0;
    private int MonsterAdditionScore = 30;
    private int spriteNum;
    public bool isFreezing = false;
    private float freezingTimer = 0;
    public SoundManager soundManager;
    public GameObject[] DestroyEffect;
    private int maxEnemyNum;
    public bool isMove = false;
    public GameObject tutorialManager;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Score") == null)
        {
            Destroy(gameObject);
            return;
        }

        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ScoreManager = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
        rigid = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        string currentScene = SceneManager.GetActiveScene().name;

        MonsterAdditionScore = 30;
    }

    private void Update()
    {
        if (!isMove)

            t += Time.deltaTime;
        t2 += Time.deltaTime;

        if (t < ChaseTime)
        {
            Vector3 dir = transform.position - playerPos.position;
            float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, z - 90);
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
        else
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);

        if (t2 > .5f && spriteNum != 0)
        {
            t2 = 0;
            Vector2 scale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector2(-scale.x, scale.y);
        }

    }

    private void OnHit(int n)
    {
        health -= n;
        StartCoroutine("ToRed");
        if (health <= 0)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            soundManager.PlayEnemyDeath();
            ScoreManager.curScore += MonsterAdditionScore;
            Instantiate(DestroyEffect[0], transform.position, Quaternion.identity);
            tutorialManager.GetComponent<TutorialManager>().OnStep4();
            Destroy(gameObject);
        }
    }

    IEnumerator ToRed()
    {
        spriteRenderer.color = new Color(1, 0.4f, 0.4f, 1);
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            OnHit(1);
            Destroy(collision.gameObject);
        }
    }
}


  