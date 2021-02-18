using ConcesionarioApp.Model;
using ConcesionarioApp.ModelView;
using System;
using System.Data;
using System.Windows;

namespace ConcesionarioApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConcesionariosModelView cmv;
        VehiculosModelView vmv;
        public MainWindow()
        {
            InitializeComponent();
            cmv = new ConcesionariosModelView();
            vmv = new VehiculosModelView();
            base.DataContext = cmv;
            //base.DataContext = vmv;
        }



        private void TablaConcesionarios_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ConcesionarioInfo selected = (ConcesionarioInfo) TablaConcesionarios.SelectedItems[0];
            //Console.WriteLine(selected.ID);
            vmv.actualizarVehiculos(selected.ID);
        }
    }
}
