using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UITame : MonoBehaviour
{
    [SerializeField] private Image tameBar;

    [SerializeField] private TextMeshProUGUI txtInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        tameBar.fillAmount = 0;
    }
    

    public void UpdateTameBar(float _value, float _maxValue)
    {
        var _curValue = _value / _maxValue;
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
}
