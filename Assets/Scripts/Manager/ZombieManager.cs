using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnState
{
    NotStart,
    Spwaning,
    End
}

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set;}
    private SpawnState spawnState = SpawnState.NotStart;

    public Transform[] spawnPointList;
    public GameObject zombiePrefab;
    private void Awake() 
    {
        Instance = this;
    }

    private void Start() {
        // StartSpawn();
    }

    public void StartSpawn()
    {
        spawnState = SpawnState.Spwaning;
        StartCoroutine(SpawnZombie());
    }

    IEnumerator SpawnZombie()
    {
        // first 5
        for (int i = 0; i < 5; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }
        print("waiting for second!");
        yield return new WaitForSeconds(1);
        // second 10
        for (int i = 0; i < 10; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }
        print("waiting for thrid!");
        yield return new WaitForSeconds(1);
        // third 20
        for (int i = 0; i < 10; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }
    }

    private void SpawnARandomZombie()
    {
        int index = Random.Range(0, spawnPointList.Length);
        GameObject go = GameObject.Instantiate(zombiePrefab, spawnPointList[index].position, Quaternion.identity);
    }
}
