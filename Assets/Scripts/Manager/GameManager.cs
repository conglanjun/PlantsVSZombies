using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PrepareUI prepareUI;
    public CardListUI cardListUI;

    private void Awake() 
    {
        Instance = this;

    }

    private void Start() {
        GameStart();
    }
    void GameStart()
    {
        Vector3 currentPosition = Camera.main.transform.position;

        Camera.main.transform.DOPath(new Vector3[]{
            currentPosition, new Vector3(5,0,-10),currentPosition},4,PathType.Linear)
            .OnComplete(ShowPrepareUI);
    }

    void ShowPrepareUI()
    {
        prepareUI.Show(OnPrepareUIComplete);        

    }

    void OnPrepareUIComplete()
    {
        SunManager.Instance.StartProduce();
        ZombieManager.Instance.StartSpawn();
        cardListUI.ShowCardList();
    }
}
