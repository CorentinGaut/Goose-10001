using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZone : MonoBehaviour
{
    public GameManager gameManager;
    void OnTriggerEnter(Collider other)
    {
        gameManager.life--;
        Debug.Log("DeathZone " + gameManager.life);
    }
}