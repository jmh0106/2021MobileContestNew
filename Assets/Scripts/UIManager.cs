using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject[] GameUI;
    public GameObject joyStick;

    public void PauseGame()
    {
        foreach (GameObject UI in GameUI)
        {
            if (UI.activeSelf == true)
                UI.SetActive(false);
            else
                UI.SetActive(true);
        }

        joyStick.SetActive(false);

        Time.timeScale = 0.1f;

        GameUI[3].LeanMoveLocalY(-350, .02f);
        GameUI[4].LeanMoveLocalY(-550, .02f);
        GameUI[5].LeanMoveLocalY(-750, .02f).setOnComplete(TimeStop);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        joyStick.SetActive(true);

        GameUI[3].LeanMoveLocalY(-150, .001f);
        GameUI[4].LeanMoveLocalY(-150, .001f);
        GameUI[5].LeanMoveLocalY(-150, .001f);

        foreach (GameObject UI in GameUI)
        {
            if (UI.activeSelf == true)
                UI.SetActive(false);
            else
                UI.SetActive(true);
        }

    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage1Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitRobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LobbyScene");
    }

    public void StartGame()
    {
        StartCoroutine("PlayGameCoroutine");
        playableDirector.Play();
    }

    public void TutorialGame()
    {

    }

    void TimeStop()
    {
        Time.timeScale = 0;
    }

    IEnumerator PlayGameCoroutine()
    {
        yield return new WaitForSeconds(3f);
        playableDirector.Stop();
        SceneManager.LoadScene("Stage1Scene");
    }
}
