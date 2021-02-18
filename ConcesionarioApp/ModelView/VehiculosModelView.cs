using ConcesionarioApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ConcesionarioApp.ModelView
{
    public class VehiculosModelView
    {
        string connectionString = "Data Source=PLX300000002221\\SQLEXPRESS;Initial Catalog=dboConcesionarios;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;
        public ObservableCollection<Vehiculo> stock { get; set; }

        public VehiculosModelView()
        {
          
        }

        public void actualizarVehiculos(int concesionarioID)
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select v.vehiculoID, v.marca, v.modelo, v.km, " +
                    "v.vendido from stock s inner join Vehiculos v on v.vehiculoID=s.vehiculoID where concesionarioID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", concesionarioID);
                adapter = new SqlDataAdapter(cmd);
                
                ds = new DataSet();
                adapter.Fill(ds, "TablaConcesionarios");

                if (stock == null)
                    stock = new ObservableCollection<Vehiculo>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    stock.Add(new Vehiculo
                    {
                        ID = Convert.ToInt32(dr[0].ToString()),
                        Marca = dr[1].ToString(),
                        Modelo = dr[2].ToString(),
                        Km= Convert.ToInt32(dr[3].ToString()),
                        Vendido = dr[2].ToString(),

                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la recuparacion de informacion de concesionario " + ex);
                MessageBox.Show("SQL Query Failed : " + ex.ToString());
            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        INotifyPropertyChanged Members;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
}
