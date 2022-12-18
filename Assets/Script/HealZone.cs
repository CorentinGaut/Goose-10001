using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealZone : MonoBehaviour
{
    //lifeSystem life;
    public GameManager gameManager;
    

    void OnTriggerEnter(Collider other)
    {
        if (gameManager.life < 3)
        {
            gameManager.life++;

        }

        Debug.Log("Life:" + gameManager.life);

    }
}