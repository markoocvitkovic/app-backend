using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstraLicenceManager.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set;}

        [MaxLength(100)]
        [Required]
        public string? Email { get; set; }
      
        [MaxLength(100)]
        public string? Password { get; set; }

        public Boolean IsActive { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public long InsertUserId { get; set; }

        public long? UpdateUserId { get; set; }
                                   
    }
}
