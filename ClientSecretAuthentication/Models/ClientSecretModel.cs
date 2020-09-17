using System.ComponentModel.DataAnnotations;

namespace ClientSecretAuthentication.Models
{
    public class ClientSecretModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}