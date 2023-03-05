using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class FoodInfo
{
    public string FoodID;
    public float FoodValue;
    public FoodObject FoodPref;
}
[CreateAssetMenu(fileName="New Food Config",menuName = "Food Config")]
public class FoodConfigSO : ScriptableObject
{
    [TableList] public List<FoodInfo> FoodList;

    public FoodInfo GetFoodInfo(string _id)
    {
        foreach (var food in FoodList)
        {
            if (_id.Equals(food.FoodID))
            {
                return food;
            }
        }

        return null;
    }
}
