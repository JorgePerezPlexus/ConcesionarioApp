using ConcesionarioApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace ConcesionarioApp.ModelView
{
    public class ConcesionariosModelView : INotifyPropertyChanged
    {

        string connectionString = "Data Source=PLX300000002221\\SQLEXPRESS;Initial Catalog=dboConcesionarios;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;
        public ObservableCollection<Concesionario> concesionarios { get; set; }

        public ConcesionariosModelView()
        {
            FillList();
        }

        public void FillList()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from Concesionarios", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "TablaConcesionarios");

                if (concesionarios == null)
                    concesionarios = new ObservableCollection<Concesionario>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    concesionarios.Add(new Concesionario
                    {
                        ID = Convert.ToInt32(dr[0].ToString()),
                        nombre = dr[1].ToString(),
                        direccionID = Convert.ToInt32(dr[2].ToString())
                    });
                }
            }
            catch (Exception ex)
            {

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
