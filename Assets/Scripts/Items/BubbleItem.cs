using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleItem : MonoBehaviour
{
    UIManager _UIManager;
    private float curX, curY;
    private int hitNum = 0;
    private int hitMax = 0;
    public float bubbleSpeed = 10;
    
    void Start()
    {
        _UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        hitMax = _UIManager.CoinShopLevel[2] + 2;
        float BubbleSize = 1.5f + _UIManager.CoinShopLevel[2] * .1f;
        gameObject.transform.localScale = new Vector2(BubbleSize, BubbleSize);

        curX = (int)Mathf.Pow(-1, (int)Random.Range(1, 2));
        curY = (int)Mathf.Pow(-1, (int)Random.Range(1, 2));
    }

    void Update()
    {
        if (transform.position.x < -7.8 || transform.position.x > 7.8 && hitNum < hitMax)
        {
            curX *= -1;
            hitNum++;
        }

        if (transform.position.y < -4.5 || transform.position.y > 4.5 && hitNum < hitMax)
        {
            curY *= -1;
            hitNum++;
        }

        transform.Translate(new Vector3(curX, curY, transform.position.z) * bubbleSpeed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}




