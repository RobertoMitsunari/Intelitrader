using System;
using System.ComponentModel.DataAnnotations;

namespace UserAPI.Dtos
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string surname { get; set; }
        public int age { get; set; }
        public DateTime creationDate { get; set; }

    }
}