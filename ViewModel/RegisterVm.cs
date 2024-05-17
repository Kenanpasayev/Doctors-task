using System.ComponentModel.DataAnnotations;

namespace WebApplication12.ViewModel
{
    public class RegisterVm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
     public string  EmailAdress{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password),Compare( nameof(Password))]
        public string ConfrimPassword { get; set; }

        public string ImgUrl { get; set; }

    }
}
