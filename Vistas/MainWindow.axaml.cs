using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using Actividad_Natacion.Core;
using System.Xml.Linq;
using Actividad_Natacion.Core;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Actividad_Natacion.Vistas
{
    public partial class MainWindow : Window
    {
        private RegistroActividades actividades;
        private Calendario miCalendario;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            try
            {
                actividades = this.cargarXML();
                Console.WriteLine("Cargado el xml en la lista actividades.");
                miCalendario = new Calendario(actividades);
                Console.WriteLine("Cargada sesión anterior.");
            }
            catch (Exception exc)
            {
                actividades = new RegistroActividades();
                miCalendario = new Calendario();
                Console.WriteLine("No se ha cargado la sesión anterior.");
            }
            
            var opExit = this.FindControl<MenuItem>("OpExit");
            var opGuardar = this.FindControl<MenuItem>("OpGuardar");
            var opCargar = this.FindControl<MenuItem>("OpCargar");
            var OpInsertar = this.FindControl<MenuItem>("OpInsertar");


            
            var btVCalendario = this.FindControl<Button>("btVCalendario");
            var btVGrafica = this.FindControl<Button>("btVGrafica");
            //var btVAddActividad = this.FindControl<MenuItem>("btVAddActividad");
            var btVListado = this.FindControl<Button>("btVListado");
        
            opGuardar.Click += (_, _) => this.guardarXML();
            opCargar.Click += (_, _) => this.cargarXML();
            opExit.Click += (_, _) => this.saveAndClose();
            OpInsertar.Click += (_, _) => this.VistaCreaActividad();
            
            btVCalendario.Click += (_, _) => this.VistaCalendario();
            btVGrafica.Click += (_, _) => this.VistaGrafica();
            btVListado.Click += (_, _) => this.VistaListado();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void VistaCalendario()
        {
            new VCalendario(this.actividades, this.miCalendario).ShowDialog(this);
        }

        private void VistaGrafica()
        {
            new VGrafica(actividades,miCalendario).ShowDialog(this);
        }

        private void VistaCreaActividad()
        {
            new VNuevaActividad(this.actividades, this.miCalendario).ShowDialog(this);
        }

        private void VistaListado()
        {
            new VListado(this.actividades, this.miCalendario).ShowDialog(this);
        }
        private RegistroActividades? OnLoad(string nf)
        {
            //return XmlRegistroReservas.RecuperarXML(nf); //toXml = new XmlRegistroReservas(this.listaReservas);
            return null;
        }
        private void guardarXML()
        {
            var raiz = new XDocument();
            var listaActividades = new XElement("registro_actividades");
            
            foreach (var a in this.actividades.RegistroActividadesToArray)
            {
                listaActividades.Add(a.toXML());
            }
            raiz.Add(listaActividades);
            raiz.Save("actividades.xml");
            
            Console.WriteLine("Guardadas con éxito " + actividades.Length + " elementos.");
            
        }

        private void saveAndClose()
        {
            this.guardarXML();
            this.Close();
        }
        
        private RegistroActividades cargarXML()
        {
            XDocument doc = XDocument.Load("actividades.xml");
            RegistroActividades toRet = new RegistroActividades();
            IEnumerable<XElement> listaActividades = doc.Root.Elements();
            IEnumerable<XElement> atributos;

            Actividad actt = new Actividad();
            int distancia=0;
            int circuito=0;
            TimeSpan duracion=new TimeSpan();
            DateTime fecha=new DateTime();
            string notas="";
            
            foreach (var a in listaActividades)
            {
                atributos = a.Elements();
                foreach (var atr in atributos)
                {
                    switch (atr.Name.ToString())
                    {
                        case "fecha":
                            fecha = DateTime.Parse(atr.Value.ToString());
                            break;
                        case "duracion":
                            duracion = TimeSpan.Parse(atr.Value.ToString());
                            break;
                        case "distancia":
                            distancia = Int32.Parse(atr.Value.ToString());
                            break;
                        case "notas":
                            notas = atr.Value.ToString();
                            break;
                        case "circuito":
                            circuito = Int32.Parse(atr.Value.ToString());
                            break;
                        default:
                            break;
                    }
                }
                Actividad toAdd = new Actividad(duracion, distancia, notas, circuito, fecha);
                toRet.AddActividad(toAdd);
            }
            Console.WriteLine("Cargadas con éxito " + toRet.Count + " actividades.");
            return toRet;
            /*
            XElement raiz = XElement.Load( "reparaciones.xml" );

            foreach (XElement subNodo in raiz.Elements())
            {
                Console.WriteLine(subNodo);
            }
            */
        }
        
    }
}