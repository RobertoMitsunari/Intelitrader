using System.ComponentModel.DataAnnotations;

namespace UserAPI.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public string firstName { get; set; }
        public string surname { get; set; }
        [Required]
        public int age { get; set; }
    }
}