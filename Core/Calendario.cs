using System;
using System.Collections.Generic;
using System.Globalization;
using Tmds.DBus;

namespace Actividad_Natacion.Core
{
    public class Calendario
    {

        Dictionary<DateTime, Actividad> Actividades { get; }

        public Calendario()
        {
            Actividades = new Dictionary<DateTime, Actividad>();
        }

        public Calendario(RegistroActividades r)
        {
            Actividades = new Dictionary<DateTime, Actividad>();
            Console.WriteLine("Intentando cargar los datos en un calendario");
            Console.WriteLine("Tamaño de la lista: " + r.Count);
            foreach (var a in r)
            {
                this.addActividad(a);
            }

            Console.WriteLine("Generado un calendario con " + Actividades.Count + " pares.");
        }

        public void addActividad(Actividad? a)
        {
            Actividades.Add(a.Fecha, a);
        }

        public Actividad getActividad(DateTime date)
        {
            return Actividades[date];
        }

        public void removeActividad(Actividad a)
        {
            Actividades.Remove(a.Fecha);
        }

        public bool existsActividad(Actividad a)
        {
            return Actividades.ContainsValue(a);
        }
        
        public bool existsActividad(DateTime date)
        {
            return Actividades.ContainsKey(date);
        }

        public int countActividades()
        {
            return Actividades.Count;
        }
    }
}