using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public static SunManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    } 

    [SerializeField]
    private int sunPoint;
    public int SunPoint
    {
        get{ return sunPoint; }
    } 

    public TextMeshProUGUI sunPointText;
    private Vector3 sunPointTextPosition;
    public float produceTime = 2;
    public float produceTimer;
    public GameObject sunPrefab;

    private bool isStartProduce = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSunPointText();
        CalcSunPointTextPosition();
        StartProduce();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartProduce)
        {
            ProduceSun();
        }
    }

    private void StartProduce()
    {
        isStartProduce = true;
    }

    void ProduceSun()
    {
        produceTimer += Time.deltaTime;
        if (produceTimer > produceTime)
        {
            produceTimer = 0;
            Vector3 position = new Vector3(UnityEngine.Random.Range(-5,6.5f),6.2f,-1);
            GameObject go = GameObject.Instantiate(sunPrefab, position, Quaternion.identity);

            position.y = UnityEngine.Random.Range(-4, 3f);
            go.GetComponent<Sun>().LinearTo(position);
        }
    }

    private void UpdateSunPointText()
    {
        sunPointText.text = SunPoint.ToString();
    }

    public void SubSun(int point)
    {
        sunPoint -= point;
        UpdateSunPointText();
    }

    public void AddSun(int point)
    {
        sunPoint += point;
        UpdateSunPointText();
    }

    public Vector3 GetSunPointTextPosition()
    {
        return sunPointTextPosition;
    }

    public void CalcSunPointTextPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(sunPointText.transform.position);
        position.z = 0;
        sunPointTextPosition = position;
    }

}
