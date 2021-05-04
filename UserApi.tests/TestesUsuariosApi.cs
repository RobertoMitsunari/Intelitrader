using System;
using Xunit;
using UserAPI.Controller;
using UserAPI.Data;
using UserAPI.Profiles;
using UserAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.tests
{
    public class RepoTeste 
    {
        public MockUserRepo repository {get;}
        private IMapper _mapper;
        public UsersController control {get;}
        public RepoTeste()
        {
            _mapper = new MapperConfiguration(c => c.AddProfile<UsersProfile>()).CreateMapper();
            repository = new MockUserRepo();
            control = new UsersController(repository,_mapper);
        }


    }
    [CollectionDefinition("Sequencia1", DisableParallelization = true)]
    public class DatabaseCollection : ICollectionFixture<RepoTeste>
    {

    }

    [Collection("Sequencia1")]
    public class TesteGet
    {

        RepoTeste teste;

        public TesteGet(RepoTeste t)
        {
            teste = t;
        }


        [Fact]
        public void TesteGetById_ReturnsTeste1()
        {
            //Procura por um usuario já criado
            Assert.Equal("Teste1",teste.control.GetUserById(0).Value.firstName);
        }
        [Fact]
        public void TesteGetById_ReturnsNull()
        {
            //Procura por um usuario q nao existe
            Assert.Equal(null,teste.control.GetUserById(40).Value.firstName);
        }

        
        [Fact]
        public void TesteGetAllUsers_ReturnsAllUsers()
        {
            //Verifica se todos os usuarios já criados então presentes
            Assert.Equal(3,teste.repository.GetAllUsers().Count);
        }

    }
    
    [Collection("Sequencia1")]
    public class TesteInsert
    {

        RepoTeste teste;

        public TesteInsert(RepoTeste t)
        {
            teste = t;
        }

        
        [Fact]
         public void TestInsertUser_ReturnsUser3()
         {
            //Cria um usuario comum
            User user = new User();
            user.Id = 3;
            user.firstName = "Teste3";
            user.surname = "SurnameTeste3";
            user.age = 20;
            teste.control.CreateUser(user);
            User userInsrt = new User();
            userInsrt = teste.repository.GetUserById(3);
            Assert.Equal("Teste3", userInsrt.firstName);
         }



    }

    [Collection("Sequencia1")]
    public class TesteInsertData
    {
        RepoTeste teste;

        public TesteInsertData(RepoTeste t)
        {
            teste = t;
        }

        [Fact]
         public void TestDataDoUsuarioInserido_ReturnsDataAtual()
         {
            //Testa inserir de um usuario com o surname null
            //Verifica se a creationDate bate com a data atual
            User user = new User();
            user.Id = 4;
            user.firstName = "Teste4";
            user.surname = null;
            user.age = 20;
            teste.control.CreateUser(user);
            DateTime creationTime = DateTime.Now;
            User userInsrt = new User();
            userInsrt = teste.repository.GetUserById(4);
            bool testeData = false;
            if(creationTime.Date == userInsrt.creationDate.Date){
                testeData = true;
            }
            Assert.True(testeData);
            Assert.Null(userInsrt.surname);
        }

    }

    [Collection("Sequencia1")]
    public class TesteQntUsers
    {
        RepoTeste teste;

        public TesteQntUsers(RepoTeste t)
        {
            teste = t;
        }


        [Fact]
        public void TesteGetAllUsers_Returns5()
        {
            Assert.Equal(5,teste.repository.GetAllUsers().Count);
        }

    }

    [Collection("Sequencia1")]
    public class TesteUsuariosApiUpdate
    {
        RepoTeste teste;

        public TesteUsuariosApiUpdate(RepoTeste t)
        {
            teste = t;
        }

        [Fact]
        public void TestUpdateUser_ReturnsUpdatedUser3()
        {
            //Testa se é possivel atualizar um usuario
            //Teste se o surname com valor null foi atualizado corretamente
            User userInsrt = new User();
            userInsrt.Id = 4;
            userInsrt.firstName = "Teste44";
            userInsrt.surname = "SurnameTeste4";
            userInsrt.age = 20;
            teste.control.UpdateUser(4,userInsrt);
            userInsrt = teste.repository.GetUserById(4);
            Assert.Equal("Teste44", userInsrt.firstName);
            Assert.Equal("SurnameTeste4", userInsrt.surname);
        
        }
    }

    [Collection("Sequencia1")]
    public class TesteUsuariosApiUpdate2
    {
        RepoTeste teste;

        public TesteUsuariosApiUpdate2(RepoTeste t)
        {
            teste = t;
        }

        [Fact]
        public void TestUpdateComSurnameVazio_ReturnsUser3()
        {
            //Testa se o firstName foi e atualizado e se o surname se manteve sem alterações
            User userInsrt = new User();
            userInsrt.Id = 3;
            userInsrt.firstName = "Teste3";
            userInsrt.surname = null;
            userInsrt.age = 20;
            teste.control.UpdateUser(3,userInsrt);
            userInsrt = teste.repository.GetUserById(3);
            Assert.Equal("Teste3", userInsrt.firstName);
            Assert.Equal("SurnameTeste3", userInsrt.surname);
        
        }
    }

    [Collection("Sequencia1")]
    public class TesteUsuariosApiDelete
    {
        RepoTeste teste;
        public TesteUsuariosApiDelete(RepoTeste t)
        {
            teste = t;
        }

        [Fact]
        public void TesteGetAllUsers_Return5()
        {
            //Veirifica se apos dois usuarios inseridos, o numero de usuarios total esta correto
            Assert.Equal(5,teste.repository.GetAllUsers().Count);
        }

        [Fact]
        public void DeleteUser_ReturnsIdNegativo()
        {
            teste.control.DeleteUser(3);
            Assert.Equal(-1,teste.control.GetUserById(3).Value.Id);
        }

        [Fact]
        public void TesteGetAllUsers_Returns4()
        {
            //Veirifica se o numero de usuarios esta menor
            Assert.Equal(4,teste.repository.GetAllUsers().Count);
        }

    }
}
