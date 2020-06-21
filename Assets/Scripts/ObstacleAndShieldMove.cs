using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleAndShieldMove : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * GameManager.Instance.Speed);
        if (transform.position.x <= ScreenBounds.Left)
        {
            if (gameObject.CompareTag("Shield"))
            {
                SpawnManager.Instance.ReturnToPool("shield", gameObject);
            }
            else
            {
                SpawnManager.Instance.ReturnToPool("obstacles", gameObject);
            }
        }
    }
}
