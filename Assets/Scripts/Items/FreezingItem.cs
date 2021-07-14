using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingItem : MonoBehaviour
{
    UIManager _UIManager;
    float timer = 0;
    float endTimer = 5;

    private void Start()
    {
        _UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        endTimer += _UIManager.CoinShopLevel[6] * 0.5f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > endTimer)
            Destroy(gameObject);
    }
}
