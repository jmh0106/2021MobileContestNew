using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseSkill : MonoBehaviour
{
    GameObject player;
    private float timer = 0;
    private float BossSkillCool = 6;
    public Sprite[] bossEyeSprites;
    private int spriteN=1;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > BossSkillCool)
        {
            spriteN = (spriteN + 1) % 2;
            Debug.Log(spriteN);
            GameObject[] bossEyes = GameObject.FindGameObjectsWithTag("BossEye");
            for (int i = 0; i < bossEyes.Length; i++)
            {
                SpriteRenderer bossEyeSpriteRenderer = bossEyes[i].GetComponent<SpriteRenderer>();
                bossEyeSpriteRenderer.sprite = bossEyeSprites[spriteN];

            }
            player.GetComponent<Player>().ReverseDirection();
            timer = 0;
        }
    }
}
