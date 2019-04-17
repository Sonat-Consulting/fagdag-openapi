using System.ComponentModel.DataAnnotations;

namespace NetCoreApi.Model
{
    public class NewEmployee
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}