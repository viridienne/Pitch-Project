using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TamingGameplay : MonoBehaviour
{
    [SerializeField] private UITame uiTame;

    [SerializeField] private TameInfoSO tameInfoConfig;
    [SerializeField] private FoodConfigSO foodConfigSo;

    public TameInfo SpawnMonster;

    public FoodInfo FoodInfo;

    public float DefaultTameLevel;
    public float CurrentTameLevel;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnFoodEat += OnFoodEaten;
    }
    
    [Button]
    public void StartTameGameplay()
    {
        SpawnMonster = null;
        FoodInfo = null;

        SpawnMonster = tameInfoConfig.TameInfoList[0];
        
        DefaultTameLevel = SpawnMonster.TameLevel;
        CurrentTameLevel = 0;
        var _fav = SpawnMonster.FavroriteID;
        FoodInfo = foodConfigSo.GetFoodInfo(_fav);
        
        SpawnFoodManager.Instance.StartSpawningObject(FoodInfo);
        uiTame.SetInfoText(SpawnMonster.Name,SpawnMonster.Region,SpawnMonster.Favorite,SpawnMonster.Description);
    }

    private void OnFoodEaten(string _foodId, float _value)
    {
        if (_foodId == FoodInfo.FoodID)
        {
            CurrentTameLevel += _value;
            uiTame.UpdateTameBar(CurrentTameLevel, DefaultTameLevel);
        }
    }
}
