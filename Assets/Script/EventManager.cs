using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public Action<string, float> OnFoodEat;
    private void Awake()
    {
        Instance = this;
    }
    
}
