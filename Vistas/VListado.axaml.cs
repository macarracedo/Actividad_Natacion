using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actividad_Natacion.Core;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Actividad_Natacion.Vistas
{
    public partial class VListado : Window
    {
        private RegistroActividades actividades;
        private Calendario miCalendario;
        public VListado(RegistroActividades listaActividades, Calendario miCalendario) : this()
        {
            this.actividades = listaActividades;
            this.miCalendario = miCalendario;
            var btExit = this.FindControl<Button>("btExit");
            var dtActividades = this.FindControl<DataGrid>("DtActividades");
            var btEliminar = this.FindControl<Button>("btEliminar");
            var btModificar = this.FindControl<Button>("btModificar");
            var btConsultar = this.FindControl<Button>("btConsultar");

            var btConfEliminar = this.FindControl<Button>("btConfEliminar");
            var btConfCancelar = this.FindControl<Button>("btConfCancelar");
            var btInsertar = this.FindControl<Button>("btInsertar");

            dtActividades.Items = listaActividades;

            btExit.Click += (_,_) => this.OnExit();
            btInsertar.Click += (_, _) => this.OnInsert();
            btConfEliminar.Click += (_, _) => this.OnDelete(dtActividades.SelectedIndex);
            btConfCancelar.Click += (_, _) => this.OnConfirmCancel();
            btEliminar.Click += (_, _) => this.OnConfirmDelete(dtActividades.SelectedIndex);
            btModificar.Click += (_, _) => this.OnModify(dtActividades.SelectedIndex);
            btConsultar.Click += (_, _) => this.OnCheck(dtActividades.SelectedIndex);

            
        }
        private void OnExit()
        {
            this.Close();
        }
        
        public VListado()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void OnCheck(int index)
        {
            if (index != -1)
            {
                new VInfoActividad(this.actividades[index]).ShowDialog(this);
            }
            else
            {
                new MessageWindow("Debes seleccionar una fila antes",false).Show();
            }
            
        }

        private void OnConfirmDelete(int position)
        {
            
            if (position != -1)
            {
                var dpConfimacion = this.FindControl<DockPanel>("dpConfimacion");
                dpConfimacion.IsVisible = true;
            }
            else
            {
                new MessageWindow("Debes seleccionar una fila antes",false).Show();
            }
            
        }

        private void OnConfirmCancel()
        {
            var dpConfimacion = this.FindControl<DockPanel>("dpConfimacion");
            dpConfimacion.IsVisible = false;
        }
        
        private void OnDelete(int position)
        {
            if (position != -1)
            {
                try
                {
                    Actividad toRemove = this.actividades[position];
                    this.actividades.RemoveActividad(toRemove);
                    this.miCalendario.removeActividad(toRemove);
                    this.OnConfirmCancel();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
            else
            {
                new MessageWindow("Debes seleccionar una fila antes",false).Show();
            }
            
        }

        private void OnModify(int position)
        {
            if (position != -1)
            {
                new VNuevaActividad(this.actividades, this.miCalendario, this.actividades[position]).ShowDialog(this);
            }
            else
            {
                new MessageWindow("Debes seleccionar una fila antes",false).Show();
            }
        }

        private void OnInsert()
        {
            new VNuevaActividad(this.actividades,this.miCalendario).ShowDialog(this);
        }

        private RegistroActividades? OnLoad(string nf)
        {
            //return XmlRegistroReservas.RecuperarXML(nf); //toXml = new XmlRegistroReservas(this.listaReservas);
            return null;
        }
        private void OnUpdateCount()
        {
            var count = this.FindControl<Label>("LbNumActividades");
            count.Content=this.actividades.Length.ToString();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}