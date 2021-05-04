using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using XamarinWebApi.Models;
using XamarinWebApi.Service;

namespace XamarinWebApi.ModelView
{
    class CadastrarUsuarioModelView
    {
        private CadastrarUsuario _page;
        private DataService _dataService;
        public CadastrarUsuarioModelView(CadastrarUsuario page)
        {
            _page = page;
            _dataService = new DataService();
        }

        public async void cadastrarUsuario(User user)
        {
            var errors = new List<ValidationResult>();
            var context = new ValidationContext(user);
            bool isValid = Validator.TryValidateObject(user, context, errors, true);
            var properties = GetValidatablePropertyNames(user);
            foreach (var propertyName in properties)
            {
                setVisibilidadeErros(propertyName.ToString().Replace("User.", ""), false);
            }
            foreach (var erro in errors)
            {
                var enu = erro.MemberNames.GetEnumerator();
                while (enu.MoveNext())
                {
                    setVisibilidadeErros(enu.Current, true);
                }
            }

            if (isValid)
            {
                try
                {
                    if (await _dataService.AddUserAsync(user))
                    {
                        _page.DisplayAlert("Alert", "Usuário cadastrado", "OK");
                        foreach (var propertyName in properties)
                        {
                            setVisibilidadeErros(propertyName, false);
                            ApagaTexto(propertyName);
                        }

                    }
                }
                catch (Exception ex)
                {
                    _page.DisplayAlert("Alert", "Erro: " + ex.Message, "OK");
                }
            }
            else
            {
                _page.DisplayAlert("Alert", "Dados inválidos", "OK");
            }

        }

        private void setVisibilidadeErros(String campo,bool visivilidade)
        {
            var control = _page.FindByName<Label>("lb_" + campo);
            if (control != null)
            {
                control.IsVisible = visivilidade;
            }
        }
        private void ApagaTexto(String campo)
        {
            var control = _page.FindByName<Entry>("et_" + campo);
            if (control != null)
            {
                control.Text = "";
            }
        }

        private static IEnumerable<string> GetValidatablePropertyNames(object model)
        {
            var validatableProperties = new List<string>();
            var properties = GetValidatableProperties(model);
            foreach (var propertyInfo in properties)
            {
                var errorControlName = propertyInfo.Name;
                validatableProperties.Add(errorControlName);
            }
            return validatableProperties;
        }
        private static List<PropertyInfo> GetValidatableProperties(object model)
        {
            var properties = model.GetType().GetProperties().Where(prop => prop.CanRead
                //&& prop.GetCustomAttributes(typeof(ValidationAttribute), true).Any()//
                && prop.GetIndexParameters().Length == 0).ToList();
            return properties;
        }
    }

}
