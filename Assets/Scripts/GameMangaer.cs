using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangaer : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;
    public GameObject[] enemyObjets;
    public float spawnDelay=1;
    public float curSpawnDelay;
    
    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (spawnDelay > 0.05)
            spawnDelay -= Time.deltaTime * 0.01f;

        if (timer > curSpawnDelay){
            SpawnEnemy();
            curSpawnDelay = Random.Range(spawnDelay*0.8f, spawnDelay*1.2f);
            timer = 0;
        }
            
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 8);
        int ranPoint = Random.Range(0, 4);
        if (ranPoint == 0)
            spawnPoint.position = new Vector3(0, Random.Range(0, 1),0);
        else if (ranPoint == 1)
            spawnPoint.position = new Vector3(1, Random.Range(0, 1),0);
        else if (ranPoint == 2)
            spawnPoint.position = new Vector3(Random.Range(0, 1), 0,0);
        else
            spawnPoint.position = new Vector3(Random.Range(0, 1), 1,0);

        if (ranEnemy < 5)
            Instantiate(enemyObjets[0],spawnPoint);
        else if (ranEnemy == 6)
            Instantiate(enemyObjets[1], spawnPoint);
        else
            Instantiate(enemyObjets[2], spawnPoint);

    }
}
