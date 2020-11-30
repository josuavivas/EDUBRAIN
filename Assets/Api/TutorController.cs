using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorController : MonoBehaviour
{
    public Text idtxt;
    public Text nombretxt;
    public Text apellidotxt;
    public Text correotxt;
    public Text direcciontxt;
    public Text usuariotxt;
    public Text passwordtxt;

    public void DisplayTutorData(  string nombre, string apellido, string correo, string direccion, string usuario, string password)
    {
         
        nombretxt.text = nombre;
        apellidotxt.text = apellido;
        correotxt.text = correo;
        direcciontxt.text = direccion;
        usuariotxt.text = usuario;
        passwordtxt.text = password;
    }

  
}
