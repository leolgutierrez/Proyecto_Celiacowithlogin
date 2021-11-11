﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Proyecto_Celiaco.card;
using SQLite;
using SQLitePCL;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Net.Mail;

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

            //Acontinuacion unos condicionales horribles para validar
            //datos y poder insertar si se cumplen los condicionales
            if (verificar_novacio())
            {
                if (validarusuario())
                {
                    if (validaremail())
                    {
                        await App.SQLiteDB.SaveusuarioAsync(user);
                        await DisplayAlert("Registrado", "Se le envio un correo", "uwu");

                        //ahora le envio el correo al pibe 
                        string Correo = txtboxcorreo.Text;
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress(txtboxcorreo.Text);
                        message.To.Add(Correo);
                        message.Subject = "Bienvenido a nuestra aplicacion";
                        message.Body = "Esperamos que disfrute de nuestro aplicacion \n \n \n atte \n nombreequipo ";
                        message.IsBodyHtml = false;


                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        System.Net.NetworkCredential credenciales = new System.Net.NetworkCredential();
                        credenciales.UserName = "leolenguajestres@gmail.com";
                        credenciales.Password = "retumbar2012";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = credenciales;
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Send(message);
                        



                    }

                }
                



                else
                {
                    if (validarusuario() == false)
                    {
                        await DisplayAlert("ERROR", "el nombre de usuario ya se encuentra en uso", "uwu");

                    }

                    if(validaremail()==false)
                    {
                        await DisplayAlert("ERROR", "Email ya en uso,", "uwu");

                    }

                }
                
            }
            else
            {
                await DisplayAlert("Alerta","Algunos campos estan vacios","ok");
            }
         
        }

        public bool verificar_novacio()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtboxusuario.Text))
            {
                respuesta = false;
            }

            else if (string.IsNullOrEmpty(txtboxcontraseña.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtboxcorreo.Text))
            {
                respuesta = false;
            }

            else
            {
                respuesta = true;
            }

            return respuesta;

        }


        public bool validarusuario()
        {
            bool respuesta;
            //ACA SACO LA DIRECC DE LA BDD 
            SqliteConnection db =
               new SqliteConnection($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "proyectox.db3")}");
            db.Open();//abro la canilla
            string comando = "select nombre_usuario from usuario where nombre_usuario='"+txtboxusuario.Text+"'"; //me fijo si existe el usuario
            SqliteCommand cum = new SqliteCommand(comando, db);


            SqliteDataReader leedor = cum.ExecuteReader(); //abro un reader para que sea mas facil el manejo de datos
            if (leedor.Read())
            {
                respuesta = false;
                ; //el primer resultado de una tabla imaginaria
            }
            else
            {
                respuesta = true;
            }

            
            return respuesta;
        }

        public bool validaremail()
        {
            bool respuesta;
            //ACA SACO LA DIRECC DE LA BDD 
            SqliteConnection db =
               new SqliteConnection($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "proyectox.db3")}");
            db.Open();//abro la canilla
            string comando = "select email from usuario where email='" + txtboxcorreo.Text + "'"; //me fijo si existe el correo
            SqliteCommand cum = new SqliteCommand(comando, db);


            SqliteDataReader leedor = cum.ExecuteReader(); //abro un reader para que sea mas facil el manejo de datos
            if (leedor.Read())
            {
                respuesta = false;
                ; //el primer resultado de una tabla imaginaria
            }
            else
            {
                respuesta = true;
            }


            return respuesta;

        }



    }
}