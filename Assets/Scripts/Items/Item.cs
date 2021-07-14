using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float itemSpeed=1.5f;
    private int curX, curY;
    private int hitNum = 0;
    public GameObject freezingArea;
    public GameObject bubble;
    public GameObject bomb;
    public GameObject TimeSlow;
    public SoundManager soundManager;
   
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        hitNum = 0;
        curX = (int)Mathf.Pow(-1, (int)Random.Range(1, 3));
        curY = (int)Mathf.Pow(-1, (int)Random.Range(1, 3));
    }

    void Update()
    {
        if (transform.position.x < -8.7 && hitNum < 3)
        {
            if (curX != 1)
                hitNum++;
            curX = 1;
        }

        if (transform.position.x > 8.7 && hitNum < 3)
        {
            if (curX != -1)
                hitNum++;
            curX = -1;
        }

        if (transform.position.y < -5.3 && hitNum < 3)
        {
            if (curY != 1)
                hitNum++;
            curY = 1;
        }

        if (transform.position.y > 5.3 && hitNum < 3)
        {
            if (curY != -1)
                hitNum++;
            curY = -1;
        }
        
        transform.Translate(new Vector3(curX, curY, transform.position.z) * itemSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int n = (int)Random.Range(0, 7);

            if (n < 2) //slow
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++)
                    enemies[i].GetComponent<Enemy>().ToSlow(0);

                if (GameObject.FindGameObjectWithTag("TimeSlow") != null)
                    Destroy(GameObject.FindGameObjectWithTag("TimeSlow"));

                soundManager.PlayTimeslowItem();
                GameObject tmp = Instantiate(TimeSlow, new Vector3(0, 0, 1), Quaternion.identity);

                Debug.Log("slow");
                
                Destroy(this.gameObject);
            }
            else if (n < 4) //freeze
            {
                Instantiate(freezingArea, transform.position, transform.rotation);
                Debug.Log("freeze");
                soundManager.PlayFreezeItem();
                Destroy(this.gameObject);
            }
            else if (n < 5) //bubble
            {
                Instantiate(bubble, transform.position, transform.rotation);
                Debug.Log("bubble");
                soundManager.PlayBubbleItem();
                Destroy(this.gameObject);
            }
            else //bomb
            {
                Instantiate(bomb, new Vector2(Random.Range(-8, 8), Random.Range(-4, 4)), transform.rotation);
                soundManager.PlaySalvationItem();
                Destroy(this.gameObject);
            }
           
        }
    }

    void ActiveBomb()
    {
        Instantiate(bomb, new Vector2(Random.Range(-8, 8), Random.Range(-4, 4)), transform.rotation);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
