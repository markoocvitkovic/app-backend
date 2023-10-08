using AstraLicenceManager.Entities;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstraLicenceManager.Dto
{
    public class LicenceDto
    {
        [Key]
        public long Id { get; set; }
 
        [ForeignKey("App")]
        public long? AppId { get; set; }
       
        public App? App { get; set; }             

        [ForeignKey("AppLevel")]

        public long AppLevelId { get; set; }

        public AppLevel? AppLevel { get; set; } 

        public string? DeviceId { get; set; }

        public string Code { get; set; }

        public string? Description { get; set; }

        public Boolean Active { get; set; }

        public Boolean Update { get; set; }

        public Boolean Permanent { get; set; }

        public DateTime? FirstCheck { get; set;}

        [ForeignKey("Company")]
        public long? CompanyId { get; set; }

        public Company? Company { get; set; }

        public DateTime InsertDate { get; set;}

        public DateTime? UpdateDate { get; set;}

        [ForeignKey("User")]
        public long InsertUserId { get; set; }

        public User? User { get; set; }

        public long? UpdateUserId { get; set;}        

    }

    public class LicenceDtoProfile : Profile
    {
        public LicenceDtoProfile()
        {          

            CreateMap<LicenceDto, Licence>().ForMember(a => a.Company, opt => opt.Ignore())
                                            .ForMember(a => a.App, opt => opt.Ignore())
                                            .ForMember(a => a.User, opt => opt.Ignore());

            CreateMap<Licence, LicenceDto>();
                                        
        }
    }

}
