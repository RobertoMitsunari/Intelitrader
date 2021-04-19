using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserAPI.Data;
using UserAPI.Dtos;
using UserAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace UserAPI.Controller
{
    //Users
    [Route("/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public UsersController(IUserRepo repository,IMapper mapper,ILogger<UsersController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }       

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers(){
            _logger.Log(LogLevel.Information,MyLogEvents.ListItems,"Executando /Users GET ");
            var commandItems = _repository.GetAllUsers();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(commandItems));
        }

        //get /Users/id
        [HttpGet("{id}")]
        public ActionResult <UserReadDto> GetUserById(int id){
            _logger.LogInformation(MyLogEvents.GetItem, "Getting user {Id}", id);
            var commandItem = _repository.GetUserById(id);
            if(commandItem != null){
                _logger.LogInformation(MyLogEvents.GetItem, "Users getted {Id}", id);
                return Ok(_mapper.Map<UserReadDto>(commandItem));
            }else{
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "Get({Id}) USER NOT FOUND", id);
                return NotFound();
            }
            
        }
        [HttpPost]
        public ActionResult <UserReadDto> CreateCommand(UserCreateDto userCreateDto)
        {
            _logger.LogInformation(MyLogEvents.InsertItem, "Inserting user");
            var userModel = _mapper.Map<User>(userCreateDto);
            userModel.creationDate = DateTime.Now;
            _repository.CreateUser(userModel);
            try{
                _repository.SaveChanges();
            }catch (DbUpdateException ex){
                _logger.LogError(MyLogEvents.InsertItem,ex," FALHA HTTP POST DbUpdateException");
                throw ex;
            }
            _logger.LogInformation(MyLogEvents.InsertItem, "Inserted user");
            return Ok();
        }

        // PUT /Users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id,UserUpdateDto userUpdateDto)
        {
            var userModelFromRepo = _repository.GetUserById(id);
            if(userModelFromRepo == null){
                _logger.LogWarning(MyLogEvents.UpdateItemNotFound, "Put({Id}) USER NOT FOUND", id);
                return NotFound();
            }
            if(userUpdateDto.surname == null){
                userUpdateDto.surname = userModelFromRepo.surname;
            }
            _mapper.Map(userUpdateDto, userModelFromRepo);
            //Não faz nd mas supsotamente é uma boa prática (?)
            _repository.UpdateUser(userModelFromRepo);
            try{
                _repository.SaveChanges();
            }catch(DbUpdateConcurrencyException ex){
                _logger.LogError(MyLogEvents.InsertItem,ex," FALHA HTTP PUT DbUpdateException");
                throw ex;
            }
             _logger.LogInformation(MyLogEvents.UpdateItem, "Updating user {Id}", id);
            return Ok();
        }

        //DELETE Users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var userModelFromRepo = _repository.GetUserById(id);
            if(userModelFromRepo == null){
                _logger.LogWarning(MyLogEvents.DeleteItem, "Delete({Id}) USER NOT FOUND", id);
                return NotFound();
            }
            _repository.DeleteUser(userModelFromRepo);
            try{
                _repository.SaveChanges();
            }catch(DbUpdateConcurrencyException ex){
                _logger.LogError(MyLogEvents.InsertItem,ex," FALHA HTTP DELETE DbUpdateException");
                throw ex;
            }
            
            _logger.LogInformation(MyLogEvents.DeleteItem, "Deleting user {Id}", id);
            return Ok();
        }
    }
}