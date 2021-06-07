using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangaer : MonoBehaviour
{
    public GameObject enemy;
    private float spawnDelay=1;
    private float curSpawnDelay=1;

    private float spawnRadius = 9;
    float timer = 0;

    private Vector3 spawnPos;

    private void Update()
    {
        timer += Time.deltaTime;

        if (spawnDelay > 0.05)
            spawnDelay -= Time.deltaTime * 0.01f;

        if (timer > curSpawnDelay){
            Debug.Log("enemy Spawn");
            SpawnEnemy();
            curSpawnDelay = Random.Range(spawnDelay*0.8f, spawnDelay*1.2f);
            timer = 0;
        }
            
    }

    void SpawnEnemy()
    {
        spawnPos = Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(enemy, spawnPos, transform.rotation);

    }

}
