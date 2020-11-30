using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClienteApi : MonoBehaviour
{
    private string getUrl = "localhost:4000/tutor/getbyid/5f126a3d3f572335183352e0";
    private string postUrl = "localhost:4000/tutor/postlist";


     
    public TutorController tutorController;

    void Start()
    {
        var tutor = new Tutor()
        { 
            nombre = "Selena",
            apellido ="Ramos",
            correo ="sele@espe.edu.ec",
            direccion="barrio ecuador",
            usuario="sele8",
            password="12345"

};

        StartCoroutine(Post("localhost:4000/tutor/postlist", tutor));
        //  StartCoroutine(Get(url));

    }

    public IEnumerator Get(string url)
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
                    // handle the result

                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);

                    var tutor = JsonUtility.FromJson<Tutor>(result);

                    Debug.Log(tutor.nombre);
                    tutorController.DisplayTutorData(tutor.nombre.ToString(), tutor.apellido.ToString(), tutor.correo.ToString(), tutor.direccion.ToString()
                        , tutor.usuario.ToString(), tutor.password.ToString());
                    //Debug.Log("Enemy name is " + enemy.name);
                    //Debug.Log("Enemy is strong. Health power: " + enemy.health);
                    //Debug.Log("Enemy has brute force. Attack power: " + enemy.attack);
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }

    }
    public IEnumerator Post(string url, Tutor tutor)
    {
        var jsonData = JsonUtility.ToJson(tutor);
 

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
                    result = "{\"result\":" + result + "}";
                    Debug.Log(result);
                    var resultEnemyList = JsonHelper.FromJson<Tutor>(result);

                    foreach (var item in resultEnemyList)
                    {
                        Debug.Log(item.nombre);
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
} 