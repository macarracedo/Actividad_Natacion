using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Actividad_Natacion
{
    public partial class Vista_Calendario : Window
    {
        public Vista_Calendario()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var today = DateTime.Today;
            var cal = this.FindControl<Calendar>("Cal");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}