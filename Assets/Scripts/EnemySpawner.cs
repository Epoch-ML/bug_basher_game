using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> groundSpawnHolders;

    public GameObject roach;
    public GameObject fly;

    [Header("References")]
    public UniversalTimer spawnTimer;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnStateChanged;

        spawnTimer = GetComponent<UniversalTimer>();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        // foreach (Transform groundHolder in groundSpawnHolders)
        // {
        //     if (groundHolder.childCount == 0)
        //     {
        //         Instantiate(roach, groundHolder.transform.position, Quaternion.identity, groundHolder.transform);
        //     }
        // }
    }

    private void OnStateChanged(GameState state)
    {
        if (state == GameState.Game)
        {
            foreach (Transform groundHolder in groundSpawnHolders)
            {
                if (groundHolder.childCount == 0)
                {
                    Instantiate(roach, groundHolder.transform.position, Quaternion.identity, groundHolder.transform);
                }
            }

            spawnTimer.canTrigger = true;
        }
        else
        {
            spawnTimer.canTrigger = false;
        }

    }
    public void SpawnRoach()
    {
        List<Transform> emptyHolders = new List<Transform>();

        foreach (Transform groundHolder in groundSpawnHolders)
        {
            if (groundHolder.childCount == 0)
            {
                emptyHolders.Add(groundHolder);
            }
        }


        if (emptyHolders.Count != 0)
        {
            Transform randomEmptyHolder = emptyHolders[Random.Range(0, emptyHolders.Count)];
            Instantiate(roach, randomEmptyHolder.transform.position, Quaternion.identity, randomEmptyHolder.transform);
        }
    }
}
