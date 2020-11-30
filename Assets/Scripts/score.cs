using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class score 
{
    public int id { get; set; }
    public string puntos { get; set; }
    public string fecha { get; set; }
    public string pausa { get; set; }
    public int nivel { get; set; }

    public score(int id, string puntos, string fecha, string pausa, int nivel)
    {
        this.id = id;
        this.puntos = puntos;
        this.fecha = fecha;
        this.pausa = pausa;
        this.nivel = nivel;
    }
}
