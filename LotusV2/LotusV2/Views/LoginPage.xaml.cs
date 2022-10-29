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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

      async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

       async void Button_Clicked_1(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Usuario.Equals(EntryUser.Text) && u.Contraseña.Equals(EntryPassword.Text)).FirstOrDefault();

            if (myquery != null)
            {
                App.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
               
                    Device.BeginInvokeOnMainThread(async () => 
                    {

                        var result = await this.DisplayAlert("Error", "No se encontro el usuario", "Ok", "Cancelar");

                        if (result)
                            await Navigation.PushAsync(new LoginPage());
                        else
                        {
                            await Navigation.PushAsync(new LoginPage());
                        }
                    });
                }
            }
        }
    }
