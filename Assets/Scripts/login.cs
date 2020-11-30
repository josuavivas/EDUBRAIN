using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static  class login
    {
    private static player jugador;

    private static void Inicializar(player jug)
    {
        jugador=jug;
    }

    public static void Log(player jug)
    {
        if (jugador == null)
        {
            Inicializar(jug);
            
        }
        else {
            Debug.Log("ya esta logeado");
        }

       
    }

    public static player getlog()
    {
        if (jugador != null)
        {
            return jugador;
            
        }
        else
        {
            Debug.Log("No se ha logeado");
        }

        return null;


    }

}

