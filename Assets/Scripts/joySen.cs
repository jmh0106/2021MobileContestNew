using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joySen : MonoBehaviour
{
    public static float joystickSen = 0.5f;
    public static float BgmVol = 1f;
    public static float SndEffVol = 1f;

    private void Awake()
    {
        var obj = FindObjectsOfType<joySen>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
