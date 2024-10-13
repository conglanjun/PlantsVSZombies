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

    // Start is called before the first frame update
    void Start()
    {
        UpdateSunPointText();
        CalcSunPointTextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
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
