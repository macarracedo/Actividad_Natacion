using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Actividad_Natacion.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Actividad_Natacion.Vistas
{
    public partial class VNuevaActividad : Window
    {
        
        private RegistroActividades actividades;
        private Calendario miCalendario;
        private Actividad a;
        private DateTime opDate;
        public int Circuito_MinValue = 3;
        public int Circuito_MaxValue = 12;
        public int Circuito_DefaultValue = 5;

        
        public VNuevaActividad(RegistroActividades actividades,Calendario miCalendario):this()
        {
            this.actividades = actividades;
            this.miCalendario = miCalendario;
            var nudCircuito= this.FindControl<NumericUpDown>("nudCircuito");
            nudCircuito.Minimum = Circuito_MinValue;
            nudCircuito.Maximum = Circuito_MaxValue;
            nudCircuito.Value = Circuito_DefaultValue;
            
            this.drawButton("Insertar"); 
        }
        
        public VNuevaActividad(RegistroActividades actividades,Calendario miCalendario, DateTime? date):this()
        {
            this.actividades = actividades;
            this.miCalendario = miCalendario;
            DateTime date2 = date ?? default(DateTime);
            this.opDate = date2;
            var nudCircuito= this.FindControl<NumericUpDown>("nudCircuito");
            nudCircuito.Minimum = Circuito_MinValue;
            nudCircuito.Maximum = Circuito_MaxValue;
            nudCircuito.Value = Circuito_DefaultValue;
            this.fillJustDate();
            this.drawButton("Insertar"); 
        }
        
        
        //Constructor para modificacion
        public VNuevaActividad(RegistroActividades actividades,Calendario miCalendario, Actividad a):this()
        {
            this.actividades = actividades;
            this.miCalendario = miCalendario;
            this.a = a;
            var nudCircuito= this.FindControl<NumericUpDown>("nudCircuito");
            nudCircuito.Minimum = Circuito_MinValue;
            nudCircuito.Maximum = Circuito_MaxValue;
            nudCircuito.Value = Circuito_DefaultValue;
            this.fillData();
            this.drawButton("Guardar cambios");
            
        }

        public VNuevaActividad()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void fillJustDate()
        {
            var dpFecha= this.FindControl<DatePicker>("dpFecha");
            dpFecha.SelectedDate = this.opDate;
            dpFecha.IsEnabled = false;
        }
        
        private void fillData()
        {
            var tbDuracion= this.FindControl<TextBox>("tbDuracion");
            var tbDistancia= this.FindControl<TextBox>("tbDistancia");
            var nudCircuito= this.FindControl<NumericUpDown>("nudCircuito");
            var tbNotas= this.FindControl<TextBox>("tbNotas");
            var dpFecha= this.FindControl<DatePicker>("dpFecha");

            tbDuracion.Text = this.a.Duracion.TotalMinutes.ToString();
            tbDistancia.Text = this.a.Distancia.ToString();
            //tbCircuito.Text = this.a.Circuito.ToString();
            nudCircuito.Value = Double.Parse(this.a.Circuito.ToString());
            tbNotas.Text = this.a.Notas;
            dpFecha.SelectedDate = this.a.Fecha;
        }
        private void drawButton(string op)
        {
            var btExit = this.FindControl<Button>("btExit");
            btExit.Click += (_,_) => this.OnExit();
            var btInsertarActividad = this.FindControl<Button>("btInsertarActividad");
            btInsertarActividad.Content = op;
            System.Console.WriteLine("Draw Button...");
            btInsertarActividad.Click += (_,_) => this.OnClick();
            

        }

        private void OnExit()
        {
            this.Close();
        }

        
        private void OnClick()
        {
           
            //Introducir logica de INSERCION o MODIFICACION

            if (this.a == null)
            {
                this.OnInsert();
            }
            else
            {
                this.OnModify();
            }

        }

        private void OnModify()
        {
            if (this.CheckIfNull())
            {
                var tbDuracion= this.FindControl<TextBox>("tbDuracion");
                var tbDistancia= this.FindControl<TextBox>("tbDistancia");
                var nudCircuito= this.FindControl<NumericUpDown>("nudCircuito");
                var tbNotas= this.FindControl<TextBox>("tbNotas");
                var dpFecha= this.FindControl<DatePicker>("dpFecha");

                
                DateTime fecha = dpFecha.SelectedDate.Value.DateTime;
                TimeSpan duracion = new TimeSpan(0, int.Parse(tbDuracion.Text),0);
                
                if (this.CheckNumberInteger(tbDistancia.Text) && this.CheckNumberInteger(tbDuracion.Text))
                {
                    Actividad toAdd = new Actividad(duracion, int.Parse(tbDistancia.Text), tbNotas.Text,
                        int.Parse(nudCircuito.Text), fecha);
                    //Reserva toAdd = new Reserva(cliente,habitacion,fEntrada,fSalida,Int32.Parse(iva.Text),hayGaraje,Double.Parse(precioDia.Text),tipo.Text );

                    this.actividades.RemoveActividad(this.a);
                    this.miCalendario.removeActividad(this.a);
                    this.actividades.AddActividad(toAdd);
                    this.miCalendario.addActividad(toAdd);
                    this.Close();
                }
                else
                {
                    new MessageWindow("Error en algun dato",false).Show();
                }
            }
            else
            {
                new MessageWindow("Algún campo esta vacío",false).Show();
            }
        }

        private void OnInsert()
        {
            if (this.CheckIfNull())
            {
                var tbDuracion= this.FindControl<TextBox>("tbDuracion");
                var tbDistancia= this.FindControl<TextBox>("tbDistancia");
                var nudCircuito= this.FindControl<NumericUpDown>("nudCircuito");
                var tbNotas= this.FindControl<TextBox>("tbNotas");
                var dpFecha= this.FindControl<DatePicker>("dpFecha");

                
                DateTime fecha = dpFecha.SelectedDate.Value.DateTime;
                TimeSpan duracion = new TimeSpan(0, int.Parse(tbDuracion.Text),0);

                if (this.CheckNumberInteger(tbDistancia.Text) && this.CheckNumberInteger(tbDuracion.Text))
                {
                    Actividad toAdd = new Actividad(duracion, int.Parse(tbDistancia.Text), tbNotas.Text,
                        int.Parse(nudCircuito.Text), fecha);
                    this.actividades.AddActividad(toAdd);
                    this.miCalendario.addActividad(toAdd);
                    System.Console.WriteLine("Actividad añadida con éxito");
                    this.Close();
                }
                else
                {
                    new MessageWindow("Error en algun dato",false).Show();
                }
            }
            else
            {
                new MessageWindow("Algún campo esta vacío",false).Show();
            }


        }
        
        
        private bool CheckNumberInteger(string value)
        {
            bool toret = true;
            try
            {
                int v=Int32.Parse(value);
            }
            catch (Exception exc)
            {
                toret = false;
            }

            return toret;
        }


        /// <summary>
        /// metodo que comprueba que todos los campos tienen valor
        /// </summary>
        private bool CheckIfNull()
        {
            var tbDuracion= this.FindControl<TextBox>("tbDuracion");
            var tbDistancia= this.FindControl<TextBox>("tbDistancia");
            var nudCircuito= this.FindControl<NumericUpDown>("nudCircuito");

            var tbNotas= this.FindControl<TextBox>("tbNotas");
            var dpFecha= this.FindControl<DatePicker>("dpFecha");

            if (tbDuracion.Text == null || dpFecha.SelectedDate == null || tbDistancia.Text == null
                || tbNotas.Text == null || nudCircuito.Text == null)
            {
                return false;
            }

            return true;

        }
        /// <summary>
        /// Comprueba que donde se pide un numero se escribe
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckNumberDouble(string value)
        {
            bool toret = true;
            try
            {
                int v=Int32.Parse(value);
            }
            catch (Exception exc)
            {
                toret = false;
            }

            return toret;
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}