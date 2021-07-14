using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{
    Sprite BombSprite;
    UIManager _UIManager;
    int Bombtime;

    private void Start()
    {
        _UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        StartCoroutine("Bomb");
    }

    IEnumerator Bomb()
    {
        LeanTween.value(gameObject, new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), 1f);

        yield return new WaitForSeconds(3 + _UIManager.CoinShopLevel[0] * .3f);

        LeanTween.value(gameObject, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 1f);
        Destroy(gameObject, 1f);
    }
}