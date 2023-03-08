using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIMergePoint : MonoBehaviour
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private CanvasGroup iconCanvasGroup;

    private bool isPopup;
    // Start is called before the first frame update
    void Start()
    {
        iconCanvasGroup.alpha = 0f;
    }

    public void PopupIcon(Sprite _sprite)
    {
        imgIcon.sprite = _sprite;
        imgIcon.transform.localScale = Vector3.zero;
        iconCanvasGroup.DOFade(1f, 0.25f);
        imgIcon.transform.DOScale(Vector3.one, 0.25f);
    }

    public void LostPoint()
    {
        imgIcon.transform.DOScale(Vector3.zero, 0.25f);
        iconCanvasGroup.DOFade(0f, 0.25f);
    }

    public void FadeIcon()
    {
        iconCanvasGroup.DOFade(0f, 0.1f);
    }
}
