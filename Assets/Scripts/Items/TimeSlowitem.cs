using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowitem : MonoBehaviour
{
    public float time = 0;

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 5f)
            Destroy(gameObject);
    }
}
