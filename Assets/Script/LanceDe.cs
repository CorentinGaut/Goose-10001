using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceDe : MonoBehaviour
{
    public GameManager gameManager;

    public void OnButtonPress()
    {
        gameManager.resultatDe = Random.Range(1, 7);    //generation dun chiffre aleatoire entre 1 et 6
        Debug.Log(gameManager.resultatDe);              //affichage du resultat dans la console 
    }
}
