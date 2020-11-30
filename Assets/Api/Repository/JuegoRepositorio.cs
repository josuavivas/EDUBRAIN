using System.Collections; 
using UnityEngine;
using UnityEngine.Networking;

public class JuegoRepositorio : MonoBehaviour
{
    private string getUrl = "localhost:4000/juego/index";
    private string postUrl = "localhost:4000/juego/create";
    private string postactualizarJuego = "localhost:4000/juego/edit";
    private string getbyname= "localhost:4000/juego/findbyname";

    Juego juego = new Juego();

    void Start()
    {

        //StartCoroutine(GetJuego(getUrl));

        /*  Juego juego = new Juego()
          {
              nombre = "Silabas", 
              niveles = 6
          };
          StartCoroutine(PostCrearJuego(postUrl, juego));
          StartCoroutine(PostActualizarJuego(postactualizarJuego, juego, "5f1d04b27720803f443031bd")); */
        StartCoroutine(GetJuegoByName(getbyname, "Silabas"));

    }

    public IEnumerator GetJuego(string url)
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
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    result = "{\"result\":" + result + "}";
                    Debug.Log(result);
                    var resultEnemyList = JsonHelper.FromJson<Juego>(result);

                    foreach (var juego in resultEnemyList)
                    {
                        Debug.Log(juego.nombre);
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


    public IEnumerator GetJuegoByName(string url, string name)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + '/' + name))
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

                    var juego = JsonUtility.FromJson<Juego>(result);
                    Debug.Log(juego.nombre);
                     

                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }

    }

    public IEnumerator PostCrearJuego(string url, Juego juego)
    {
        var jsonData = JsonUtility.ToJson(juego);


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

    public IEnumerator PostActualizarJuego(string url, Juego juego, string id)
    {
        var jsonData = JsonUtility.ToJson(juego);


        using (UnityWebRequest www = UnityWebRequest.Post(url + "/" + id, jsonData))
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
