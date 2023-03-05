using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FoodObject : MonoBehaviour
{
    private float moveSpeed;

    private string foodID;
    private float value;
    private void Awake()
    {
        moveSpeed = GameManager.Instance.SpawnConfig.DefaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void SetSpeed(float _value)
    {
        if (_value > 0)
        {
            moveSpeed = _value;
        }
    }

    public void SetFoodInfo(string _id, float _value)
    {
        foodID = _id;
        value = _value;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Wall"))
        {
            SpawnFoodManager.Instance.EnqueueObj(this);
            gameObject.SetActive(false);
            if (col.CompareTag("Player"))
            {
               EventManager.Instance.OnFoodEat?.Invoke(foodID,value);
            }
        }
    }
}
