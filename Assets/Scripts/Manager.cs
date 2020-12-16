using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Manager : MonoBehaviour
{
    private string getPuntajeByGameUrl = "apidislexia.herokuapp.com/puntaje/findbygame";
    private string postPuntajeEditUrl = "apidislexia.herokuapp.com/puntaje/edit";
    private string getJuegoByNameUrl = "apidislexia.herokuapp.com/juego/findbyname";
    private string postJuegoCreateUrl = "apidislexia.herokuapp.com/puntaje/create";
    private string getPuntajeListaUrl = "apidislexia.herokuapp.com/puntaje/index";


    private string idjuego = "";
    private List<Puntaje> listaPuntaje;

    public Text puntos, fecha;
    public GameObject puntostab;
    bddislexia db = new bddislexia();
    public GameObject lettreOne, lettreTwo,
                      BoxOne, BoxTwo, distrac1, distrac2;
    public GameObject winText, btnsiguinte, btnir;

    public Text intentosj, totintentos;

    public GameObject intro;
    public GameObject mensaje, inicio;
    score sc = null;
    //clase puntaje para la bdd
    Puntaje sc2 = null;
    bool activa;
    public Canvas canvas;



    private string[] nombresPalabra1 = { "Abeja", "Araña", "Ardilla", "Burro", "Cabra", "Canguro", "Cebra", "Cocodrilo", "Conejo", "Gallina", "Gorila" };
    private string[] nombresPalabra2 = { "Hormiga", "Jirafa", "Leon", "Mariposa", "Oso", "Oveja", "Pajaro", "Paloma", "Perro", "Pez", "Rana" };
    private string[] nombresImagen1 = { "Abeja", "Araña", "Ardilla", "Burro", "Cabra", "Canguro", "Cebra", "Cocodrilo", "Conejo", "Gallina", "Gorila" };
    private string[] nombresImagen2 = { "Hormiga", "Jirafa", "Leon", "Mariposa", "Oso", "Oveja", "Pajaro", "Paloma", "Perro", "Pez", "Rana" };


    private string[] nombresPalabraDis = { "Cerdo", "Mono", "Lombriz", "Camello", "Perro", "Sapo", "Puma", "Vaca", "Gato","Elefante" };


    Vector3 lettreOneIni, lettreTwoIni, distrac1Ini, distrac2Ini;

    string str = "";
    public string word;

    private Image palabra1;
    private Image palabra2;

    private Image distracIma1;
    private Image distracIma2;

    public Image imag1;
    public Image imag2;
    public string errores;

    bool oneCorrect=false, twoCorrect = false;

    Vector3 iniScaleLettreOne, iniScaleLettreTwo;

     public AudioSource source;
    public AudioClip correct;
     public AudioClip incorrect;
     public AudioClip gana;

     public AudioClip[] sonido1;
     public AudioClip[] sonido2;

    int cont = 0;
    int intentos=0;

    string txtinten = "INTENTOS:";
    System.Random rnd = new System.Random();
     
    void Start()
    {
        StartCoroutine(GetJuegoByName(getJuegoByNameUrl, "Silabas"));
        
        /*   List<score> lscore = db.consultarScore(login.getlog().id, "etiquetar");
        
         errores = ""+lscore.Count();
         if (lscore.Count() > 0){
             if (lscore[lscore.Count() - 1].pausa == "PAUSA")
             {
                 sc = lscore[lscore.Count() - 1];
                 cont = sc.nivel;
                 intentos = int.Parse(sc.puntos);

                 intentosj.text=txtinten+intentos;
             }
      
    }  */

        puntostab.SetActive(false);

        palabra1 = lettreOne.GetComponent<Image>();
        palabra1.sprite = Resources.Load<Sprite>("IMAGENES/palabras/"+nombresPalabra1[cont]);

        palabra2 = lettreTwo.GetComponent<Image>();
        palabra2.sprite = Resources.Load<Sprite>("IMAGENES/palabras/" + nombresPalabra2[cont]);
      
        distracIma1 = distrac1.GetComponent<Image>();
        distracIma1.sprite = Resources.Load<Sprite>("IMAGENES/distractor/" + nombresPalabraDis[rnd.Next(0,5)]);

        distracIma2 = distrac2.GetComponent<Image>();
        distracIma2.sprite = Resources.Load<Sprite>("IMAGENES/distractor/" + nombresPalabraDis[rnd.Next(5,10)]);

        imag1.sprite=Resources.Load<Sprite>("IMAGENES/animales/" + nombresPalabra1[cont]);
        imag2.sprite = Resources.Load<Sprite>("IMAGENES/animales/" + nombresPalabra2[cont]);

        winText.SetActive(false);

        btnsiguinte.SetActive(true);
    
        lettreOneIni = lettreOne.transform.position;
        lettreTwoIni = lettreTwo.transform.position;
        distrac1Ini = distracIma1.transform.position;
        distrac2Ini = distracIma2.transform.position;
  
        iniScaleLettreOne = lettreOne.transform.localScale;
        iniScaleLettreTwo = lettreTwo.transform.localScale;
    }



    public void DragLettreOne()
    {
        lettreOne.transform.position = Input.mousePosition;
    }
    public void DragLettreTwo()
    {
        lettreTwo.transform.position = Input.mousePosition;
    }


    public void Dragdistractor1()
    {
        distrac1.transform.position = Input.mousePosition;  
    }


    public void Dragdistractor2()
    {
        distrac2.transform.position = Input.mousePosition;
    }


    public void DropDistrac1()
    {

        intentos++;
        
        intentosj.text=txtinten + intentos;
        distrac1.transform.position = distrac1Ini;
        source.clip = incorrect;
       
    }

    public void DropDistrac2()
    {

        intentos++;

        intentosj.text = txtinten + intentos;
        distrac2.transform.position = distrac2Ini;
        source.clip = incorrect;
       

    }
    public void DropLettreOne()
    {
        float Distance = Vector3.Distance(lettreOne.transform.position, BoxOne.transform.position);

        intentos++;

        intentosj.text = txtinten + intentos;
        if (Distance < 40 && oneCorrect == false)
        {
            lettreOne.transform.localScale = BoxOne.transform.localScale;
            lettreOne.transform.position = BoxOne.transform.position;
            oneCorrect = true;
            BoxOne.name = lettreOne.name;
            source.clip = sonido1[cont];
                 
        }
      
        else
        {
            lettreOne.transform.position = lettreOneIni;
            source.clip = incorrect;
            
        }

    }


    public void DropLettreTwo()
    {
        
        float Distance2 = Vector3.Distance(lettreTwo.transform.position, BoxTwo.transform.position);

        intentos++;

        intentosj.text = txtinten + intentos;
        if (Distance2 < 50 && twoCorrect == false)
        {
            lettreTwo.transform.localScale = BoxTwo.transform.localScale;
            lettreTwo.transform.position = BoxTwo.transform.position;
            twoCorrect = true;
            BoxTwo.name = lettreTwo.name;
            source.clip = sonido2[cont];
            

           
        }
        else
        {
            lettreTwo.transform.position = lettreTwoIni;
            source.clip = incorrect;
           
        }

    }

 
    public void Reload()
    {
        cont++;
        winText.SetActive(false);
        btnsiguinte.SetActive(false);
        palabra1.sprite = Resources.Load<Sprite>("IMAGENES/palabras/" + nombresPalabra1[cont]);

        palabra2.sprite = Resources.Load<Sprite>("IMAGENES/palabras/" + nombresPalabra2[cont]);

        imag1.sprite = Resources.Load<Sprite>("IMAGENES/animales/" + nombresPalabra1[cont]);
        imag2.sprite = Resources.Load<Sprite>("IMAGENES/animales/" + nombresPalabra2[cont]);


        distracIma1.sprite = Resources.Load<Sprite>("IMAGENES/distractor/" + nombresPalabraDis[rnd.Next(0, 5)]);

      

        distracIma2.sprite = Resources.Load<Sprite>("IMAGENES/distractor/" + nombresPalabraDis[rnd.Next(5, 10)]);
        str = "";


       oneCorrect = false;
       twoCorrect = false;
      

       BoxOne.name = "1";
       BoxTwo.name = "2";
       


        lettreOne.transform.position = lettreOneIni;
        lettreTwo.transform.position = lettreTwoIni;


        lettreOne.transform.localScale = iniScaleLettreOne;
        lettreTwo.transform.localScale = iniScaleLettreTwo;

    }


    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            activa = !activa;
            canvas.enabled = activa;
            Time.timeScale = (activa) ? 0 : 1f;
        }

        if (oneCorrect==true && twoCorrect==true) {
   
            if (cont>=10) {

                totintentos.text = "SUS INTENTOS FUERON:"+intentos;
                winText.SetActive(true);
                
            }
               
                btnsiguinte.SetActive(true);
            
        }
    }


   public void btnbnext() {

        if (cont == 10) {
            source.clip = gana;
            
        }

        if (cont >= 10)
        {
            if (sc2 != null)
            {
                sc2.estado = "COMPLETADO";
                sc2.nivel = cont;
                sc2.score= intentos;

                Debug.Log(sc2.estudianteId._id);
                Debug.Log(sc2.juegoId._id);

                PostPuntaje postPuntaje = new PostPuntaje()
                {
                    score = sc2.score,
                    estado = sc2.estado,
                    nivel = sc2.nivel,
                    fecha = sc2.fecha,
                    estudianteId = sc2.estudianteId._id,
                    juegoId = sc2.juegoId._id
                };


                //db.Updatescore("etiquetar",sc);
                StartCoroutine(PostActualizarPuntaje(postPuntajeEditUrl, postPuntaje, sc2._id));
            }
            else
            {

               sc = new score(0, "" + intentos,""+ DateTime.Now, "completado", cont);

                PostPuntaje postPuntaje = new PostPuntaje()
                {
                    score = intentos,
                    nivel = 10,
                    fecha = DateTime.Now.ToString(),
                    estado = "completado",
                    estudianteId = LoginE.getlog()._id,
                    juegoId = idjuego
                    
               };

                //db.GuardarScore(sc, "etiquetar");

                StartCoroutine(PostAgregarPuntaje(postJuegoCreateUrl, postPuntaje));
            }
            SceneManager.LoadScene("Principal");
        }
        else {
            Reload();

        }

    }

    public void verscore()
    {
        //linea 468 consulto el puntaje del juego etiquetar
        // List<score> lscore = db.consultarScore(login.getlog().id, "etiquetar");
        StartCoroutine(GetByGameListaPuntaje(getPuntajeByGameUrl));


        puntostab.SetActive(true);
    }


    public void verPuntajeForeach(List<Puntaje> listaPuntaje )
    {
        string pun = "", fech = "";

        foreach (Puntaje puntaje in listaPuntaje)
        {
            int valor = puntaje.score;

            valor = 122 - valor;
            if (valor < 0)
            {
                valor = 0;
            }

            if (valor > 100)
            {
                pun += "***% \n";
            }
            else
            {
                pun += valor + " % \n";
            }

            fech += puntaje.fecha + "\n";
        }
        fecha.text = fech;
        puntos.text = pun;

    }

    public void salircore()
    {
        puntostab.SetActive(false);

    }

    public void ir()
    {
        mensaje.SetActive(false); 
        inicio.SetActive(false);
    }

    public void Salir()
    {
        SceneManager.LoadScene("Principal");
    }


    /* public void salirPausa()
     {

         if (sc != null)
         {
             sc.pausa = "PAUSA";
             sc.nivel = cont;
             sc.puntos = "" + intentos;
             db.Updatescore("etiquetar",sc);
         }
         else
         {
             sc = new score(0, "" + intentos, "" + DateTime.Now, "PAUSA", cont);
             linea 530 guarda el puntaje del juego etiquetar
             db.GuardarScore(sc, "etiquetar");
         }
         SceneManager.LoadScene("Principal");
     }*/


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



    //LISTAR PUNTAJE CREADA POR MI

    public IEnumerator GetByGameListaPuntaje(string getPuntajeByGameListaUrl)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(getPuntajeByGameListaUrl + "/" + idjuego))
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
                    listaPuntaje = puntajeLista;
                    verPuntajeForeach(listaPuntaje);
                    errores = "" + puntajeLista.Count();

                    if (puntajeLista.Count() > 0)
                    {
                        if (puntajeLista[puntajeLista.Count() - 1].estado == "PAUSA")
                        {
                            sc2 = puntajeLista[puntajeLista.Count() - 1];
                            cont = sc2.nivel;

                            intentos = sc2.score;

                            intentosj.text = txtinten + intentos;
                        }

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



    public IEnumerator GetJuegoByName(string getJuegoByNameUrl, string name)
    {

        using (UnityWebRequest www = UnityWebRequest.Get(getJuegoByNameUrl + '/' + name))
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
                    idjuego = juego._id;
                    StartCoroutine(GetByGameListaPuntaje(getPuntajeByGameUrl));

                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }

    }


    public IEnumerator PostActualizarPuntaje(string postPuntajeActualizarUrl, PostPuntaje postPuntaje, string id)
    {
        //recojo datos del tutor y del juego para guardar a la bdd

        var jsonData = JsonUtility.ToJson(postPuntaje);

        using (UnityWebRequest www = UnityWebRequest.Post(postPuntajeActualizarUrl + "/" + id, jsonData))
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
