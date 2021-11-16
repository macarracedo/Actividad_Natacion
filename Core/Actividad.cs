using System;
using System.Globalization;
using System.Xml.Linq;

namespace Actividad_Natacion.Core
{
    public class Actividad
    {
        public Actividad()
        {
        }
        public Actividad(TimeSpan duracion, int distancia, string notas, int circuito, DateTime fecha)
        {
            this.Duracion = duracion;
            this.Distancia = distancia;
            this.Circuito = circuito;
            this.Notas = notas;
            this.Fecha = fecha;
            this.IdActividad = this.GenerateID();
        }
        public Actividad(TimeSpan duracion, int distancia, string notas, int circuito, DateTime fecha, int id_actividad)
        {
            this.Duracion = duracion;
            this.Distancia = distancia;
            this.Circuito = circuito;
            this.Notas = notas;
            this.Fecha = fecha;
            this.IdActividad = id_actividad;
        }
        
        /// <summary>
        /// Genera un id único para cada actividad. Con la implementacion actual suponemos que sólo se puede realizar
        /// una actividad por día. Si esto cambiase bastaría con cambiar este método.
        /// </summary>
        /// <returns>Identificador de tipo <see cref="int"/></returns>
        private int GenerateID()
        {
            string id=this.Fecha.Year+""+this.Fecha.Month+""+this.Fecha.Day+"";
            return Int32.Parse(id);
        }

        public DateTime Fecha { get; set; }

        public string FechaShort
        {
            get
            {
                return Fecha.ToShortDateString();
            }
        }
        public TimeSpan Duracion { get; set; }

        public string DuracionMinutos
        {
            get
            {
                return Duracion.TotalMinutes.ToString();
            }
        }

        public int Distancia { get; set; }

        public double DistanciaEnKm
        {
            get
            {
                return this.Distancia / 1000.0000;
            }
        }

        public string Notas { get; set; }
        public int IdActividad { get; }
        public int Circuito { get; set; }

        

        public XElement toXML()
        {
            var raiz = new XElement("actividad");
            raiz.Add(new XElement("id_actividad",this.IdActividad.ToString()));
            raiz.Add(new XElement("fecha",this.Fecha.ToString()));
            raiz.Add(new XElement("duracion",this.Duracion.ToString()));
            raiz.Add(new XElement("distancia",this.Distancia.ToString()));
            raiz.Add(new XElement("notas",this.Notas));
            raiz.Add(new XElement("circuito",this.Circuito.ToString()));

            return raiz;
        }
    }
}