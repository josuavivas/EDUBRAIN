using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  
using UnityEngine.Networking;

public class gestion : MonoBehaviour
{
     Estudiante estudiante;
     private string postloginestudiante = "apidislexia.herokuapp.com/acceso/signinestudiante";
     private string geturl = "apidislexia.herokuapp.com/puntaje/index";

     bddislexia bd = new bddislexia();

     public InputField user, password;

    public GameObject forlogin;
    public GameObject Fondo;

    public AudioSource source;

     public AudioClip sonido1;

     //bddislexia bd = new bddislexia();


     // Start is called before the first frame update
     void Start()
     {

         if (login.getlog() != null)
        {
            forlogin.SetActive(false);
            Fondo.SetActive(true);

        }

         source.clip = sonido1;
         source.Play();

     }


     public void loginmetod()
     {
         LoginPost accesoestudiante = new LoginPost()
         {
             usuario = user.text,
             password = password.text
         };

         StartCoroutine(PostLoginEstudiante(postloginestudiante, accesoestudiante));


     }

    public void MenuEO()
     {
         SceneManager.LoadScene("MenuEO");
     }

     public void MenuJM()
     {
         SceneManager.LoadScene("MenuJM");
     }

     public void MenuPE()
     {
         SceneManager.LoadScene("MenuPE");
     }

      public void MenuLT()
      {
          SceneManager.LoadScene("MenuLT");
      }

    public void MenuCF()
    {
        SceneManager.LoadScene("IntroCF");
    }



    public void Salir()
     {
         forlogin.SetActive(true);

     }


     public IEnumerator PostLoginEstudiante(string url, LoginPost postestudiante)
     {
         var jsonData = JsonUtility.ToJson(postestudiante);



         using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
         {
             www.SetRequestHeader("content-type", "application/json");
             www.uploadHandler.contentType = "application/json";
             www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
             yield return www.SendWebRequest();

             if (www.isNetworkError)
             {
                 Debug.Log(www.error);
             }
             else
             {
                 if (www.isDone)
                 { // handle the result
                     var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                     Debug.Log(jsonData);
                     Debug.Log(result);
                     var loginresult = JsonUtility.FromJson<ResLogin>(result);
                     estudiante = loginresult.user;

                     if (estudiante != null)
                     {

                         forlogin.SetActive(false);
                         Debug.Log(estudiante.nombre);
                         LoginE.Log(estudiante);
                     }
                 }
                 else
                 {
                     Debug.Log("Error! data couldn't get.");

                 }
             }
         }
     }
}
