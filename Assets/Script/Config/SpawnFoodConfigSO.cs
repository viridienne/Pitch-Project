using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Config",menuName = "Spawn Config")]
public class SpawnFoodConfigSO : ScriptableObject
{    
    public GameObject foodPref;

    public float Speed;
    public float Delay;
    public int SpawnAmount;
    public float[] RowPosY;

    // public float MaxSpeed;
    // public float MinSpeed;
    // public float MinDelay;
    // public float MaxDelay;
}
