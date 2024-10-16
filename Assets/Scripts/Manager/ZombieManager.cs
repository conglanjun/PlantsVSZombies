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

    private List<Zombie> zombieList = new List<Zombie>();
    private void Awake() 
    {
        Instance = this;
    }

    private void Update() {
        if (spawnState == SpawnState.End && zombieList.Count == 0)
        {
            GameManager.Instance.GameEndSuccess();
        }
    }

    private void Start() {
        // StartSpawn();
    }

    public void Pause()
    {
        spawnState = SpawnState.End;
        foreach(Zombie zombie in zombieList)
        {
            zombie.TransitionToPause();
        }
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
        AudioManager.Instance.PlayClip(Config.lastwave);
        for (int i = 0; i < 10; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }
        spawnState = SpawnState.End;
    }

    private void SpawnARandomZombie()
    {
        if (spawnState == SpawnState.Spwaning)
        {
            int index = Random.Range(0, spawnPointList.Length);
            GameObject go = GameObject.Instantiate(zombiePrefab, spawnPointList[index].position, Quaternion.identity);
            zombieList.Add(go.GetComponent<Zombie>());
            go.GetComponent<SpriteRenderer>().sortingOrder = spawnPointList[index].GetComponent<SpriteRenderer>().sortingOrder;
        }
    }

    public void RemoveZombie(Zombie zombie)
    {
        zombieList.Remove(zombie);
    }
}
