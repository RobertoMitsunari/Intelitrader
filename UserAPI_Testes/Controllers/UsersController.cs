using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        private IUserRepo _repository;
        private  IMapper _mapper;
        
        public UsersController(IUserRepo repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }       

        [HttpGet]
        public List<User> GetAllUsers(){
            
            List<User> UserItems = _repository.GetAllUsers();
            return UserItems;
        }

        //get /Users/id
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id){
            
            User UserItem = _repository.GetUserById(id);

            if(UserItem != null){
                return UserItem;
            }else{
                return NotFound();
            }
            
        }
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            
            user.creationDate = DateTime.Now;
            _repository.CreateUser(user);
            /*
            try{
                _repository.SaveChanges();
            }catch (DbUpdateException ex){
                throw ex;
            }
            */
            return user;
        }

        // PUT /Users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id,User userUpdateDto)
        {
            var userModelFromRepo = _repository.GetUserById(id);
            if(userModelFromRepo == null){
                return NotFound();
            }
            if(userUpdateDto.surname == null){
                userUpdateDto.surname = userModelFromRepo.surname;
            }
            _repository.UpdateUser(userUpdateDto);
            /*
            try{
                _repository.SaveChanges();
            }catch(DbUpdateConcurrencyException ex){
                throw ex;
            }
            */
            return Ok();
        }

        //DELETE Users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            User userModelFromRepo = _repository.GetUserById(id);
            if(userModelFromRepo == null){
                return NotFound();
            }
            _repository.DeleteUser(userModelFromRepo);
            /*
            try{
                _repository.SaveChanges();
            }catch(DbUpdateConcurrencyException ex){
                throw ex;
            }
            */
            return Ok();
        }
    }
}