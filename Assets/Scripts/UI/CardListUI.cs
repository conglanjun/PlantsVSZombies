using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardListUI : MonoBehaviour
{
    public List<Card> cardList;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        DisableCardList();
        // ShowCardList();

    }
    public void ShowCardList()
    {
        GetComponent<RectTransform>().DOAnchorPosY(-48, 1f);
        EnableCardList();


    }
    public void DisableCardList()
    {
        foreach(Card card in cardList)
        {
            card.DisableCard();
        }

    }

    void EnableCardList()
    {
        foreach(Card card in cardList)
        {
            card.EnableCard();
        }
    }
}
