using System;
using System.Collections.Generic;
using Actividad_Natacion.Core;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;

namespace Actividad_Natacion.Vistas

{
    
    using Avalonia;
    using Avalonia.Media;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    public partial class VGrafica : Window
    {
        private RegistroActividades actividades;
        private Calendario miCalendario;
        private bool porActividad;
        

        public VGrafica(RegistroActividades actividades,Calendario miCalendario):this()
        {
            this.actividades = actividades;
            this.miCalendario = miCalendario;
            
            this.ChartMin = this.FindControl<Chart>( "ChGrfMin" );
            this.ChartMetros = this.FindControl<Chart>( "ChGrfMetros" );
            var btExit = this.FindControl<Button>("btExit");
            var rbBars = this.FindControl<RadioButton>( "RbBars" );
            var rbLine = this.FindControl<RadioButton>( "RbLine" );
            var rbMeses = this.FindControl<RadioButton>( "RbMeses" );
            var rbDias = this.FindControl<RadioButton>( "RbDias" );

            
            rbBars.Checked += (_, _) => this.OnChartFormatChanged();
            rbLine.Checked += (_, _) => this.OnChartFormatChanged();
            rbMeses.Checked += (_, _) => this.OnDisplayInfoChanged();
            rbDias.Checked += (_, _) => this.OnDisplayInfoChanged();
            
            btExit.Click += (_,_) => this.OnExit();

            this.ChartMin.DataPen.LineCap = PenLineCap.Square;
            this.ChartMin.AxisPen.Thickness = 2.0;
            this.ChartMin.DataPen.Thickness = 1.0;
            this.ChartMin.LegendY = "Minutos";
            this.ChartMin.LegendX = "Días";
            this.ChartMin.Values = this.actividades.getMinutosPorActividad();

            this.ChartMetros.DataPen.LineCap = PenLineCap.Round;
            this.ChartMetros.AxisPen.Thickness = 2.0;
            this.ChartMetros.DataPen.Brush = new SolidColorBrush(new Color(255,0,100,162));
            this.ChartMetros.DataPen.Thickness = 2.0;
            this.ChartMetros.LegendY = "Metros";
            this.ChartMetros.LegendX = "Días";
            this.ChartMetros.Values = this.actividades.getMetrosPorActividad();

            porActividad = true;

        }

        private void OnExit()
        {
            this.Close();
        }
        
        public VGrafica()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void OnDisplayInfoChanged()
        {
            if ( porActividad ) {
                this.ChartMin.Values = this.actividades.getMinutosPorMes();
                this.ChartMetros.LegendX = "Meses";
                this.ChartMetros.Values = this.actividades.getMetrosPorMes();
                this.ChartMin.LegendX = "Meses";
                porActividad = false;
            } else {
                this.ChartMetros.LegendX = "Días";
                this.ChartMin.Values = this.actividades.getMinutosPorActividad();
                this.ChartMin.LegendX = "Días";
                this.ChartMetros.Values = this.actividades.getMetrosPorActividad();
                porActividad = true;
            }
            
            this.ChartMin.Draw();
            this.ChartMetros.Draw();
        }
        
        void OnChartFormatChanged()
        {
            if ( this.ChartMin.Type == Chart.ChartType.Bars ) {
                this.ChartMin.Type = Chart.ChartType.Lines;
                this.ChartMetros.Type = Chart.ChartType.Lines;
            } else {
                this.ChartMin.Type = Chart.ChartType.Bars;
                this.ChartMetros.Type = Chart.ChartType.Bars;
            }
            
            this.ChartMin.Draw();
            this.ChartMetros.Draw();

        }
        
        public override void Render(DrawingContext context)
        {
            base.Render( context );

            this.ChartMin.Draw();
            this.ChartMetros.Draw();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void update()
        {
        }
        
        Chart ChartMin { get; }
        Chart ChartMetros { get; }
    }
    
    

}