using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text score;
    private int curScore;
    float timer;
    float plusScoreTime=0.5f;

    void Start()
    {
        timer = 0;
        curScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > plusScoreTime)
        {
            curScore += 100;
            timer = 0;
        }
        score.text = curScore.ToString();

    }
}
