using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePiece : MonoBehaviour
{
    public void OnButtonPress()
    {
        PileOuFace();
    }

    void PileOuFace()
    {
        int resultatPiece = Random.Range(0, 2);         //generation dun chiffre aleatoire entre 0 et 1

        if (resultatPiece == 1)
            Debug.Log("face");
        else
            Debug.Log("pile");
    }
}
