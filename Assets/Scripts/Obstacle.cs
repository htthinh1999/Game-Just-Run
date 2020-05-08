using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * GameManager.Instance.Speed);
        if (transform.position.x <= ScreenBounds.Left)
        {
            SpawnManager.Instance.ReturnToPool(gameObject);
        }
    }
  
}
