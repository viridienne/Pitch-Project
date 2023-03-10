using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DigitalRuby.SoundManagerNamespace;
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
    [SerializeField] private Animator monsterAnimator;
    public TameInfo SpawnMonster;

    public FoodInfo[] FoodInfo;


    // Start is called before the first frame update
    void Start()
    {
        StartTameGameplay();
        // EventManager.Instance.TriggerMonster += OnTriggerMonster;
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
        EventManager.Instance.OnMonsterAnimation?.Invoke("entrance");
        SoundManager.PlayOneShotSound(AudioHelper.Instance.GetAudio("entrance"),AudioHelper.Instance.GetAudio("entrance").clip);
        SoundManager.PlayLoopingMusic(AudioHelper.Instance.GetAudio("BGM"),0.4f,0.5f,true);
    }

    private Sprite getFoodSprite(string _id)
    {
       var _s = FoodInfo.First(t => t.FoodID.Equals(_id));
       return _s.icon;
    }
    //
    // private void OnTriggerMonster()
    // {
    //     monsterAnimator.SetTrigger("Trigger");
    // }
}
