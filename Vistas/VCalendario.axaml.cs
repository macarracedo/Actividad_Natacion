using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Threading.Tasks;
using Actividad_Natacion.Core;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Actividad_Natacion.Vistas
{
    public partial class VCalendario : Window
    {
        private RegistroActividades actividades;
        private Calendario miCalendario;
        public VCalendario(RegistroActividades listaActividades, Calendario miCalendario):this()
        {
            var cal = this.FindControl<Calendar>("Cal");
            var dpInfo = this.FindControl<DockPanel>("InfoActividad");
            var btExit = this.FindControl<Button>("btExit");

            var btInsertar = this.FindControl<Button>("btInsertar");
            var btModificar = this.FindControl<Button>("btModificar");
            var btEliminar = this.FindControl<Button>("btEliminar");
            var btConfEliminar = this.FindControl<Button>("btConfEliminar");
            var btConfCancelar = this.FindControl<Button>("btConfCancelar");
            
            this.actividades = listaActividades;
            this.miCalendario = miCalendario;
            
            cal.SelectedDatesChanged += (_, _) => this.SelectionChanged();
            btInsertar.Click += (_, _) => this.OnInsert();
            btModificar.Click += (_, _) => this.OnModify(cal.SelectedDate);
            btEliminar.Click += (_, _) => this.OnConfirmDelete();
            btConfEliminar.Click += (_, _) => this.OnDelete(cal.SelectedDate);
            btConfCancelar.Click += (_, _) => this.OnConfirmCancel();
            btExit.Click += (_, _) => this.OnExit();
            CalendarBlackoutDatesCollection blackoutDatesCollection = new CalendarBlackoutDatesCollection(new Calendar());
            //var c = micalendario.Get();
            //c.ForEach(f => cal.SelectedDates.Add(f));
            
            
        }
        
        public VCalendario()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void OnExit()
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private void OnInsert()
        {
            var cal = this.FindControl<Calendar>("Cal");
            new VNuevaActividad(this.actividades,this.miCalendario,cal.SelectedDate).ShowDialog(this);
        }

        private void OnModify(DateTime? date)
        {
            if (date != null)
            {
                DateTime date2 = date ?? default(DateTime);
                new VNuevaActividad(this.actividades, this.miCalendario, this.miCalendario.getActividad(date2))
                    .ShowDialog(this);
            }
            else
            {
                new MessageWindow("Debes seleccionar una fecha antes",false).Show();
            }
        }

        private void OnConfirmDelete()
        {
            var dpConfimacion = this.FindControl<DockPanel>("dpConfimacion");
            dpConfimacion.IsVisible = true;
        }

        private void OnConfirmCancel()
        {
            var dpConfimacion = this.FindControl<DockPanel>("dpConfimacion");
            dpConfimacion.IsVisible = false;
        }
        
        private void OnDelete(DateTime? date)
        {
            if (date != null)
            {
                try
                {
                    DateTime date2 = date ?? default(DateTime);
                    this.actividades.RemoveActividad(this.miCalendario.getActividad(date2));
                    this.miCalendario.removeActividad(this.miCalendario.getActividad(date2));
                    this.SelectionChanged();
                    this.OnConfirmCancel();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error.");
                }
            }
            else
            {
                new MessageWindow("Debes seleccionar una fecha antes",false).Show();
            }
        }
        
        private void SelectionChanged()
        {
            var tbDate = this.FindControl<TextBox>("tbDate");
            var cal = this.FindControl<Calendar>("Cal");
            var dpInfo = this.FindControl<DockPanel>("InfoActividad");
            if (this.existeActividad(cal.SelectedDate.Value))
            {
                tbDate.Text = "Informacion de la actividad: ";
                this.fillData(miCalendario.getActividad(cal.SelectedDate.Value));
                this.showInfoActividad(true);
                //cal.
            }
            else
            {
                tbDate.Text = "No hay actividad para el día seleccioado.";
                this.showInfoActividad(false);
            }
            
        }

        private void showInfoActividad(bool visible)
        {
            var tbDistancia = this.FindControl<DockPanel>("InfoDistancia");
            var tbDuracion = this.FindControl<DockPanel>("InfoDuracion");
            var tbCircuito = this.FindControl<DockPanel>("InfoCircuito");
            var tbFecha = this.FindControl<DockPanel>("InfoFecha");
            var tbNotas1 = this.FindControl<DockPanel>("InfoNotas1");

            var btInsertar = this.FindControl<Button>("btInsertar");
            var btModificar = this.FindControl<Button>("btModificar");
            var btEliminar = this.FindControl<Button>("btEliminar");

            tbCircuito.IsVisible = visible;
            tbDistancia.IsVisible = visible;
            tbDuracion.IsVisible = visible;
            tbFecha.IsVisible = visible;
            tbNotas1.IsVisible = visible;
            
            btInsertar.IsVisible = !visible;
            btModificar.IsVisible = visible;
            btEliminar.IsVisible = visible;

        }

        private bool existeActividad(DateTime date)
        {
            bool toRet = false;
            
            if (date == null)
            {
                toRet = false;
            }else
            {
                toRet = miCalendario.existsActividad(date);
            }

            return toRet;
        }

        private void fillData(Actividad a)
        {
            var tbDistancia = this.FindControl<TextBox>("tbDuracion");
            var tbDuracion = this.FindControl<TextBox>("tbDistancia");
            var tbCircuito = this.FindControl<TextBox>("tbCircuito");
            var tbNotas = this.FindControl<TextBox>("tbNotas");
            var tbFecha = this.FindControl<TextBox>("tbFecha");

            tbDistancia.Text = a.Distancia.ToString();
            tbDuracion.Text = a.Duracion.TotalMinutes.ToString();
            tbCircuito.Text = a.Circuito.ToString();
            tbNotas.Text = a.Notas.ToString();
            tbFecha.Text = a.Fecha.ToShortDateString();
        }
    }
}