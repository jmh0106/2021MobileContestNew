using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    private float moveSpeed = 6;
    float bulletCoolTime = 0.2f;
    private Vector3 lastDirection = Vector3.up;
    private UIManager _UIManager;
    public GameObject shield;

    bool playerTripleBullet = false;
    bool playerShield = false;
    bool playerPet = false;

    public GameObject DestroyEffect;
    public GameObject bullet;

    float timer = 0;
    public int direction = 1;

    private void Start()
    {
        _UIManager = GameObject.Find("MainCanvas").GetComponent<UIManager>();
        joystick = GameObject.Find("JoystickBG").GetComponent<Joystick>();

        if (_UIManager.PassiveShopLock[0] == false)
            playerTripleBullet = true;

        if (_UIManager.PassiveShopLock[1] == false)
        {
            Instantiate(shield, Vector3.zero, Quaternion.identity).transform.parent = gameObject.transform;
            playerShield = true;
        }

        if (_UIManager.PassiveShopLock[2] == false)
            playerPet = true;
    }

    void Update()
    {
        if (joystick == null)
            GameObject.Find("JoystickBG").GetComponent<Joystick>();

        Move();
        timer += Time.deltaTime;

        if (timer > bulletCoolTime)
        {
            Fire();
            timer = 0;
        }
    }

    private void Move()
    {
        float x = joystick.joyVec.x * joystick.x;
        float y = joystick.joyVec.y * joystick.y;

        if (x != 0 || y != 0)
        {
            transform.position += new Vector3(x * direction, y * direction, 0) * moveSpeed * Time.deltaTime;
            //회전
            float z = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, z - 90);

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
        if ((collision.gameObject.tag == "Enemy" && !collision.gameObject.GetComponent<Enemy>().isFreezing) || collision.gameObject.tag == "Boss")
        {
            _UIManager.EndingStart(false);
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    public void ReverseDirection()
    {
        direction *= -1;
    }

    void Pet()
    {

    }

    void Shield()
    {

    }

    void TripleShot()
    {

    }
}
