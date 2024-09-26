using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CardState
{
    Cooling,
    WaitingSun,
    Ready
}

public class Card : MonoBehaviour
{
    private CardState cardState = CardState.Cooling;

    public GameObject cardLight;
    public GameObject cardGray;
    public Image cardMask;

    [SerializeField]
    private float cdTime = 2;
    private float cdTimer = 0;

    [SerializeField]
    private int needSunPoint = 50;

    // Update is called once per frame
    void Update()
    {
        switch(cardState)
        {
            case CardState.Cooling:
                CoolingUpdate();
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
            default:
                break;
        }
    }

    void CoolingUpdate()
    {
        cdTimer += Time.deltaTime;
        cardMask.fillAmount = (cdTime - cdTimer) / cdTime;
        if (cdTimer >= cdTime) 
        {
            TransitionToWaitingSun();
        }
    }

    void WaitingSunUpdate()
    {
        if (SunManager.Instance.SunPoint >= needSunPoint)
        {
            TransitionToReady();
        }
    }

    void ReadyUpdate()
    {

    }

    void TransitionToWaitingSun()
    {
        cardState = CardState.WaitingSun;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }

    void TransitionToReady()
    {
        cardState = CardState.Ready;
        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }
}

