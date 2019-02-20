using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ascoltoibutton : MonoBehaviour {

    public Image p1;
    public Image p2;
    public Image p3;
    public Image p4;
	// Use this for initialization
	void Start () {
     
        numerogiocatori.text = ascoltoplayer.giocatori.ToString();
        if (ascoltoplayer.checkgreen) p1.color = Color.green;
        if (ascoltoplayer.checkred) p2.color = Color.red;
        if (ascoltoplayer.checkblue) p3.color = Color.blue;
        if (ascoltoplayer.checkyellow) p4.color = Color.yellow;
    }
    public Text testo;
    public Text numerogiocatori;
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Buzz 1"))
            testo.text = "A Verde";
        if (Input.GetButtonDown("Buzz 2"))
            testo.text = "B Rosso";
        if (Input.GetButtonDown("Buzz 3"))
            testo.text = "X Blu";
        if (Input.GetButtonDown("Buzz 4"))
            testo.text = "Y Giallo";
       
    }
}
