using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ending : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI EndingScoreResult;
    public GameObject[] GameUI;
    public GameObject Joystick;
    public GameObject GameButtonUI;
    public Sprite ClearMointor;
    public Sprite DieMointor;
    public SoundManager soundManager;

    public int FinalScore;
    public int CurScore = 0;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        textMeshProUGUI = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        EndingScoreResult = GameObject.FindGameObjectWithTag("EndingScore").GetComponent<TextMeshProUGUI>();
    }

    public void EndGame(bool isClear)
    {
        StartCoroutine("End", isClear);
    }

    IEnumerator End(bool isClear)
    {
        Time.timeScale = .1f;
        Joystick.SetActive(false);
        GameButtonUI.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            GameUI[i].SetActive(true);
        }

        GameUI[4].SetActive(false);

        LeanTween.value(GameUI[3], new Color(0, 0, 0, 0), new Color(0, 0, 0, .4f), .1f);
        yield return new WaitForSeconds(.1f);
        if (!isClear)
            LeanTween.moveY(GameUI[2], 0, .2f);
        GameUI[1].GetComponent<SpriteRenderer>().sprite = (isClear) ? ClearMointor : DieMointor;
        LeanTween.value(GameUI[1], new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), .1f);


        yield return new WaitForSeconds(.2f);

        FinalScore = int.Parse(textMeshProUGUI.text);

        soundManager.PlayScoreCount();
        while (FinalScore > CurScore)
        {

            if (FinalScore - CurScore > 10000)
                CurScore += 1000;
            if (FinalScore - CurScore > 1000)
                CurScore += 100;
            if (FinalScore - CurScore > 100)
                CurScore += 10;
            CurScore += 1;

            EndingScoreResult.text = CurScore.ToString();

            yield return null;
        }

        GameUI[5].SetActive(true);
        GameUI[6].SetActive(true);
        LeanTween.alpha(GameUI[5].GetComponent<RectTransform>(), 1f, .1f);
        LeanTween.alpha(GameUI[6].GetComponent<RectTransform>(), 1f, .1f).setOnComplete(TimeStop);
    }

    void TimeStop()
    {
        Time.timeScale = 0;
    }
}
