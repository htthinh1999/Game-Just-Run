using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float Speed = 2;

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * Speed);
        if (transform.position.x <= ScreenBounds.Left)
        {
            SpawnManager.Instance.ReturnToPool(gameObject);
        }
    }
}
