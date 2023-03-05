using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Config",menuName = "Spawn Config")]
public class SpawnFoodConfigSO : ScriptableObject
{
    public float DefaultSpeed;
    public int SpawnAmount;
    public float MaxY;
    public float MinY;
    public float MaxSpeed;
    public float MinSpeed;
    public float SpawnDelay;
}
