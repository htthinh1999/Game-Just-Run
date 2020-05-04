using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentMove : MonoBehaviour
{
    //public float Speed = 4f; //Ground: 4f; Clouds: 0.1f; Mountain Far: 0.2f; Mountain Near: 0.4f
    public float SpeedInverseRatio = 1; // Ground: 1f; Clouds: 40f; Mountain Far: 20f; Mountain Near: 10f

    [SerializeField] float leftRange = -38f; //Ground: -38; Clouds: -44; Mountain: -43.3f

    Vector2 originalPosistion;

    void Start()
    {
        originalPosistion = transform.position;
    }

    void Update()
    {
        if (transform.position.x >= leftRange)
        {
            transform.position = (Vector2)transform.position + Vector2.left * Time.deltaTime * GameManager.Instance.Speed / SpeedInverseRatio; 
        }
        else
        {
            transform.position = originalPosistion;
        }
    }
}
