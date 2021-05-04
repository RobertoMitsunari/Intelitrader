using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinWebApi.Models;
using XamarinWebApi.Service;

namespace XamarinWebApi
{
    public partial class MainPage : ContentPage
    {
        DataService dataService;
        List<User> users;

        public MainPage()
        {
            InitializeComponent();
            dataService = new DataService();
            AtualizaDados();
        }

        private async void Abrir_Cadastro(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastrarUsuario(Navigation));
        }
        private async void AtualizaDados()
        {
            
            users = await dataService.GetUserAsync();
            lista.ItemsSource = users;

        }
        override
        protected void OnAppearing()
        {
            AtualizaDados();
        }
    }
}
