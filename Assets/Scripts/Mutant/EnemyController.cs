using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject mutant;
    public Transform player;
    public static int countEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        countEnemy = 0; 
        Spawn();
    }

    public void Update()
    {
        if (countEnemy < 9) Spawn();
    }

    void Spawn()
    {
        while(countEnemy < 9)
        {
            if (PlayerUI.health != 20) PlayerUI.health += 1;
            float posX = player.position.x + Random.Range(-30, 30);
            float posZ = player.position.z + Random.Range(-30, 30);
            enemy[countEnemy] = Instantiate(mutant, new Vector3(posX,mutant.transform.position.y,posZ), Quaternion.identity);
            countEnemy++;
        }
    }
}
