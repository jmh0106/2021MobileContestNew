using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1GameManager : MonoBehaviour
{
    public int bossPhase = 1;
    public int bossHealth;
    public float bossScale;
    public GameObject stage1Boss;
    public GameObject player;
    public float bossBulletScale = 1;
    private int bossDieCheck = 0;
    public UIManager _UIManager;

    private void Awake()
    {
        bossHealth = 10;
        bossScale = 3;
        Instantiate(player, new Vector3(0, -3, 10), transform.rotation);
    }

    private void Start()
    {
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public void CreateBoss(int n, Transform trans)
    {
        if (n == 1)
        {
            bossPhase = 2;
            bossHealth = 7;
            bossScale = 2.5f;
            bossBulletScale = 0.8f;
            Instantiate(stage1Boss, trans.position, trans.rotation);
            Instantiate(stage1Boss, trans.position+new Vector3(3,0,trans.position.z), trans.rotation);
        }
        else if (n == 2)
        {
            bossPhase = 3;
            bossHealth = 5;
            bossScale = 2;
            bossBulletScale = 0.7f;
            Instantiate(stage1Boss, trans.position, trans.rotation);
            Instantiate(stage1Boss, trans.position + new Vector3(1, 0, trans.position.z), trans.rotation);
        }
        else if (n == 3)
        {
            bossPhase = 4;
            bossHealth = 3;
            bossScale = 1.5f;
            bossBulletScale = 0.6f;
            Instantiate(stage1Boss, trans.position, trans.rotation);
            Instantiate(stage1Boss, trans.position + new Vector3(1, 0, trans.position.z), trans.rotation);
        }
        else
        {
            bossPhase = 5;
            bossHealth = 2;
            bossScale = 1;
            bossBulletScale = 0.5f;

        }

        bossDieCheck++;

        if (bossDieCheck == 15)
        {
            if (SceneManager.GetActiveScene().name == "boss1" && _UIManager.isBossClear[0] == false)
            {
                _UIManager.isBossClear[0] = true;
                _UIManager.PassivePoint++;
                PlayerPrefs.SetInt("Player_Passive_Point", _UIManager.PassivePoint);
                PlayerPrefs.SetInt("Player_Boss_Clear_0", 1);
            }

            if (SceneManager.GetActiveScene().name == "boss2" && _UIManager.isBossClear[1] == false)
            {
                _UIManager.isBossClear[1] = true;
                _UIManager.PassivePoint++;
                PlayerPrefs.SetInt("Player_Passive_Point", _UIManager.PassivePoint);
                PlayerPrefs.SetInt("Player_Boss_Clear_1", 1);
            }

            if (SceneManager.GetActiveScene().name == "boss3" && _UIManager.isBossClear[2] == false)
            {
                _UIManager.isBossClear[2] = true;
                _UIManager.PassivePoint++;
                PlayerPrefs.SetInt("Player_Passive_Point", _UIManager.PassivePoint);
                PlayerPrefs.SetInt("Player_Boss_Clear_2", 1);
            }
        }
    }
    public int GetHealth()
    {
        return bossHealth;
    }
    public float GetScale()
    {
        return bossScale;
    }
    public int GetPhase()
    {
        return bossPhase;
    }
    
    public float GetBulletScale()
    {
        return bossBulletScale;
    }
}
