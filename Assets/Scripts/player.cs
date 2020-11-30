using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class player
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string edad { get; set; }
        public string user { get; set; }
        public string password { get; set;} 
        public List<score> putuacion { get; set; }



    public player(int id, string nombre, string edad, string user, string password)
        {
            this.id = id;
            this.nombre = nombre;
            this.edad = edad;
            this.user = user;
            this.password = password;
        }

    public player(int id, string nombre, string edad, string user, string password, List<score> putuacion)
    {
        this.id = id;
        this.nombre = nombre;
        this.edad = edad;
        this.user = user;
        this.password = password;
        this.putuacion = putuacion;
    }


}

