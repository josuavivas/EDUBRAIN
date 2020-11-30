using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections; 
using UnityEngine; 
using UnityEngine.Networking;




  public  class bddislexia : MonoBehaviour
{
    player jugador ;

    Estudiante estudiante;


    private string postloginestudiante = "apidislexia.herokuapp.com/acceso/signinestudiante";



    private void Start()
    { 

    }

     public Estudiante LoginPlayer(string txtuser, string txtpassword)
     {
        

        return estudiante;

     }

    public void GuardarPlayer(player jugador)
    {


        string conn = "URI=file:" + Application.dataPath + "/Plugins/dislexia.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        DateTime fecha = DateTime.Now;
        Debug.Log("" + jugador.nombre + "','" + jugador.edad + "','" + jugador.user + "','" + jugador.password + "'");
        string sqlQuery = "INSERT INTO player (nombre,edad,usuario,password) VALUES ('" + jugador.nombre + "','" + jugador.edad + "','" +jugador.user+ "','"+jugador.password+"')";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();


        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

    }


    public List<score> consultarScore(int idplay, string tabla)
    {

        string conn = "URI=file:" + Application.dataPath + "/Plugins/dislexia.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM " + tabla + " where idplayer=" + idplay;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        List<score> lissc = new List<score>();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string score = reader.GetString(1);
            string fecha = reader.GetString(2);
            string pausa = reader.GetString(4);
            int nivel = reader.GetInt32(5);
            Debug.Log(score + "  " + fecha);

            score sc = new score(id, score, fecha, pausa, nivel);
            lissc.Add(sc);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return lissc;
    }

    public void Updatescore(string tabla, score sc)
    {
        string conn = "URI=file:" + Application.dataPath + "/Plugins/dislexia.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        int id = 0;
        if (sc == null)
        {
            string sqlQuery1 = "SELECT MAX(id) FROM " + tabla;
            dbcmd.CommandText = sqlQuery1;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }

        }
        else
        {
            id = sc.id;
        }

        Debug.Log(id);
        if (id != 0)
        {
            IDbCommand dbcmd1 = dbconn.CreateCommand();
            string sqlQuery = "UPDATE " + tabla + " SET score='" + sc.puntos + "',fecha='" + sc.fecha + "',pause='" + sc.pausa + "',nivel=" + sc.nivel + "  WHERE id=" + id;
            dbcmd1.CommandText = sqlQuery;
            dbcmd1.ExecuteNonQuery();
            dbcmd1.Dispose();
            dbcmd1 = null;

        }
        dbcmd.Dispose();
        dbcmd = null;

        dbconn.Close();
        dbconn = null;
    }

    public void GuardarScore(score score, string tabla)
    {

        string conn = "URI=file:" + Application.dataPath + "/Plugins/dislexia.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "INSERT INTO " + tabla + " (score,fecha,idplayer,pause,nivel) VALUES ('" + score.puntos + "','" + score.fecha + "'," + login.getlog().id + ",'" + score.pausa + "'," + score.nivel + ")";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

    }


    public IEnumerator PostLoginEstudiante(string url, LoginPost postestudiante)
    {
        Debug.Log("HOLA ENTRO");
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
                    var loginresult = JsonUtility.FromJson<ResLogin>(result);
                    estudiante = loginresult.user;
                    
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

