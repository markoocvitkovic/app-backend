using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstraLicenceManager.Entities
{
    public class App
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

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
