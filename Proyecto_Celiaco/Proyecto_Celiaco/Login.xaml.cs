using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using SQLitePCL;
using System.IO;
using Microsoft.Data.Sqlite;

namespace Proyecto_Celiaco
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnregistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Registro());
        }

        private void botonlogear_Clicked(object sender, EventArgs e)
        {
            //ACA SACO LA DIRECC DE LA BDD 
            SqliteConnection db =
               new SqliteConnection($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "proyectox.db3")}");
            db.Open();//abro la canilla
            string comando = "select email from usuario where email='leo@gmail.com'"; //un ejemplo de select
            SqliteCommand cum = new SqliteCommand(comando, db);


            SqliteDataReader leedor = cum.ExecuteReader(); //abro un reader para que sea mas facil el manejo de datos
            if (leedor.Read())
            {
                txtgigante.Text = leedor.GetString(0); //el primer resultado de una tabla imaginaria
            }

        }
    }
}