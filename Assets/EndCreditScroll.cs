using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndCreditScroll : MonoBehaviour
{
    public RectTransform creditText;
    public float scrollSpeed = 50f; 

    void Start()
    {
        if (creditText != null)
        {
            creditText.DOAnchorPosY(800, scrollSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                Debug.Log("End Credit Finished");
            });
        }
        else if (creditText == null) return;
    }
}
