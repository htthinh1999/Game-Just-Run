using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator[] animators;

    void Start()
    {
        
    }

    void Update()
    {
        if (!GameManager.Instance.GameOver)
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].speed = GameManager.Instance.Speed / 10f;
            }
        }
        else
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].speed = 0;
            }
        }
    }
}
