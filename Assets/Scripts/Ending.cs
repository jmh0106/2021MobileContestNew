using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ending : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI EndingScoreResult;
    public SoundManager soundManager;
    public GameObject[] GameUI;
    public GameObject Joystick;
    public GameObject GameButtonUI;
    public Sprite ClearMointor;
    public Sprite DieMointor;

    int FinalScore;

    public void EndGame(bool isClear)
    {
        StartCoroutine("End", isClear);
    }

    IEnumerator End(bool isClear)
    {
        Time.timeScale = .1f;
        Joystick.SetActive(false);
        GameButtonUI.SetActive(false);
        FinalScore = int.Parse(textMeshProUGUI.text);

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

        soundManager.PlayScoreCount();

        LeanTween.value(0, FinalScore, .2f).setOnUpdate((float val) =>
        {
            EndingScoreResult.text = ((int)val).ToString();
        });

        yield return new WaitForSeconds(.2f);

        GameUI[5].SetActive(true);
        GameUI[6].SetActive(true);
        LeanTween.alpha(GameUI[5].GetComponent<RectTransform>(), 1f, .1f);
        LeanTween.alpha(GameUI[6].GetComponent<RectTransform>(), 1f, .1f).setOnComplete(TimeStop);
    }

    void TimeStop()
    {
        Time.timeScale = 0;
    }

    public void ResetEnding()
    {
        Time.timeScale = 1;
        GameButtonUI.SetActive(true);

        for (int i = 0; i < 5; i++)
            GameUI[i].SetActive(false);

        GameUI[4].SetActive(true);
    }
}
