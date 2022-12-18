using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject case8;

    int actualCasePose;
    public int valeurLanceDe;

    void Start()
    {
        ////stockage des positions du player 
        //new Vector3 playerPos = player.transform.position;

        ////stockage des positions des cases 
        //new Vector3 playerPos = player.transform.position;
    }

    void Update()
    {
        // Recup position joueur : player.transform.position

        player.transform.position = new Vector3(valeurLanceDe * 10 , player.transform.position.y, player.transform.position.z);

        if (player.transform.position == case8.transform.position)
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }
}
