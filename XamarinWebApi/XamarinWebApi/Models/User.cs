using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace XamarinWebApi.Models
{
    class User
    {
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        public string surname { get; set; }
        [Required,Range(1, int.MaxValue)]
        public int age { get; set; }
        public DateTime creationDate { get; set; }
    }
}
