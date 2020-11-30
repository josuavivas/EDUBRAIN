using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginE : MonoBehaviour
{ 
 
    private static Estudiante estudiante;

    private static void Inicializar(Estudiante jug)
    {
        estudiante = jug;
    }

    public static void Log(Estudiante jug)
    {
        if (estudiante == null)
        {
            Inicializar(jug);

        }
        else
        {
            Debug.Log("ya esta logeado");
        }


    }

    public static Estudiante getlog()
    {
        if (estudiante != null)
        {
            return estudiante;

        }
        else
        {
            Debug.Log("No se ha logeado");
        }

        return null;


    }

}

