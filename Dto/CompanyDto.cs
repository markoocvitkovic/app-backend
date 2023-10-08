using AstraLicenceManager.Entities;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstraLicenceManager.Dto
{
    public class CompanyDto
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public int SifPar { get; set; }

        public string Name { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public long? ZipCode { get; set; }

        public string? Country { get; set; }

        public string? Email { get; set; }

        public string? Director { get; set; }

        public long? CompanyId { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [ForeignKey("User")]
        public long InsertUserId { get; set; }

        public User? User { get; set; }

        public long? UpdateUserId { get; set;}

    }

    public class CompanyDtoProfile : Profile
    {
        public CompanyDtoProfile()
        {
            CreateMap<CompanyDto, Company>().ForMember(a => a.User , opt => opt.Ignore());

            CreateMap<Company, CompanyDto>();
        }
    }
}
