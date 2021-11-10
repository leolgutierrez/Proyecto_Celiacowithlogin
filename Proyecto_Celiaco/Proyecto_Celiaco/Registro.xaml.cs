using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Proyecto_Celiaco.card;

namespace Proyecto_Celiaco
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }

        private async void btninsertarregistro_Clicked(object sender, EventArgs e)
        {
            usuario user = new usuario
            {
                nombre_usuario = txtboxusuario.Text,
                contraseña = txtboxcontraseña.Text,
                email = txtboxcorreo.Text
                //asdasdaasd

            };


            await App.SQLiteDB.SaveusuarioAsync(user);
            await DisplayAlert("Registrado", "proba logeando", "uwu");
        }
    }
}