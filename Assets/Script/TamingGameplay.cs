using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class TamingGameplay : MonoBehaviour
{
    [SerializeField] private UITame uiTame;
    [SerializeField] private UIMergeController mergeController;
    [SerializeField] private TameInfoSO tameInfoConfig;
    [SerializeField] private FoodConfigSO foodConfigSo;
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private Transform background;
    [SerializeField] private float startX;
    [SerializeField] private float endX;
    public TameInfo SpawnMonster;

    public FoodInfo[] FoodInfo;


    // Start is called before the first frame update
    void Start()
    {
    }
    
    [Button]
    public void StartTameGameplay()
    {
        SpawnMonster = null;
        FoodInfo = new FoodInfo[3];

        SpawnMonster = tameInfoConfig.TameInfoList[0];
        
        mergeController.DefaultTameLevel = SpawnMonster.TameLevel;
        mergeController.CurrentTameLevel = 0;
        mergeController.FavFood = SpawnMonster.FavroriteID;
        FoodInfo[0] = foodConfigSo.GetFoodInfo("CARROT");
        FoodInfo[1] = foodConfigSo.GetFoodInfo("BERRY");
        FoodInfo[2] = foodConfigSo.GetFoodInfo("SPINACH");
        
        mergeController.FavFoodSprite = getFoodSprite(SpawnMonster.FavroriteID);
        SpawnFoodManager.Instance.StartSpawningObject(FoodInfo);
        uiTame.SetInfoText(SpawnMonster.Name,SpawnMonster.Region,SpawnMonster.Favorite,SpawnMonster.Description);
    }

    private Sprite getFoodSprite(string _id)
    {
       var _s = FoodInfo.First(t => t.FoodID.Equals(_id));
       return _s.icon;
    }
}
