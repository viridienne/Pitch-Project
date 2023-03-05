using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnFoodManager : MonoBehaviour
{
    public static SpawnFoodManager Instance;

    private SpawnFoodConfigSO config => GameManager.Instance.SpawnConfig;
    private Queue<FoodObject> spawnList;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnList = new Queue<FoodObject>();
        isSpawning = false;
    }

    public void StartQueue(FoodInfo _foodInfo)
    {
        for (int i = 0; i < config.SpawnAmount; i++)
        {
            var _obj = Instantiate(_foodInfo.FoodPref.gameObject,transform);
            _obj.SetActive(false);
            spawnList.Enqueue(_obj.GetComponent<FoodObject>());
        }
    }
    public void SpawnNewObject(FoodInfo _foodInfo)
    {
        if (spawnList.Count <= 0)
        {
            StartQueue(_foodInfo);
            return;
        }
        var _obj = spawnList.Dequeue();
        float _y = Random.Range(config.MinY, config.MaxY);
        var _pos = _obj.transform.position;
        _pos.y = _y;
        _obj.transform.position = _pos;
        _obj.gameObject.SetActive(true);
        _obj.SetSpeed(Random.Range(config.MinSpeed,config.MaxSpeed));
        _obj.SetFoodInfo(_foodInfo.FoodID,_foodInfo.FoodValue);
    }

    public void EnqueueObj(FoodObject _obj)
    {
        if(spawnList!=null) spawnList.Enqueue(_obj);        
    }

    public void StartSpawningObject(FoodInfo _foodInfo)
    {
        isSpawning = true;
        StartCoroutine(IESpawningObject(_foodInfo));
    }

    public void StopSpawningObject()
    {
        isSpawning = false;
    }

    private bool isSpawning;
    private float delay;
        
    private IEnumerator IESpawningObject(FoodInfo _foodInfo)
    {
        StartQueue(_foodInfo);
        while (isSpawning)
        {
            delay += Time.deltaTime;
            if (delay >= config.SpawnDelay)
            {
                delay = 0;
                SpawnNewObject(_foodInfo);
            }

            yield return new WaitForEndOfFrame();
        }

    }
}
