using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialPlayer : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    private float moveSpeed = 6;
    float bulletCoolTime = 0.2f;
    private Vector3 lastDirection = Vector3.up;
    private UIManager _UIManager;

    public GameObject DestroyEffect;
    public GameObject bullet;
    public GameObject tutorialManager;

    float timer = 0;

    public bool isBulletOn = false;
    public bool isMoveOn = false;
    public bool isDie = false;
    bool isStep2 = true;
    private void Awake()
    {
        //ending = tutorialManager.GetComponent<Ending>();
    }

    void Update()
    {
        if(isMoveOn)
            Move();

        if (isBulletOn)
        {
            timer += Time.deltaTime;

            if (timer > bulletCoolTime)
            {
                Fire();
                timer = 0;
            }
        }
    }
    private void PlayerMoved()
    {
        
            tutorialManager.GetComponent<TutorialManager>().OnStep3();
    }

    private void Move()
    {
        float x = joystick.joyVec.x;
        float y = joystick.joyVec.y;

        if (x != 0 || y != 0)
        {
            transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
            //회전
            float z = Mathf.Atan2(y,x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, z-90);
            if (isStep2)
            {
                Invoke("PlayerMoved", 2);
                isStep2 = false;
            }
            
        }


        //화면 밖으로 못 나가게
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = worldPos;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h, v, 0) * moveSpeed * Time.deltaTime;

    }


    void Fire()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TutorialEnemy")
        {
            Debug.Log("tutorialFinish");
            tutorialManager.GetComponent<TutorialManager>().OnStep5();
            //ending.EndGame(false);
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

}
