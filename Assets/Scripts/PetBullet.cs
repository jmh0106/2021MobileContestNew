using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class PetBullet : MonoBehaviour
{

    bool curSceneBoss = false;

    //GameObject targetEnemy;
    // Start is called before the first frame update
    void Start()
    {
        //targetEnemy = FindNearestEnemy();
        if (SceneManager.GetActiveScene().name == "Boss1" || SceneManager.GetActiveScene().name == "Boss2" || SceneManager.GetActiveScene().name == "Boss3")
            curSceneBoss = true;


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, FindNearestEnemy().transform.position, 0.05f);
    }

    private GameObject FindNearestEnemy()
    {

        GameObject neareastObject;


        if (curSceneBoss)
        {
            //var objects = GameObject.FindGameObjectsWithTag("BossSprite").ToList();

            //neareastObject = objects
            //    .OrderBy(obj =>
            //    {
            //        return Vector3.Distance(transform.position, obj.transform.position);
            //    })
            //.FirstOrDefault();
            neareastObject = GameObject.FindGameObjectWithTag("BossEye");
        }
        else
        {
            var objects = GameObject.FindGameObjectsWithTag("Enemy").ToList();

            neareastObject = objects
                .OrderBy(obj =>
                {
                    return Vector3.Distance(transform.position, obj.transform.position);
                })
            .FirstOrDefault();

        }

        return neareastObject;
    }
}