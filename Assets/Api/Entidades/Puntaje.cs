[System.Serializable]
public class Puntaje
{
    public string _id;
    public int score;
    public int nivel;
    public string estado;
    public string fecha;
    public Juego juegoId;
    public EstudiantePuntaje estudianteId;

}
public class PostPuntaje
{
    public int nivel;
    public int score;
    public string fecha;
    public string estado;
    public string juegoId;
    public string estudianteId;

}