using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AccesoRepositorio : MonoBehaviour
{
    //private string postregistroestudiante = "localhost:4000/acceso/signupestudiante";
    private string postloginestudiante = "localhost:4000/acceso/signinestudiante";
    LoginPost accesoestudiante = new LoginPost(){
          usuario = "mely13",
          password = "12345"
    };

    void Start()
    {
        /* AccesoEstudiante estudiante = new AccesoEstudiante()
         {
             nombre = "Caro",
             edad = 6,
             tutorId = "5f1e5584df68404a1c8b89f0",
             usuario = "caro6",
             password = "caro6"
         }; 
         StartCoroutine(PostCrearEstudiante(postregistroestudiante, estudiante)); */

        StartCoroutine(PostLoginEstudiante(postloginestudiante, accesoestudiante));

    }

    public IEnumerator PostCrearEstudiante(string url, AccesoEstudiante estudiante)
    {
        var jsonData = JsonUtility.ToJson(estudiante);


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
                {
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);


                    var msg = JsonUtility.FromJson<Msg>(result);

                    Debug.Log(msg.msg);
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
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

                    Debug.Log(result);
                    var msg = JsonUtility.FromJson<ResLogin>(result);

                    Debug.Log(msg.message);
                    Debug.Log(msg.user.password);

                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");

                }
            }
        }
    }
}
