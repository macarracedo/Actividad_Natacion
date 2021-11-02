using System;

namespace Actividad_Natacion
{
    public class Actividad
    {
        public Actividad(TimeSpan d, double km, string n)
        {
            this.Duracion = d;
            this.Distancia = km;
            this.Notas = n;
        }
        
        public TimeSpan Duracion { get; set; }
        public double Distancia { get; set; }
        public string Notas { get; set; }
    }
}