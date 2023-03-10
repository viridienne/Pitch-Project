using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnFoodManager : MonoBehaviour
{
    public static SpawnFoodManager Instance;
    
    private SpawnFoodConfigSO config => GameManager.Instance.SpawnConfig;
    private Dictionary<int, Queue<FoodObject>> spawnDict;
    public float startX;
    private float speed;
    private float roundDelay;
    private int rowIndex;
    public float Speed => speed;
    private void Awake()
    {
        Instance = this;
    }

    private bool increased;
    public void IncreaseSpeed()
    {
        if(increased)return;
        
        // EventManager.Instance.TriggerMonster?.Invoke();
        Invoke(nameof(doubleSpeed),0.66f);
        increased = true;
    }

    private void doubleSpeed()
    {
        speed *=2;
        roundDelay /= 2f;
    }
    // Start is called before the first frame update
    void Start()
    {
        isSpawning = false;
        // speed = Random.Range(config.MinSpeed, config.MaxSpeed);
        speed = config.Speed;
        roundDelay = config.Delay;
        increased = false;
        isWin = false;
        EventManager.Instance.OnWin += onWin;
    }

    private bool isWin;
    private void onWin()
    {
        speed = 0f;
        isWin = true;
    }
    public void StartQueue(FoodInfo _foodInfo, Queue<FoodObject> _queue)
    {
        for (int i = 0; i < config.SpawnAmount; i++)
        {
            var _obj = Instantiate(config.foodPref,transform);
            _obj.SetActive(false);
            _queue.Enqueue(_obj.GetComponent<FoodObject>());
        }
    }
    public void SpawnNewObject(FoodInfo _foodInfo,Queue<FoodObject> _queue, int _queueIndex)
    {
        var _obj = _queue.Dequeue();
        int _index = Random.Range(0, 3);
        float _y = config.RowPosY[rowIndex];
        rowIndex++;
        if (rowIndex >= config.RowPosY.Length)
        {
            rowIndex = 0;
        }
        var _pos = _obj.transform.position;
        _pos.x = transform.position.x;
        _pos.y = _y;
        _obj.transform.position = _pos;
        _obj.gameObject.SetActive(true);
        _obj.SetFoodInfo(_foodInfo.FoodID,_foodInfo.FoodValue,_queueIndex,_foodInfo.icon);
    }

    public void EnqueueObj(FoodObject _obj, int _queueIndex)
    {
        if(spawnDict[_queueIndex]!=null) spawnDict[_queueIndex].Enqueue(_obj);        
    }

    public void StartSpawningObject(FoodInfo[] _foodInfo)
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
    private IEnumerator IESpawningObject(FoodInfo[] _foodInfo)
    {
        spawnDict = new Dictionary<int, Queue<FoodObject>>();
        for (int i = 0; i < _foodInfo.Length; i++)
        {
            spawnDict[i] = new Queue<FoodObject>();
            StartQueue(_foodInfo[i],spawnDict[i]);
        }

        rowIndex = 0;
        while (isSpawning)
        {
            if(isWin) yield break;
            delay += Time.deltaTime;
            if (delay >= roundDelay)
            {
                delay = 0;
                randomFood(_foodInfo);
                randomFood(_foodInfo);
                randomFood(_foodInfo);
            }
            
            yield return new WaitForEndOfFrame();
        }

    }

    private void randomFood(FoodInfo[] _foodInfo)
    {
        var index = Random.Range(0, 4);
        if(index==3) return;
        SpawnNewObject(_foodInfo[index],spawnDict[index],index);
    }
}
