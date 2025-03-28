using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndCreditScroll : MonoBehaviour
{
    public RectTransform endGameText;
    public float scrollSpeed = 50f; 

    void Start()
    {
        endGameText.DOAnchorPosY(2100, scrollSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {Debug.Log("End Game");});
    }
}
