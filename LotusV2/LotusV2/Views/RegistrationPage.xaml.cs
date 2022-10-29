using LotusV2.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LotusV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<RegUserTable>();
            var item = new RegUserTable()
            {
                Usuario = EntryUsername.Text,
                Contraseña = EntryUserPassword.Text,
                Correo = EntryEmail.Text,
                Telefono = EntryTel.Text,

            };
            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("Registro Exitoso", "Bienvenido", "Ok", "Cancelar");

                if (result)
                    await Navigation.PushAsync(new LoginPage());
            });
        }
    }
}