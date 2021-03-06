﻿using ConcesionarioApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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
        public ObservableCollection<ConcesionarioInfo> infoConcesionarios { get; set; }

        public ConcesionariosModelView()
        {
            ListarConcesionarioInfo();
        }

        public void ListarConcesionarioInfo()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select c.concesionarioID id, c.nombre nombre, " +
                    "d.poblacion poblacion , d.ciudad ciudad from Concesionarios c inner join Direcciones d " +
                    "on c.direccionID=d.direccionID", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "TablaConcesionarios");

                if (infoConcesionarios == null)
                    infoConcesionarios = new ObservableCollection<ConcesionarioInfo>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    infoConcesionarios.Add(new ConcesionarioInfo
                    {
                        ID = Convert.ToInt32(dr[0].ToString()),
                        nombre = dr[1].ToString(),
                        poblacion = dr[2].ToString(),
                        ciudad = dr[3].ToString()
                    });
                    Console.WriteLine(infoConcesionarios.ToString());
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
