using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinWebApi.Models;
using XamarinWebApi.ModelView;

namespace XamarinWebApi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarUsuario : ContentPage
    {
        CadastrarUsuarioModelView modelView;
        INavigation navigation { get; }
        public CadastrarUsuario(INavigation nav)
        {
            InitializeComponent();
            navigation = nav;
            modelView = new CadastrarUsuarioModelView(this);
        }

        private void Button_Cadastrar(object sender, EventArgs e)
        {
            User user = new User();
            user.firstName = et_firstName.Text;
            user.surname = et_surname.Text;
            user.age = 0;
            if (et_age.Text != null && et_age.Text != "")
            {
                user.age = Int16.Parse(et_age.Text);
            }
            modelView.cadastrarUsuario(user);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //lets the Entry be empty
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (!Int16.TryParse(e.NewTextValue, out Int16 value))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }
    }
}