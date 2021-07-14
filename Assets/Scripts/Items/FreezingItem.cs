using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingItem : MonoBehaviour
{
    UIManager _UIManager;
    float timer = 5;

    private void Start()
    {
        _UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        timer += _UIManager.CoinShopLevel[6] * 0.5f;
    }

    IEnumerator Freeze()
    {
        LeanTween.value(gameObject, new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), .5f);

        yield return new WaitForSeconds(5 + _UIManager.CoinShopLevel[0] * .5f);

        LeanTween.value(gameObject, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), .5f);
        Destroy(gameObject, .5f);
    }
}
