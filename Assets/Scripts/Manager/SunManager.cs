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

    // Start is called before the first frame update
    void Start()
    {
        UpdateSunPointText();
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
}
