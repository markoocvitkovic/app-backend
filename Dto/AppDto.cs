using AstraLicenceManager.Entities;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstraLicenceManager.Dto
{
    public class AppDto
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

        public DateTime? UpdateDate { get; set; }

        [ForeignKey("User")]
        public long InsertUserId { get; set; }   
        
        public User? User { get; set; }

        public long? UpdateUserId { get; set; }
    }

    public class AppDtoProfile : Profile
    {
        public AppDtoProfile()
        {
            CreateMap<AppDto, App>().ForMember(a => a.Company, opt => opt.Ignore())
                                    .ForMember(a => a.User, opt => opt.Ignore());

            CreateMap<App, AppDto>();
        }
    }

}
