using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstraLicenceManager.Entities
{
    public class AppLevel
    {
        [Key]
        public long Id { get; set; }

        public int Value { get; set; }

        public string Description { get; set; }

        [ForeignKey("App")]
        public long AppId { get; set; }

        public App App { get; set; }

        [ForeignKey("Company")]
        public long? CompanyId { get; set; }

        public Company? Company { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("User")]
        public long InsertUserId { get; set; }

        public User? User { get; set; }

        public long? UpdateUserId { get; set; }
    }
}
