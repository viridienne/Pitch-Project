using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DigitalRuby.SoundManagerNamespace;
using TMPro;

public class UITame : MonoBehaviour
{
    [SerializeField] private Image tameBar;

    [SerializeField] private TextMeshProUGUI txtInfo;

    [SerializeField] private RawImage background;

    [SerializeField] private TextMeshProUGUI txtTime;
    private float playSeconds = 180;
     private float scrollX => SpawnFoodManager.Instance.Speed;
    // Start is called before the first frame update
    void Start()
    {
        tameBar.fillAmount = 0;
        playSeconds = 180;
        StartCoroutine(IEScrollBG());
    }
    

    public void UpdateTameBar(float _value, float _maxValue)
    {
        var _curValue = _value / _maxValue;
        if (_curValue >= 0.5f)
        {
            SpawnFoodManager.Instance.IncreaseSpeed();
            EventManager.Instance.OnMonsterAnimation?.Invoke("agitated");
            SoundManager.PlayOneShotSound(AudioHelper.Instance.GetAudio("agitated"),AudioHelper.Instance.GetAudio("agitated").clip);
        }

        if (_curValue >= 1f)
        {
            EventManager.Instance.OnWin?.Invoke();
            EventManager.Instance.OnMonsterAnimation?.Invoke("gaintrust");
        }
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
            if (playSeconds <= 0)
            {
                playSeconds = 0;
            }
            txtTime.SetText($"{TimeSpan.FromSeconds(playSeconds).Minutes.ToString()}"+":"+$"{TimeSpan.FromSeconds(playSeconds).Seconds.ToString()}");
            yield return new WaitForEndOfFrame();
        }
    }
}
