[System.Serializable]
public class AccesoEstudiante
{
    public string _id;
    public string nombre;
    public int edad;
    public string tutorId;
    public string usuario;
    public string password;
}
 
public class LoginPost
{ 
    public string usuario;
    public string password; 
}


public class ResLogin
{
    public string message;
    public Estudiante user; 
}