using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FoodObject : MonoBehaviour
{
    private float moveSpeed => SpawnFoodManager.Instance.Speed;
    [SerializeField]private SpriteRenderer icon;
    private int queueIndex;
    private string foodID;
    private float value;
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }


    public void SetFoodInfo(string _id, float _value, int _index, Sprite _sprite)
    {
        icon.sprite = _sprite;
        foodID = _id;
        value = _value;
        queueIndex = _index;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Wall"))
        {
            if (col.CompareTag("Player"))
            {
                EventManager.Instance.OnFoodEat?.Invoke(foodID,value);
            }
            SpawnFoodManager.Instance.EnqueueObj(this,queueIndex);
            gameObject.SetActive(false);
        }
    }
}
