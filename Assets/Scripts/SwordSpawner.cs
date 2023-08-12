using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{
    public static SwordSpawner instance;

    public bool isSwordPresent;
    public Transform[] spawnPositions;
    public GameObject swordPf;

    public UniversalTimer timer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RollSwordSpawn()
    {
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            Debug.Log("Spawning the Sword of Excelsior...");
            SpawnSword();
        }
    }

    public void SpawnSword()
    {
        isSwordPresent = true;
        timer.canTrigger = false;

        int randomPosition = Random.Range(0, spawnPositions.Length);
        Instantiate(swordPf, spawnPositions[randomPosition].position, Quaternion.identity);
    }

    public void SwordUsed()
    {
        isSwordPresent = false;
        timer.canTrigger = true;
    }
}
