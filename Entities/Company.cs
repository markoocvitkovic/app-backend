using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstraLicenceManager.Entities
{
    public class Company
    {
        [Key]
        public long Id { get; set; }

        public int SifPar { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        public long? ZipCode { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? Director { get; set; }

        public long? CompanyId { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("User")]
        public long InsertUserId { get; set; }

        public User? User { get; set; }

        public long? UpdateUserId { get; set; }
    }
}
