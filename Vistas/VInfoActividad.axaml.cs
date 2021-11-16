using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Actividad_Natacion.Core;

namespace Actividad_Natacion.Vistas
{
    public partial class VInfoActividad : Window
    {
        private Actividad a;
        public VInfoActividad(Actividad actividad) : this()
        {
            a = actividad;
            var tbDuracion= this.FindControl<TextBox>("tbDuracion");

            var tbDistancia= this.FindControl<TextBox>("tbDistancia");
            var tbCircuito= this.FindControl<TextBox>("tbCircuito");
            var tbNotas= this.FindControl<TextBox>("tbNotas");
            var tbFecha= this.FindControl<TextBox>("tbFecha");

            var btExit = this.FindControl<Button>("btExit");

            tbCircuito.Text = a.Circuito.ToString();
            tbDistancia.Text = a.Distancia.ToString();
            tbDuracion.Text = a.Duracion.ToString();
            tbNotas.Text = a.Notas;
            tbFecha.Text = a.Fecha.ToShortDateString();
            btExit.Click += (_, _) => this.OnExit();

        }
        public VInfoActividad()
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

        
    }
}