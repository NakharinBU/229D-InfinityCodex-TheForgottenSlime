using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndCreditScroll : MonoBehaviour
{
    public RectTransform endGameText;
    public RectTransform creditText;
    public float scrollSpeed = 50f; 

    void Start()
    {
        if (endGameText != null)
        {
            endGameText.DOAnchorPosY(2100, scrollSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                Debug.Log("End Game");
            });
        }
        else if (endGameText == null) return;

        if (creditText != null)
        {
            creditText.DOAnchorPosY(2100, scrollSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                SceneManager.LoadScene(0);
            });
        }
        else if (creditText == null) return;
    }
}
