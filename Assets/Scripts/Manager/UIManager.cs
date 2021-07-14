using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum _STAGESTATE
    {
        LOBBY,
        STAGE_SELECT,
        STAGE
    };

    public int Coin;
    public int PassivePoint;

    public _STAGESTATE STAGESTATE;
    public GameObject CoinShopPanel;
    public GameObject PassiveShopPanel;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI PassivePointText;
    public PlayableDirector playableDirector;
    public GameObject[] GameUI;
    public GameObject[] GameCategoryUI;

    public Image[] CoinShopUI;
    public int[] CoinShopLevel;
    public Sprite[] CoinLevelUpSprite;

    public Image[] PassiveShopUI;
    public bool[] PassiveShopLock;
    public Sprite[] PassiveShopUnlockSprite;

    public GameObject[] EndingUI;
    public Sprite[] MonitorSprite; // Clear가 앞부분

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        MakePlayerPrefs();
    }

    public void MakePlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Player_Coin"))
            PlayerPrefs.SetInt("Player_Coin", 0);
        Coin = PlayerPrefs.GetInt("Player_Coin");

        if (!PlayerPrefs.HasKey("Player_Passive_Point"))
            PlayerPrefs.SetInt("Player_Passive_Point", 0);
        PassivePoint = PlayerPrefs.GetInt("Player_Passive_Point");

        for (int i = 0; i < 8; i++)
        {
            if (!PlayerPrefs.HasKey("Player_Skill_Level_" + i))
                PlayerPrefs.SetInt("Player_Skill_Level_" + i, 0);
            CoinShopLevel[i] = PlayerPrefs.GetInt("Player_Skill_Level_" + i);
        }

        for (int i = 0; i < 3; i++)
        {
            if (!PlayerPrefs.HasKey("Player_Passive_Lock_" + i))
                PlayerPrefs.SetInt("Player_Passive_Lock_" + i, 0);
            PassiveShopLock[i] = (PlayerPrefs.GetInt("Player_Passive_Lock_" + i) == 0) ? true : false;
        }
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("Player_Coin", 0);
        PlayerPrefs.SetInt("Player_Passive_Point", 0);
        for (int i = 0; i < 8; i++)
            PlayerPrefs.SetInt("Player_Skill_Level_" + i, 0);
        for (int i = 0; i < 3; i++)
            PlayerPrefs.SetInt("Player_Passive_Lock_" + i, 0);
    }

    // 게임 멈춤
    public void PauseGame()
    {
        Time.timeScale = .1f;

        GameUI[5].SetActive(false);
        for (int i = 6; i < 10; i++)
            GameUI[i].SetActive(true);
        GameUI[15].SetActive(true);

        GameUI[7].LeanMoveLocalY(-350, .02f);
        GameUI[8].LeanMoveLocalY(-550, .02f);
        GameUI[9].LeanMoveLocalY(-750, .02f).setOnComplete(TimeStop);
    }

    // 게임 재개
    public void ContinueGame()
    {
        Time.timeScale = 1;

        GameUI[5].SetActive(true);
        for (int i = 6; i < 10; i++)
            GameUI[i].SetActive(false);

        GameUI[15].SetActive(false);
        GameUI[7].LeanMoveLocalY(-150, .001f);
        GameUI[8].LeanMoveLocalY(-150, .001f);
        GameUI[9].LeanMoveLocalY(-150, .001f);
    }

    // 게임 재시작
    public void RestartGame()
    {
        Time.timeScale = 1;

        GameUI[5].SetActive(true);
        for (int i = 6; i < 10; i++)
            GameUI[i].SetActive(false);

        GameUI[15].SetActive(false);
        GameUI[7].LeanMoveLocalY(-150, .001f);
        GameUI[8].LeanMoveLocalY(-150, .001f);
        GameUI[9].LeanMoveLocalY(-150, .001f);
        GameUI[1].SetActive(true);
        GameUI[1].transform.position = new Vector3(10000, 0, 0);

        ResetEnding();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 게임 종료
    public void ExitGame()
    {
        Application.Quit();
    }

    // 로비로 나가기
    public void ExitRobby()
    {
        Time.timeScale = 1;

        GameUI[5].SetActive(true);
        for (int i = 6; i < 10; i++)
            GameUI[i].SetActive(false);

        GameUI[15].SetActive(false);
        GameUI[7].LeanMoveLocalY(-150, .001f);
        GameUI[8].LeanMoveLocalY(-150, .001f);
        GameUI[9].LeanMoveLocalY(-150, .001f);
        GameUI[1].transform.position = new Vector3(10000, 0, 0);
        GameUI[1].SetActive(true);

        ResetEnding();

        SceneManager.LoadScene("StageSelectScene");
    }

    //게임 시작
    public void StartGame()
    {
        StartCoroutine("PlayGameCoroutine");
        playableDirector.Play();
    }

    // 튜토리얼 시작
    public void TutorialGame()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    // 세팅 패널 열기
    public void SettingPanel()
    {
        GameUI[0].SetActive(true);
    }

    // 상점 패널 열기
    public void ShopPanel()
    {
        GameUI[16].SetActive(true);
        CoinPassiveUpdate();
        CoinShopUpgradeUpdate();
        PassiveShopUpgradeUpdate();
    }

    public void CoinPassiveUpdate()
    {
        CoinText.text = Coin.ToString();
        PassivePointText.text = PassivePoint.ToString();
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

        if (Coin < (CoinShopLevel[ButtonNum] + 1) * 1000)
            return;

        Coin -= (CoinShopLevel[ButtonNum] + 1) * 1000;
        CoinShopLevel[ButtonNum]++;
        CoinPassiveUpdate();
        CoinShopUpgradeUpdate();
        PlayerPrefs.SetInt("Player_Coin", Coin);
        PlayerPrefs.SetInt("Player_Skill_Level_" + ButtonNum, CoinShopLevel[ButtonNum]);
    }

    public void CoinShopUpgradeUpdate()
    {
        for (int i = 0; i < 8; i++)
        {
            CoinShopUI[i].sprite = CoinLevelUpSprite[CoinShopLevel[i]];
            CoinShopUI[i].gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ((CoinShopLevel[i] + 1) * 1000).ToString();
        }
    }

    public void PassiveShopUpgradeButton(int ButtonNum)
    {
        if (PassiveShopLock[ButtonNum] == false)
            return;

        if (PassivePoint < 1)
            return;

        PassivePoint--;
        CoinPassiveUpdate();
        PassiveShopLock[ButtonNum] = false;
        PassiveShopUpgradeUpdate();
        PlayerPrefs.SetInt("Player_Passive_Point", PassivePoint);
        PlayerPrefs.SetInt("Player_Passive_Lock_" + ButtonNum, 1);
    }

    public void PassiveShopUpgradeUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PassiveShopLock[i] == false)
                PassiveShopUI[i].sprite = PassiveShopUnlockSprite[i];
        }
    }

    void TimeStop()
    {
        Time.timeScale = 0;
    }

    IEnumerator PlayGameCoroutine()
    {
        yield return new WaitForSeconds(3f);
        playableDirector.Stop();
        SceneManager.LoadScene("StageSelectScene");
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

    public void BossSceneChange(string StageNum)
    {
        SceneManager.LoadScene("Boss" + StageNum);
    }

    public void HuntSceneChange(string StageNum)
    {
        SceneManager.LoadScene("HuntingScene" + StageNum);
    }

    public void EndingStart(bool isClear)
    {
        StartCoroutine("Ending", isClear);
    }

    IEnumerator Ending(bool isClear)
    {
        Time.timeScale = .1f;
        int FinalScore = int.Parse(GameUI[3].GetComponent<TextMeshProUGUI>().text);

        if (SceneManager.GetActiveScene().name.Contains("Hunt"))
            Coin += FinalScore;
        PlayerPrefs.SetInt("Player_Coin", Coin);

        GameUI[3].SetActive(false);

        if (!isClear)
            LeanTween.moveY(EndingUI[1], 0, .2f);

        EndingUI[0].GetComponent<SpriteRenderer>().sprite = MonitorSprite[(isClear) ? 0 : 1];
        LeanTween.value(EndingUI[2], new Color(0, 0, 0, 0), new Color(0, 0, 0, .4f), .1f);
        LeanTween.value(EndingUI[0], new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), .1f);

        yield return new WaitForSeconds(.2f);

        LeanTween.value(0, FinalScore, .2f).setOnUpdate((float val) =>
        {
            EndingUI[3].GetComponent<TextMeshProUGUI>().text = ((int)val).ToString();
        });

        yield return new WaitForSeconds(.2f);

        EndingUI[4].SetActive(true);
        EndingUI[5].SetActive(true);
        LeanTween.alpha(EndingUI[4].GetComponent<RectTransform>(), 1f, .1f);
        LeanTween.alpha(EndingUI[5].GetComponent<RectTransform>(), 1f, .1f).setOnComplete(TimeStop);
    }

    public void ResetEnding()
    {
        EndingUI[4].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        EndingUI[5].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        EndingUI[4].SetActive(false);
        EndingUI[5].SetActive(false);
        GameUI[3].SetActive(true);

        EndingUI[0].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        EndingUI[2].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        EndingUI[1].transform.position = new Vector3(0, 14, 0);
        EndingUI[3].GetComponent<TextMeshProUGUI>().text = "";
        Time.timeScale = 1f;
        GameUI[3].GetComponent<ScoreManager>().curScore = 0;
    }
}
