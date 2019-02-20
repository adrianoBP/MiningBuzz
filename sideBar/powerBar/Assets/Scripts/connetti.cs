using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class connetti : MonoBehaviour {

    public static void NewGameBtn(int num)
    {
        if (num == 0)
        {
            ascoltoplayer.giocatori = 0;
            ascoltoplayer.checkgreen = false;
            ascoltoplayer.checkblue = false;
            ascoltoplayer.checkyellow = false;
            ascoltoplayer.checkred = false;
        }
       
        SceneManager.LoadScene(num);
    }
}
