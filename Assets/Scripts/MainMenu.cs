using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void etiquetar()
    {
        SceneManager.LoadScene("etiquetar");
    }


    public void principal()
    {
        SceneManager.LoadScene("Principal");
    }


    public void recomedaciones()
    {
        SceneManager.LoadScene("Recomendaciones");
    }

    // Juego Palabra Escondida
    public void PalabraEscondida()
    {
        SceneManager.LoadScene("PalabraEscondida");
    }

   

    // Juego Memoria
    public void memoria()
    {

        SceneManager.LoadScene("Main");
    }


    // Juego Lateralidad
    public void Lateralidad()
    {
        SceneManager.LoadScene("Laterabilidad");
    }

    // Juego Comletar Frase
    public void CompletarFrase()
    {
        SceneManager.LoadScene("CompletarFrase");
    }
}
