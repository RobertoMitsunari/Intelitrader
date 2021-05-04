using System;
using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        public string surname { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
    }
}