using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class UIMergeController : MonoBehaviour
{
    [SerializeField] private Vector3[] defaultPos;
    [SerializeField] private UIMergePoint[] mergePoints;
    [SerializeField] private UITame uiTame;
    [SerializeField] private Transform toPoint;
    public string FavFood;
    public Sprite FavFoodSprite;
    [SerializeField]private int curPoint;

    public float DefaultTameLevel;
    public float CurrentTameLevel;
    private void Start()
    {
        curPoint = 0;
        EventManager.Instance.OnFoodEat += OnFoodEaten;
        defaultPos = new Vector3[3];
        for (int i = 0; i < defaultPos.Length; i++)
        {
            defaultPos[i] = mergePoints[i].transform.position;
        }
        
    }

    public void PointUp()
    {
        mergePoints[mergePoint].PopupIcon(FavFoodSprite);
    }

    public void PointDown()
    {
        mergePoints[mergePoint].LostPoint();
    }

    private IEnumerator MergeAllPoints(float _value)
    {
        mergePoints[0].transform.DOMoveX(mergePoints[0].transform.position.x - 2, 0.25f);
        mergePoints[2].transform.DOMoveX(mergePoints[1].transform.position.x + 2, 0.25f);
        yield return new WaitForSeconds(0.4f);
        mergePoints[0].transform.DOMoveX(mergePoints[1].transform.position.x, 0.25f);
        mergePoints[2].transform.DOMoveX(mergePoints[1].transform.position.x, 0.25f);
        yield return new WaitForSeconds(0.15f);
        mergePoints[1].transform.DOScale(Vector3.one * 2,0.5f);
        yield return new WaitForSeconds(0.1f);
        mergePoints[0].gameObject.SetActive(false);
        mergePoints[2].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.35f);
        mergePoints[1].transform.DOMove(toPoint.position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        CurrentTameLevel += _value;
        uiTame.UpdateTameBar(CurrentTameLevel, DefaultTameLevel);
        for (int i = 0; i < mergePoints.Length; i++)
        {
            if(i>=mergePoint) mergePoints[i].FadeIcon();
            
            mergePoints[i].transform.position = defaultPos[i];
            mergePoints[i].gameObject.SetActive(true);
        }
        mergePoints[1].transform.localScale=Vector3.one;
    }
    private int mergePoint = 0;
    private int mergeMistake = 0;
    private void OnFoodEaten(string _foodId, float _value)
    {
        if (_foodId == FavFood)
        {
            PointUp();
            mergePoint++;
            if (mergePoint >= 3)
            {
                curPoint = 0;
                mergePoint = 0;
                mergeMistake = 0;
                StartCoroutine(MergeAllPoints(_value));
            }
        }
        else
        {
            mergePoint--;
            if (mergePoint < 0)
            {
                mergePoint = 0;
            }
            PointDown();
            mergeMistake++;
            if (mergeMistake >= 3)
            {
                mergeMistake = 0;
                CurrentTameLevel -= _value;
                if (CurrentTameLevel < 0) CurrentTameLevel = 0;
                uiTame.UpdateTameBar(CurrentTameLevel, DefaultTameLevel);
            }

        }


    }
}
