using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinWebApi.Models;

namespace XamarinWebApi.Service
{
    class DataService
    {
        HttpClient client = new HttpClient();
        public async Task<List<User>> GetUserAsync()
        {
            try
            {
                string url = "http://192.168.15.156:8080/Users";
                var response = await client.GetStringAsync(url);
                var users = JsonConvert.DeserializeObject<List<User>>(response);
                return users;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                string url = "http://192.168.15.156:8080/Users";
                var dados = JsonConvert.SerializeObject(user);
                var content = new StringContent(dados, Encoding.UTF8, "application/json");
                HttpResponseMessage resposta;
                resposta = await client.PostAsync(url, content);
                if (!resposta.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir produto");
                }
                return resposta.IsSuccessStatusCode;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
