using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public Action<string, float> OnFoodEat;
    public Action TriggerMonster;
    public Action<string> OnMonsterAnimation;
    public Action OnWin;
    private void Awake()
    {
        Instance = this;
    }
    
}
