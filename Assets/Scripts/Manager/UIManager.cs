using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum _STAGESTATE
    {
        LOBBY,
        STAGE_SELECT,
        STAGE
    };

    public _STAGESTATE STAGESTATE;
    public GameObject CoinShopPanel;
    public GameObject PassiveShopPanel;
    public PlayableDirector playableDirector;
    public GameObject[] GameUI;
    public GameObject[] GameCategoryUI;

    public Image[] CoinShopUI;
    public int[] CoinShopLevel;
    public Sprite[] CoinLevelUpSprite;

    public Image[] PassiveShopUI;
    public bool[] PassiveShopLock;
    public Sprite[] PassiveShopUnlockSprite;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ���� ����
    public void PauseGame()
    {
        Time.timeScale = .1f;

        GameUI[5].SetActive(false);
        for (int i = 6; i < 10; i++)
            GameUI[i].SetActive(true);

        GameUI[7].LeanMoveLocalY(-350, .02f);
        GameUI[8].LeanMoveLocalY(-550, .02f);
        GameUI[9].LeanMoveLocalY(-750, .02f).setOnComplete(TimeStop);
    }

    // ���� �簳
    public void ContinueGame()
    {
        Time.timeScale = 1;

        GameUI[5].SetActive(true);
        for (int i = 6; i < 10; i++)
            GameUI[i].SetActive(false);

        GameUI[7].LeanMoveLocalY(-150, .001f);
        GameUI[8].LeanMoveLocalY(-150, .001f);
        GameUI[9].LeanMoveLocalY(-150, .001f);
    }

    // ���� �����
    public void RestartGame()
    {
        Time.timeScale = 1;

        GameUI[5].SetActive(true);
        for (int i = 6; i < 10; i++)
            GameUI[i].SetActive(false);

        GameUI[7].LeanMoveLocalY(-150, .001f);
        GameUI[8].LeanMoveLocalY(-150, .001f);
        GameUI[9].LeanMoveLocalY(-150, .001f);

        SceneManager.LoadScene("Stage1Scene");
    }

    // ���� ����
    public void ExitGame()
    {
        Application.Quit();
    }

    // �κ�� ������
    public void ExitRobby()
    {
        Time.timeScale = 1;

        GameUI[5].SetActive(true);
        for (int i = 6; i < 10; i++)
            GameUI[i].SetActive(false);

        GameUI[7].LeanMoveLocalY(-150, .001f);
        GameUI[8].LeanMoveLocalY(-150, .001f);
        GameUI[9].LeanMoveLocalY(-150, .001f);

        SceneManager.LoadScene("StageSelectScene");
    }

    //���� ����
    public void StartGame()
    {
        StartCoroutine("PlayGameCoroutine");
        playableDirector.Play();
    }

    // Ʃ�丮�� ����
    public void TutorialGame()
    {

    }

    // ���� �г� ����
    public void SettingPanel()
    {
        GameUI[0].SetActive(true);
    }

    // ���� �г� ����
    public void ShopPanel()
    {
        
    }

    public void SetCoinShopPanelUp(bool _apple)
    {
        if (_apple)
            CoinShopPanel.transform.SetAsLastSibling();
        else
            PassiveShopPanel.transform.SetAsLastSibling();
    }

    public void CoinShopUpgradeButton(int ButtonNum)
    {
        if (CoinShopLevel[ButtonNum] == 5)
            return;

        // ��尡 �����ϴٸ� ����
        CoinShopUI[ButtonNum].sprite = CoinLevelUpSprite[++CoinShopLevel[ButtonNum]];
    }

    public void PassiveShopUpgradeButton(int ButtonNum)
    {
        if (PassiveShopLock[ButtonNum] == false)
            return;

        PassiveShopLock[ButtonNum] = false;
        PassiveShopUI[ButtonNum].sprite = PassiveShopUnlockSprite[ButtonNum];
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("LobbyScene"))
            STAGESTATE = _STAGESTATE.LOBBY;
        else if (scene.name.Equals("StageSelectScene"))
            STAGESTATE = _STAGESTATE.STAGE_SELECT;
        else
            STAGESTATE = _STAGESTATE.STAGE;

        ChangeUIActive();
    }

    private void ChangeUIActive()
    {
        for (int i = 1; i < 3; i++)
            GameCategoryUI[i].SetActive(false);

        if (STAGESTATE == _STAGESTATE.STAGE_SELECT)
            GameCategoryUI[1].SetActive(true);
        if (STAGESTATE == _STAGESTATE.STAGE)
            GameCategoryUI[2].SetActive(true);
    }

    public void BossHuntSceneChange(string StageType, string StageNum)
    {
        SceneManager.LoadScene("");
    }
}