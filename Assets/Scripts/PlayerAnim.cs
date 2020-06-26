using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] Player player;

    void Start()
    {
        
    }

    void Update()
    {
        if(player.CheckDeath())
        {
            playerAnimator.SetTrigger("Dead");
        }
        else
        {
            playerAnimator.speed = GameManager.Instance.Speed / 10f;
        }
        
    }
}
