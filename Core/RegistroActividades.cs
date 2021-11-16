using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Actividad_Natacion.Core
{
    public class RegistroActividades : ObservableCollection<Actividad>
    {
        public RegistroActividades():base(new List<Actividad>())
        {
        }

        public RegistroActividades(List<Actividad> list) : base(list)
        {
        }
        
        
        public void AddActividad(Actividad nueva) => base.Add(nueva);

        public void RemoveActividad(Actividad x) => base.Remove(x);

        public void RemoveActividadForIndex(int i) => base.RemoveAt(i);

        public Actividad[] RegistroActividadesToArray => this.ToArray();
        
        public Actividad[] RegistroActividadesToArrayByFecha => this.OrderBy(x => x.Fecha).ToArray();

        public List<int> getMinutosPorActividad()
        {
            List<int> toRet = new List<int>();
            foreach (var a in this.OrderBy(x => x.Fecha))
            {
                toRet.Add(int.Parse(a.Duracion.TotalMinutes.ToString()));
            }
            return toRet;
        }
        
        public List<int> getMinutosPorMes()
        {
            List<int> toRet = new List<int>();
            int mesAnterior=1;
            int minutosAcumulados = 0;
            foreach (var a in this.OrderBy(x => x.Fecha))
            {
                
                if(a.Fecha.Month!=mesAnterior)
                {
                    toRet.Add(minutosAcumulados);
                    minutosAcumulados = 0;
                }
                minutosAcumulados += int.Parse(a.Duracion.TotalMinutes.ToString());
                mesAnterior = a.Fecha.Month;
                
            }
            return toRet;
        }
        
        public List<int> getMetrosPorMes()
        {
            List<int> toRet = new List<int>();
            int mesAnterior=1;
            int metrosAcumulados = 0;
            foreach (var a in this.OrderBy(x => x.Fecha))
            {
                
                if(a.Fecha.Month!=mesAnterior)
                {
                    toRet.Add(metrosAcumulados);
                    metrosAcumulados = 0;
                }
                metrosAcumulados += a.Distancia;
                mesAnterior = a.Fecha.Month;
                
            }
            return toRet;
        }
        
        public List<int> getMetrosPorActividad()
        {
            List<int> toRet = new List<int>();
            foreach (var a in this.OrderBy(x => x.Fecha))
            {
                toRet.Add(a.Distancia);
            }
            return toRet;
        }
        
        public List<int> getFechasPorActividad()
        {
            List<int> toRet = new List<int>();
            foreach (var a in this.OrderBy(x => x.Fecha))
            {
                toRet.Add(a.Fecha.Day);
            }
            return toRet;
        }
        
        public int Length => this.Count;
        
        
    }
}