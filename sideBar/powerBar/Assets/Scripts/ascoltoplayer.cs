using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class ascoltoplayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }

    public Image q1;
    public Image q2;
    public Image q3;
    public Image q4;

    public Image pg1;
    public Image pg2;
    public Image pg3;
    public Image pg4;

    public Sprite sfondoq1;
    public Sprite sfondoq2;
    public Sprite sfondoq3;
    public Sprite sfondoq4;
    public Sprite sfondobase;

    public Sprite pgverdae;
    public Sprite pgrosso;
    public Sprite pgblu;
    public Sprite pggiallo;
    public Sprite pggrigio;

    public Text verdepronto;
    public Text rossopronto;
    public Text blupronto;
    public Text giallopronto;
    public Text lblpartenza;
    public static bool checkgreen = false, checkred = false, checkblue = false, checkyellow = false;
    public static bool checkgreenpronto = false, checkredpronto = false, checkbluepronto = false, checkyellowpronto = false;
    public static int giocatori = 0;
    string[] strgiocatori = new string[4];
    bool sipuogiocare = false;
    // Update is called once per frame
    void Update()
    {
        if (sipuogiocare == false)
        {

            if (!checkgreen)
            {
                if (Input.GetButtonDown("Buzz 2"))
                {
                    q1.sprite = sfondoq1;
                    pg1.sprite = pgverdae;
                    giocatori++;
                    checkgreen = true;
                    strgiocatori[0] = "verde";
                }
            }
            if (!checkgreenpronto)
            {
                if (Input.GetButtonDown("Buzz 2 pronto"))
                {
                    if (checkgreen)
                    {
                        verdepronto.text = "PRONTO";
                        checkgreenpronto = true;
                    }

                }
            }
            if (checkgreen)
            {
                if (Input.GetButtonDown("Buzz 2 annulla"))
                {
                    pg1.sprite = pggrigio;
                    q1.sprite = sfondobase;
                    giocatori--;
                    checkgreen = false;
                    checkgreenpronto = false;
                    verdepronto.text = "NON PRONTO";
                    strgiocatori[0] = "";
                }
            }

            ///////////////////////////////////////


            if (!checkred)
            {
                if (Input.GetButtonDown("Buzz 1"))
                {
                    pg2.sprite = pgrosso;
                    q2.sprite = sfondoq2;
                    giocatori++;
                    checkred = true;
                    strgiocatori[1] = "rosso";
                }
            }
            if (!checkredpronto)
            {
                if (Input.GetButtonDown("Buzz 1 pronto"))
                {
                    if (checkred)
                    {
                        rossopronto.text = "PRONTO";
                        checkredpronto = true;
                    }

                }
            }
            if (checkred)
            {
                if (Input.GetButtonDown("Buzz 1 annulla"))
                {
                    pg2.sprite = pggrigio;
                    q2.sprite = sfondobase;
                    giocatori--;
                    checkred = false;
                    checkredpronto = false;
                    rossopronto.text = "NON PRONTO";
                    strgiocatori[1] = "";
                }
            }

            ///////////////////////////////////////////////////////////////////
            if (!checkblue)
            {
                if (Input.GetButtonDown("Buzz 3"))
                {
                    pg3.sprite = pgblu;
                    q3.sprite = sfondoq3;
                    giocatori++;
                    checkblue = true;
                    strgiocatori[2] = "blu";
                }
            }
            if (!checkbluepronto)
            {
                if (Input.GetButtonDown("Buzz 3 pronto"))
                {
                    if (checkblue)
                    {
                        blupronto.text = "PRONTO";
                        checkbluepronto = true;
                    }

                }
            }
            if (checkblue)
            {
                if (Input.GetButtonDown("Buzz 3 annulla"))
                {
                    pg3.sprite = pggrigio;
                    q3.sprite = sfondobase;
                    giocatori--;
                    checkblue = false;
                    checkbluepronto = false;
                    blupronto.text = "NON PRONTO";
                    strgiocatori[2] = "";
                }
            }

            ////////////////////////////////////////////////
            if (!checkyellow)
            {
                if (Input.GetButtonDown("Buzz 4"))
                {
                    pg4.sprite = pggiallo;
                    q4.sprite = sfondoq4;
                    giocatori++;
                    checkyellow = true;
                    strgiocatori[3] = "giallo";
                }
            }
            if (!checkyellowpronto)
            {
                if (Input.GetButtonDown("Buzz 4 pronto"))
                {
                    if (checkyellow)
                    {
                        giallopronto.text = "PRONTO";
                        checkyellowpronto = true;
                    }

                }
            }
            if (checkyellow)
            {
                if (Input.GetButtonDown("Buzz 4 annulla"))
                {
                    pg4.sprite = pggrigio;
                    q4.sprite = sfondobase;
                    giocatori--;
                    checkyellow = false;
                    checkyellowpronto = false;
                    giallopronto.text = "NON PRONTO";
                    strgiocatori[3] = "";
                }
            }
            ///////////////////////////////////////////////


            if (strgiocatori[0] == "verde")
            { if (checkgreenpronto) sipuogiocare = true; else { sipuogiocare = false; return; } }
            if (strgiocatori[1] == "rosso")
            { if (checkredpronto) sipuogiocare = true; else { sipuogiocare = false; return; } }
            if (strgiocatori[2] == "blu")
            { if (checkbluepronto) sipuogiocare = true; else { sipuogiocare = false; return; } }
            if (strgiocatori[3] == "giallo")
            { if (checkyellowpronto) sipuogiocare = true; else { sipuogiocare = false; return; } }

            if (sipuogiocare)
            {
                lblpartenza.text = "PARTENZA IN 3 SECONDI";
                StartCoroutine(aspettaavvio());
                return;
            }
        }

    }
    IEnumerator aspettaavvio()
    {
        lblpartenza.text = "PARTENZA IN 3 SECONDI";
        yield return new WaitForSeconds(1);
        lblpartenza.text = "PARTENZA IN 2 SECONDI";
        yield return new WaitForSeconds(1);
        lblpartenza.text = "PARTENZA IN 1 SECONDI";
        yield return new WaitForSeconds(1);

        connetti.NewGameBtn(1);
    }

}
