using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CardState
{
    Disable,
    Cooling,
    WaitingSun,
    Ready
}

public enum PlantType
{
    SunFlower,
    PeaShooter
}

public class Card : MonoBehaviour
{
    private CardState cardState = CardState.Disable;

    public PlantType plantType = PlantType.SunFlower;

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
        if (SunManager.Instance.SunPoint < needSunPoint)
        {
            TransitionToWaitingSun();
        }

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

    void TransitionToCooling()
    {
        cardState = CardState.Cooling;
        cdTimer = 0;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(true);
    }

    public void onClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click, 1.0F);
        if (cardState == CardState.Disable) return;
        if (SunManager.Instance.SunPoint < needSunPoint) return;

        bool isSuccess = HandManager.Instance.AddPlant(plantType);
        if (isSuccess) 
        {
            SunManager.Instance.SubSun(needSunPoint);
            TransitionToCooling();
        }
    }

    public void DisableCard()
    {
        cardState = CardState.Disable;
    }

    public void EnableCard()
    {
        TransitionToCooling();
    }
}

