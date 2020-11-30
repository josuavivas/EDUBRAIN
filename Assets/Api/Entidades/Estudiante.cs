[System.Serializable]
public class Estudiante  
{
    public string _id;
    public string nombre;
    public int edad;
    public Tutor tutorId;
    public string usuario;
    public string password;
}

[System.Serializable]
public class EstudiantePuntaje
{
    public string _id;
    public string nombre;
    public int edad;
    public string tutorId;
    public string usuario;
    public string password;
}