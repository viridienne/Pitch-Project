using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UITame : MonoBehaviour
{
    [SerializeField] private Image tameBar;

    [SerializeField] private TextMeshProUGUI txtInfo;

    [SerializeField] private RawImage background;

    [SerializeField] private TextMeshProUGUI txtTime;
    private float playSeconds = 300;
     private float scrollX => SpawnFoodManager.Instance.Speed;
    // Start is called before the first frame update
    void Start()
    {
        tameBar.fillAmount = 0;
        playSeconds = 300;
        StartCoroutine(IEScrollBG());
    }
    

    public void UpdateTameBar(float _value, float _maxValue)
    {
        var _curValue = _value / _maxValue;
        if(_curValue>=0.5f) SpawnFoodManager.Instance.IncreaseSpeed();
        DOVirtual.Float(tameBar.fillAmount, _curValue, 0.15f, amount =>
        {
            tameBar.fillAmount = amount;
        });
    }

    public void SetInfoText(string _name, string _region, string _like, string _desc)
    {
        string content = "";
        content += $"NAME: {_name}\n";
        content += $"REGION: {_region}\n";
        content += $"LIKE: {_like}\n";
        content += $"STATUS: {_desc}\n";
        
        txtInfo.SetText(content);
    }
    private IEnumerator IEScrollBG()
    {
        while (true)
        {
            background.uvRect = new Rect(background.uvRect.position + new Vector2(scrollX/25, 0) * Time.deltaTime,background.uvRect.size);
            playSeconds -= Time.deltaTime;
            txtTime.SetText($"{TimeSpan.FromSeconds(playSeconds).Minutes}"+":"+$"{TimeSpan.FromSeconds(playSeconds).Seconds}");
            yield return new WaitForEndOfFrame();
        }
    }
}
