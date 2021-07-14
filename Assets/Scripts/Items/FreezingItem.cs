using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingItem : MonoBehaviour
{
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
            Destroy(gameObject);
    }
}
