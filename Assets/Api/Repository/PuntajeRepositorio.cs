using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PuntajeRepositorio : MonoBehaviour
{
    private string geturl = "localhost:4000/puntaje/index";
    private string posturl = "localhost:4000/puntaje/create";
    private string getbyId = "localhost:4000/puntaje/findbyid";
    private string posteditUrl = "localhost:4000/puntaje/edit";


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetListarPuntaje(geturl));


        PostPuntaje postPuntaje = new PostPuntaje()
        {
            score = 10,
            nivel = 2,
            fecha = "02/03/1997",
            estado ="COMPLETO",
            juegoId = "5f1d04b27720803f443031bd",
            estudianteId = "5f127532929dcd1e14a8e039"
        };
        
         StartCoroutine(PostAgregarPuntaje(posturl, postPuntaje));


        //  StartCoroutine(GetPuntajeById(getbyId, ""));

    }

    public IEnumerator GetListarPuntaje(string url)
    {

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    result = "{\"result\":" + result + "}";
                    Debug.Log(result);
                    var puntajeLista = JsonHelper.FromJson<Puntaje>(result);

                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
     
    public IEnumerator GetByIdPuntaje(string url)
    {

    using (UnityWebRequest www = UnityWebRequest.Get(url))
    {
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.isDone)
            {
                var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                result = "{\"result\":" + result + "}";
                Debug.Log(result);
                var puntajeLista = JsonHelper.FromJson<Puntaje>(result);

                foreach (var puntaje in puntajeLista)
                {
                    Debug.Log(puntaje.estudianteId.nombre);
                }

            }
            else
            {
                //handle the problem
                Debug.Log("Error! data couldn't get.");
            }
        }
    }

}


    public IEnumerator GetPuntajeById(string url, string id)
    {
    using (UnityWebRequest www = UnityWebRequest.Get(url + '/' + id))
    {
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.isDone)
            {
                var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                Debug.Log(result);

                var puntaje = JsonUtility.FromJson<Puntaje>(result);
                Debug.Log(puntaje.score);

            }
            else
            {
                Debug.Log("Error! data couldn't get.");
            }
        }
    }

    }

 


    public IEnumerator PostAgregarPuntaje(string url, PostPuntaje postPuntaje)
    {
        //recojo datos del tutor y del juego para guardar a la bdd

        var jsonData = JsonUtility.ToJson(postPuntaje);

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


    public IEnumerator PostActualizarPuntaje(string url, PostPuntaje postPuntaje, string id)
    { 
        //recojo datos del tutor y del juego para guardar a la bdd

        var jsonData = JsonUtility.ToJson(postPuntaje);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "/" + id , jsonData))
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

}
