using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using System;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Networking;

public class InterfazUsuario : MonoBehaviour
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
    public GameObject menu, inicio;
    public GameObject menuGanador;
	public Text textoMenuGanador;
	public Slider sliderDif;
	public Text textoDificultad;
    public GameObject btnsalir;
    bddislexia db = new bddislexia();
    score sc = null;
    bool activa;
    public Canvas canvas;
    public bool menuMostrado;
	public bool menuMostradoGanador;
    public int dificultad = -1, cont = 1;

    string txtinten = "INTENTOS:";

    public int SegundosCronometro;
	public Text cronometro;
	private TimeSpan tiempo;
    private int segundosTot=0;

    Puntaje sc2 = null;
    int aux = 0;
    int intentos = 0;
    public string errores;
    public Text intentosj, totintentos;
    public AudioSource source;
    public AudioClip correct;
    public AudioClip incorrect;
    public AudioClip gana;

    void Start()
    {

        // List<score> lscore = db.consultarScore(login.getlog().id, "parejas");
        aux++;
        StartCoroutine(GetJuegoByName(getJuegoByNameUrl, "Memoria"));



       /* if (lscore.Count() > 0)
        {
            if (lscore[lscore.Count() - 1].pausa == "PAUSA")
            {



                sc = lscore[lscore.Count() - 1];

                string[] values = sc.puntos.Split(':');

                TimeSpan ts = new TimeSpan(int.Parse(values[0]),int.Parse( values[1]),int.Parse(values[2]));
                segundosTot =(int) ts.TotalSeconds;
                dificultad = sc.nivel-2;
                 


            }

        }*/
        puntostab.SetActive(false);
        btnsalir.SetActive(false);
        loaded();
	}

	public void MostrarMenu(){
		menu.SetActive (true);
		menuMostrado = true;
	}

	public void EsconderMenu(){
		menu.SetActive (false);
		menuMostrado = false;
	}

	public void MostrarMenuGanador(){
       
        if (cont >= 10)
        {
            if (sc2 != null)
            {

                sc2.estado = "COMPLETADO";
                sc2.nivel = aux;
                sc2.score = intentos;

                //db.Updatescore("Laterabilidad", sc);
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

                StartCoroutine(PostActualizarPuntaje(postPuntajeEditUrl, postPuntaje, sc2._id));
            }
            else
            {

                sc = new score(0, "" + intentos, "" + DateTime.Now, "completado", cont);

                PostPuntaje postPuntaje = new PostPuntaje()
                {
                    score = intentos,
                    nivel = 1,
                    fecha = DateTime.Now.ToString(),
                    estado = "completado",
                    estudianteId = LoginE.getlog()._id,
                    juegoId = idjuego

                };

                StartCoroutine(PostAgregarPuntaje(postJuegoCreateUrl, postPuntaje));
                // db.GuardarScore(sc, "Laterabilidad");
            }
            SceneManager.LoadScene("Principal");
        }
        
        menuGanador.SetActive (true);
		menuMostradoGanador = true;
		textoMenuGanador.text = "HAS GANADO!" + '\n' + "Has encontrado todas las parejas en " + '\n' + tiempo;
        segundosTot += SegundosCronometro;
    }

	public void EsconderMenuGanador(){
		menuGanador.SetActive (false);
		menuMostradoGanador = false;
	}


	public void CambiarDificultad()
    {
        dificultad = dificultad + cont;
    }


	public void ActivarCronometro(){
		ActualizarCronometro ();
	}

	public void ReiniciarCronometro(){
        

		SegundosCronometro = 0;
		CancelInvoke ("ActualizarCronometro");

	}

	public void PausarCronometro(){

	}

	public void ActualizarCronometro(){
		SegundosCronometro++;
		tiempo = new TimeSpan(0,0,  SegundosCronometro);
		cronometro.text = tiempo.ToString();
		Invoke ("ActualizarCronometro", 1.0f);
	}
   
    public void ir()
    {
        btnsalir.SetActive(false);
        inicio.SetActive(false);
    }
    public void loaded()
    {
        if (dificultad < 3)
        {

            CambiarDificultad();

            Debug.Log("disf antes"+dificultad);

            if (dificultad == 2)
            {
                if (sc==null) {
                    TimeSpan temp = new TimeSpan(0, 0, segundosTot);

                    Debug.Log("priemra" + dificultad);
                    sc = new score(0, "" + temp, "" + DateTime.Now, "PAUSA", dificultad);
                    db.GuardarScore(sc, "parejas");
                }
            }
            else if (dificultad > 2)
            {
                Debug.Log("segunda" + dificultad);
                TimeSpan temp = new TimeSpan(0, 0, segundosTot);
                sc.puntos = "" + temp;
                sc.nivel = dificultad;
                sc.pausa = "PAUSA";
                db.Updatescore("parejas", sc);
            }
        }
        else
        {
            Debug.Log("tercera" + dificultad);
            TimeSpan temp = new TimeSpan(0, 0, segundosTot);
            sc.puntos = "" + temp;
            sc.nivel = dificultad;
            sc.pausa = "COMPLETO";
            db.Updatescore( "parejas",sc);

            btnsalir.SetActive(true);
        }
    }

    public void verscore()
    {
        bddislexia db = new bddislexia();
        /* List<score> lscore = db.consultarScore(login.getlog().id, "parejas");
         string pun = "", fech = "";
         puntostab.SetActive(true);

         foreach (score item in lscore)
         {
             pun += item.puntos + "\n";

             fech += item.fecha + "\n";


         }

         fecha.text = fech;
         puntos.text = pun;]*/

        StartCoroutine(GetByGameListaPuntaje(getPuntajeByGameUrl));
        puntostab.SetActive(true);




    }

    public void salircore()
    {
        puntostab.SetActive(false);

    }

    public void Salir()
    {
        SceneManager.LoadScene("Principal");
        
    }

    public void salirPausa()
    {
        SceneManager.LoadScene("Principal");
    }

    public void salirescena()
    {
        SceneManager.LoadScene("Principal");
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            activa = !activa;
            canvas.enabled = activa;
            Time.timeScale = (activa) ? 0 : 1f;



        }
    }

    public void verPuntajeForeach(List<Puntaje> listaPuntaje)
    {
        string pun = "", fech = "";

        foreach (Puntaje puntaje in listaPuntaje)
        {
            int valor = puntaje.score;

            valor = 110 - valor;
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
                            aux = sc2.nivel;

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
