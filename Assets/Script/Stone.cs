using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stone : MonoBehaviour
{
    public Route currentRoute;

    public Button DiceButton;
    public Button PieceButton;
    public Button BossDice;

    int routePosition;
    public int valeurDe;
    public int valeurDeBoss;
    public int valeurDeCombatBoss;
    public int valeurPiece;

    bool isMoving;
    bool aLance;        //permet de savoir si le joueur a lancé le dé lors du combat de boss

    public GameObject player;
    public GameManager gameManager;

    public GameObject un;
    public GameObject deux;
    public GameObject trois;
    public GameObject quatre;
    public GameObject cinq;
    public GameObject six;

    public GameObject collecteOeufs;
    public GameObject compteurOeuf;

    public GameObject casePont;
    public GameObject caseLabyrinthe;
    public GameObject caseTeteDeMort;
    public GameObject caseMortSubite;
    public GameObject casePuit;

    public GameObject ecranAccueil;
    public GameObject ecranVictoire;
    public GameObject ecranGameOver;
   
    public Text nbOeuf;
    public Text chiffreBoss;
    public Text textVictoire;



    void Start()
    {
        ecranAccueil.SetActive(true);
        System.Threading.Thread.Sleep(3000);
        ecranAccueil.SetActive(false);
    }

    void Update()
    {

        DiceButton.onClick.AddListener(() => {

            if (!isMoving)
            {
                valeurDe = Random.Range(1, 7);          //a decommenter a la fin des tests
                //valeurDe = 7;
                Debug.Log("Dice Rolled " + valeurDe);

                // si la position du joueur plus la valeur du dé est inférieur au nombre total de cases : on avance
                if (routePosition + valeurDe < currentRoute.childNodeList.Count)
                {
                    StartCoroutine(MoveForward());
                } else
                {
                    Debug.Log("Tu as fait un chiffre trop élevé, relance le dé !");
                }
            }
        });        

        //Afficher le nombre d'oeufs collectés
        nbOeuf.text = gameManager.oeuf.ToString();
        //Afficher chiffre boss
        chiffreBoss.text = valeurDeBoss.ToString();
    }

    IEnumerator MoveForward()
    {
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;

        //Affichage du score du dé à chaque lancé 
        if (valeurDe == 1)
        {
            un.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            un.SetActive(false);
        }
        if (valeurDe == 2)
        {
            deux.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            deux.SetActive(false);
        }
        if (valeurDe == 3)
        {
            trois.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            trois.SetActive(false);
        }
        if (valeurDe == 4)
        {
            quatre.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            quatre.SetActive(false);
        }
        if (valeurDe == 5)
        {
            cinq.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            cinq.SetActive(false);
        }
        if (valeurDe == 6)
        {
            six.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            six.SetActive(false);
        }

        //le joueur avance
        while (valeurDe > 0)
        {
            Vector3 nextPos = currentRoute.childNodeList[routePosition + 1].position;                 //-1 pour aller en arriere
            while (MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            valeurDe--;
            routePosition++;

            if (routePosition + valeurDe == currentRoute.childNodeList.Count)
            {
                Debug.Log("Victoire !!");

                textVictoire.gameObject.SetActive(false);
                ecranVictoire.SetActive(true);
            }

            TourneDansLesAnglesEnAvant();
        }

        isMoving = false;


        //Cases Oie ---------------------------------------------------------------------------------------------------------------------------------------------------------
        if ((currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[5].position) 
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[9].position) 
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[14].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[18].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[23].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[27].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[32].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[36].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[41].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[45].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[50].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[54].position)
            || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[59].position))
        {
            Debug.Log("Case Oie. Tu as gagné un oeuf !");
            gameManager.oeuf++;
            Debug.Log("Tu as collecté " + gameManager.oeuf + " oeuf !");
            collecteOeufs.SetActive(true);
            yield return new WaitForSeconds(2);
            collecteOeufs.SetActive(false);
        }

        //Case Pont ---------------------------------------------------------------------------------------------------------------------------------------------------------
        if (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[6].position)
        {
            Debug.Log("Case Pont. Tu avances jusqu'à la case 12 !");

            casePont.SetActive(true);
            yield return new WaitForSeconds(2);
            casePont.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            int goCase12 = 6;
            if (isMoving)
            {
                yield break;
            }
            isMoving = true;

            while (goCase12 > 0)
            {
                Vector3 nextPos = currentRoute.childNodeList[routePosition + 1].position;
                while (MoveToNextNode(nextPos)) { yield return null; }

                yield return new WaitForSeconds(0.1f);
                goCase12--;
                routePosition++;

                TourneDansLesAnglesEnAvant();
            }
            isMoving = false;
        }

        //Case Mort Subite --------------------------------------------------------------------------------------------------------------------------------------------------
        if (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[61].position)
        {
            Debug.Log("Case Mort subite. Combat de Boss !");

            caseMortSubite.SetActive(true);
            PieceButton.gameObject.SetActive(true);

            PieceButton.onClick.AddListener(() => {
                Debug.Log("ici");

                valeurPiece = Random.Range(0, 2);
                if (valeurPiece == 1)
                {
                    Debug.Log("Bravo ! Tu as fait face, c'est la Victoire pour toi !");
                    compteurOeuf.SetActive(false);
                    DiceButton.gameObject.SetActive(false);
                    caseMortSubite.SetActive(false);
                    ecranVictoire.SetActive(true);
                }
                else
                {
                    Debug.Log("Oh non.. Tu as fait pile, quel dommage, l'aventure s'arrête ici pour toi, c'est GameOver !");
                    compteurOeuf.SetActive(false);
                    DiceButton.gameObject.SetActive(false);
                    caseMortSubite.SetActive(false);
                    ecranGameOver.SetActive(true);
                }
            });
        }

        //Case Puit Prison --------------------------------------------------------------------------------------------------------------------------------------------------
        if ((currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[31].position)
            || currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[52].position)
        {
            CasePuit();
        }

        //Case Labyrinthe ---------------------------------------------------------------------------------------------------------------------------------------------------
        if (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[42].position)
        {
            Debug.Log("Case Labyrinthe. Tu retournes jusqu'à la case 30 !");

            caseLabyrinthe.SetActive(true);
            yield return new WaitForSeconds(2);
            caseLabyrinthe.SetActive(false);

            yield return new WaitForSeconds(0.25f);
            int goCase30 = 12;
            if (isMoving)
            {
                yield break;
            }
            isMoving = true;

            while (goCase30 > 0)
            {
                Vector3 nextPos = currentRoute.childNodeList[routePosition - 1].position;
                while (MoveToNextNode(nextPos)) { yield return null; }

                yield return new WaitForSeconds(0.05f);
                goCase30--;
                routePosition--;

                TourneDansLesAnglesEnArriere();
            }
            isMoving = false;
        }

        //Case Tete de Mort -------------------------------------------------------------------------------------------------------------------------------------------------
        if (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[57].position)
        {
            Debug.Log("Case Tete de Mort. Tu retournes au départ !");

            caseTeteDeMort.SetActive(true);
            yield return new WaitForSeconds(2);
            caseTeteDeMort.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            int goCaseDepart = 57;
            if (isMoving)
            {
                yield break;
            }
            isMoving = true;

            while (goCaseDepart > 0)
            {
                Vector3 nextPos = currentRoute.childNodeList[routePosition - 1].position;
                while (MoveToNextNode(nextPos)) { yield return null; }

                yield return new WaitForSeconds(0.0001f);
                goCaseDepart--;
                routePosition--;

                TourneDansLesAnglesEnArriere();
            }
            isMoving = false;           
        }        

        //Case Victoire -----------------------------------------------------------------------------------------------------------------------------------------------------
        if (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[62].position)
        {
            Debug.Log("Victoire !");

            textVictoire.gameObject.SetActive(false);
            ecranVictoire.SetActive(true);
        }

    }

    void CasePuit()
    {
        Debug.Log("Case Puit. Combat de Boss !");
        aLance = false;

        casePuit.SetActive(true);
        BossDice.gameObject.SetActive(true);
        DiceButton.gameObject.SetActive(false);
        valeurDeBoss = Random.Range(1, 7);

        BossDice.onClick.AddListener(() => {
            valeurDeCombatBoss = Random.Range(1, 7);
            Debug.Log(" le joueur a fait " + valeurDeCombatBoss + " au lance de dé");
            aLance = true;

            if (aLance == true)
            {
                casePuit.SetActive(false);
                BossDice.gameObject.SetActive(false);
                aLance = false;
                DiceButton.gameObject.SetActive(true);
            }

            if (valeurDeBoss > valeurDeCombatBoss)
            {
                Debug.Log(" le joueur a perdu");
                StartCoroutine(MoveBackwardAfterBoss());
            }
            else
            {
                Debug.Log(" le joueur a gagné");
                StartCoroutine(MoveForwardAfterBoss());
            }

        });
        isMoving = false;
    }

    IEnumerator MoveForwardAfterBoss()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        //le joueur avance
        while (valeurDeCombatBoss > 0)
        {
            Vector3 nextPos = currentRoute.childNodeList[routePosition + 1].position;                 //-1 pour aller en arriere
            while (MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            valeurDeCombatBoss--;
            routePosition++;

            if (routePosition + valeurDeCombatBoss == currentRoute.childNodeList.Count)
            {
                Debug.Log("Victoire !!");

                textVictoire.gameObject.SetActive(false);
                ecranVictoire.SetActive(true);
            }

            TourneDansLesAnglesEnAvant();
        }

        isMoving = false;
    }

    IEnumerator MoveBackwardAfterBoss()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        //le joueur avance
        while (valeurDeCombatBoss > 0)
        {
            Vector3 nextPos = currentRoute.childNodeList[routePosition - 1].position;                 //-1 pour aller en arriere
            while (MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            valeurDeCombatBoss--;
            routePosition--;

            TourneDansLesAnglesEnArriere();
        }

        isMoving = false;
    }

    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }

    void TourneDansLesAnglesEnAvant()
    {
        if ((currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[8].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[14].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[22].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[27].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[34].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[38].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[44].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[47].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[52].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[54].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[58].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[59].position)
                )
        {
            player.transform.Rotate(0, 90, 0);
        }
    }

    void TourneDansLesAnglesEnArriere()
    {
        if ((currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[8].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[14].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[22].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[27].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[34].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[38].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[44].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[47].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[52].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[54].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[58].position)
                || (currentRoute.childNodeList[routePosition].position == currentRoute.childNodeList[59].position)
                )
        {
            player.transform.Rotate(0, -90, 0);
        }
    }
}
