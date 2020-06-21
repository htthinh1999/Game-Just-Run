using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] Player player;   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.CheckDeath()== true)
        {
            playerAnimator.SetTrigger("Dead");
        }
        
    }
}
