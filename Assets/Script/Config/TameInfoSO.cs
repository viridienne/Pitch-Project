using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class TameInfo
{
    public string MonsterID;
    public string FavroriteID;
    
    public string Name;
    public string Description;
    public string Region;
    public string Favorite;
    public int TameLevel;
    public GameObject MonsterPref;
}
[CreateAssetMenu(fileName = "New Tame Info",menuName = "Tame Config")]
public class TameInfoSO : ScriptableObject
{
    [TableList] public List<TameInfo> TameInfoList;
}
